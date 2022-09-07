package com.example.petever.oobe;


import android.Manifest;
import android.content.Context;
import android.content.pm.PackageManager;
import android.graphics.Bitmap;
import android.graphics.SurfaceTexture;
import android.hardware.camera2.CameraAccessException;
import android.hardware.camera2.CameraCharacteristics;
import android.hardware.camera2.CameraManager;
import android.hardware.camera2.params.StreamConfigurationMap;
import android.net.Uri;
import android.os.Build;
import android.os.Bundle;

import android.os.Handler;
import android.os.Looper;
import android.util.Log;
import android.util.Size;
import android.view.View;
import android.widget.ImageView;
import android.widget.Toast;


import androidx.annotation.NonNull;
import androidx.annotation.Nullable;
import androidx.appcompat.app.AppCompatActivity;
import androidx.camera.core.Camera;
import androidx.camera.core.CameraSelector;
import androidx.camera.core.ImageAnalysis;
import androidx.camera.core.ImageCapture;
import androidx.camera.core.ImageCaptureException;
import androidx.camera.core.ImageProxy;
import androidx.camera.core.Preview;
import androidx.camera.lifecycle.ProcessCameraProvider;
import androidx.camera.view.PreviewView;
import androidx.core.app.ActivityCompat;
import androidx.core.content.ContextCompat;

import com.example.petever.R;
import com.google.common.util.concurrent.ListenableFuture;

import java.io.File;
import java.text.SimpleDateFormat;
import java.util.Date;
import java.util.Locale;
import java.util.concurrent.ExecutionException;
import java.util.concurrent.Executor;
import java.util.concurrent.Executors;


public class CameraActivity extends AppCompatActivity {
    private Executor executor = Executors.newSingleThreadExecutor();
    public static final String TAG = "CameraActivity";
    private ImageView btn_camera;
    private PreviewView previewView;
    private ListenableFuture<ProcessCameraProvider> cameraProviderFuture;

    public static Context context;

    @Override
    protected void onCreate(@Nullable Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        checkPermission();
        context = this;
        setContentView(R.layout.activity_camera);
        requestProcessCameraProvider();
        initView();
    }

    private void requestProcessCameraProvider() {
        cameraProviderFuture = ProcessCameraProvider.getInstance(this);
        cameraProviderFuture.addListener(() -> {
            try {
                ProcessCameraProvider cameraProvider = cameraProviderFuture.get();
                bindPreview(cameraProvider);
            } catch (ExecutionException | InterruptedException | CameraAccessException e) {
                Log.e(TAG, "cameraProviderFuture : " + e);
            }
        }, ContextCompat.getMainExecutor(this));

    }

    private void initView() {
        btn_camera = findViewById(R.id.btn_take_camera);
        previewView = findViewById(R.id.previewView);
    }


    private void bindPreview(@NonNull ProcessCameraProvider cameraProvider) throws
            CameraAccessException {
        Size targetResolution = new Size(960, 720);
        Preview preview = new Preview.Builder().setTargetResolution(targetResolution).build();

        CameraSelector cameraSelector =
                new CameraSelector.Builder().build();

        ImageAnalysis imageAnalysis =
                new ImageAnalysis.Builder()
                        .setTargetResolution(targetResolution)
                        .setBackpressureStrategy(ImageAnalysis.STRATEGY_KEEP_ONLY_LATEST)
                        .build();
        // Image analysis sample.
        imageAnalysis.setAnalyzer(
                executor,
                (image) -> {
//                    System.out.println("Image height: " + image.getHeight());
//                    System.out.println("Image width: " + image.getWidth());
                    image.close();
                });

        ImageCapture.Builder imageCaptureBuilder =
                new ImageCapture.Builder().setTargetResolution(targetResolution);


        final ImageCapture imageCapture =
                imageCaptureBuilder
                        .setTargetRotation(this.getWindowManager().getDefaultDisplay().getRotation())
                        .build();

        preview.setSurfaceProvider(previewView.getSurfaceProvider());

        Camera camera = cameraProvider.bindToLifecycle(this, cameraSelector, preview, imageAnalysis, imageCapture);

        btn_camera.setOnClickListener(
                (unused) -> {
                    File dir =
                            new File(
                                    getApplicationContext().getExternalCacheDir(),
                                    "PetEver");
                    if (dir.exists() || dir.mkdirs()) {
                        SimpleDateFormat dataFormat = new SimpleDateFormat("yyyy-MM-dd HH:mm:ss", Locale.US);
                        File file = new File(dir, dataFormat.format(new Date()) + ".png");
                        ImageCapture.OutputFileOptions outputFileOptions =
                                new ImageCapture.OutputFileOptions.Builder(file).build();

                        imageCapture.takePicture(executor, new ImageCapture.OnImageCapturedCallback() {
                            @Override
                            public void onCaptureSuccess(@NonNull ImageProxy image) {
                                //Run Inference for Breed Classification
                                MLClass inf = new MLClass();
                                Bitmap btmImg = ImageUtils.convertImageProxyToBitmap(image);
                                String breed = inf.runBreedClassification(btmImg);
                                Log.d("RESULT", "RESULT : " + breed);
                                runOnUiThread(new Runnable() {
                                    @Override
                                    public void run() {
                                        ImageView previewImage = findViewById(R.id.previewImage);
                                        previewImage.setVisibility(View.VISIBLE);
                                        previewImage.setImageBitmap(ImageUtils.rotateImage(btmImg, 90));
                                    }
                                });
                                image.close();
                                super.onCaptureSuccess(image);
                            }
                        });
                    }
                });
        // Show all supported output sizes.
        CameraManager cameraManager = (CameraManager) getSystemService(Context.CAMERA_SERVICE);
        for (String cameraId : cameraManager.getCameraIdList()) {
            CameraCharacteristics cameraCharacteristics =
                    cameraManager.getCameraCharacteristics(cameraId);
            StreamConfigurationMap configMap =
                    cameraCharacteristics.get(CameraCharacteristics.SCALER_STREAM_CONFIGURATION_MAP);
            Size[] outputSizes = configMap.getOutputSizes(SurfaceTexture.class);
            for (Size size : outputSizes) {
                System.out.println("Camera " + cameraId + " output size: " + String.valueOf(size));
            }
        }
    }


    private void checkPermission() {
        if (Build.VERSION.SDK_INT >= Build.VERSION_CODES.M) {
            if (checkSelfPermission(Manifest.permission.CAMERA) == PackageManager.PERMISSION_GRANTED &&
                    checkSelfPermission(Manifest.permission.WRITE_EXTERNAL_STORAGE) == PackageManager.PERMISSION_GRANTED) {
                Log.d("PERM", "퍼미션 요청");
            } else {
                ActivityCompat.requestPermissions(CameraActivity.this, new String[]{Manifest.permission.CAMERA, Manifest.permission.WRITE_EXTERNAL_STORAGE}, 1);
            }
        }
    }
}


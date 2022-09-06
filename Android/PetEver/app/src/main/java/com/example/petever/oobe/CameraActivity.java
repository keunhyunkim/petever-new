package com.example.petever.oobe;


import android.content.Context;
import android.graphics.SurfaceTexture;
import android.hardware.camera2.CameraAccessException;
import android.hardware.camera2.CameraCharacteristics;
import android.hardware.camera2.CameraManager;
import android.hardware.camera2.params.StreamConfigurationMap;
import android.net.Uri;
import android.os.Bundle;

import android.os.Handler;
import android.os.Looper;
import android.util.Log;
import android.util.Size;
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
import androidx.camera.core.Preview;
import androidx.camera.lifecycle.ProcessCameraProvider;
import androidx.camera.view.PreviewView;
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

    @Override
    protected void onCreate(@Nullable Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
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
                    System.out.println("Image height: " + image.getHeight());
                    System.out.println("Image width: " + image.getWidth());
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

                        imageCapture.takePicture(
                                outputFileOptions,
                                executor,
                                new ImageCapture.OnImageSavedCallback() {
                                    @Override
                                    public void onImageSaved(@NonNull ImageCapture.OutputFileResults outputFileResults) {

                                        Handler mHandler = new Handler(Looper.getMainLooper());
                                        Log.d(TAG, "Saved File :" + Uri.fromFile(file));
                                        mHandler.postDelayed(new Runnable() {
                                            @Override
                                            public void run() {
                                                Toast.makeText(CameraActivity.this, "Image Saved successfully ",
                                                        Toast.LENGTH_SHORT).show();
                                            }
                                        }, 0);
                                    }

                                    @Override
                                    public void onError(@NonNull ImageCaptureException exception) {
                                        exception.printStackTrace();
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

}


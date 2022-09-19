package com.example.petever.oobe;


import android.Manifest;
import android.content.Context;
import android.content.pm.PackageManager;
import android.graphics.Bitmap;
import android.hardware.camera2.CameraAccessException;
import android.hardware.camera2.CameraCharacteristics;
import android.hardware.camera2.CameraManager;
import android.hardware.camera2.params.StreamConfigurationMap;
import android.os.Build;
import android.os.Bundle;

import android.os.Handler;
import android.os.Looper;
import android.os.VibrationEffect;
import android.os.Vibrator;
import android.provider.MediaStore;
import android.util.Log;
import android.util.Size;
import android.view.View;
import android.widget.Button;
import android.widget.ImageView;
import android.widget.TextView;
import android.widget.Toast;


import androidx.annotation.NonNull;
import androidx.annotation.Nullable;
import androidx.appcompat.app.AppCompatActivity;
import androidx.camera.core.Camera;
import androidx.camera.core.CameraSelector;
import androidx.camera.core.ImageAnalysis;
import androidx.camera.core.ImageCapture;
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
    private static final String TAG = "CameraActivity";
    private static final String timeFormat = "yyyy-MM-dd HH:mm:ss";
    public static Context context;

    private Executor executor = Executors.newSingleThreadExecutor();
    private View viewDimmed;
    private Button btnCharacter;
    private Button btnRetry;
    private PreviewView previewView;
    private TextView textBreed;
    private TextView textCameraGuide;
    private ImageView imgBreedBubble;
    private ImageView imgPreview;
    private ImageView btnCamera;
    private ListenableFuture<ProcessCameraProvider> cameraProviderFuture;

    @Override
    protected void onCreate(@Nullable Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        checkPermission();
        context = this;
        setContentView(R.layout.activity_camera);
        requestProcessCameraProvider();
        initView();
    }

    private void cameraHaptic() {
        Vibrator vibrator = (Vibrator) getSystemService(Context.VIBRATOR_SERVICE);
        if (Build.VERSION.SDK_INT >= 26) {
            vibrator.vibrate(VibrationEffect.createOneShot(50, VibrationEffect.DEFAULT_AMPLITUDE));
        } else {
            vibrator.vibrate(50);
        }
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
        btnCamera = findViewById(R.id.btn_take_camera);
        previewView = findViewById(R.id.previewView);
        btnRetry = findViewById(R.id.btn_retry);
        btnCharacter = findViewById(R.id.btn_character);
        viewDimmed = findViewById(R.id.view_dimmed);
        textBreed = findViewById(R.id.text_breed);
        imgBreedBubble = findViewById(R.id.image_bubble);
        textCameraGuide = findViewById(R.id.text_camera_guide);
        imgPreview = findViewById(R.id.previewImage);
        btnRetry.setOnClickListener(view -> {
            updateViewRetry();
        });
    }

    private void bindPreview(@NonNull ProcessCameraProvider cameraProvider) throws
            CameraAccessException {
        Size targetResolution = new Size(720, 1600);
        Preview preview = new Preview.Builder().setTargetResolution(targetResolution).build();

        CameraSelector cameraSelector = new CameraSelector.Builder().build();

        ImageAnalysis imageAnalysis =
                new ImageAnalysis.Builder()
                        .setTargetResolution(targetResolution)
                        .setBackpressureStrategy(ImageAnalysis.STRATEGY_KEEP_ONLY_LATEST)
                        .build();
        // Image analysis sample.
        imageAnalysis.setAnalyzer(executor, (image) -> {
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

        btnCamera.setOnClickListener((view) -> {
            cameraHaptic();

            File dir = new File(getApplicationContext().getExternalCacheDir(), "PetEver");
            if (dir.exists() || dir.mkdirs()) {
                SimpleDateFormat dataFormat = new SimpleDateFormat("yyyy-MM-dd HH:mm:ss", Locale.US);
                File file = new File(dir, dataFormat.format(new Date()) + ".png");
                ImageCapture.OutputFileOptions outputFileOptions =
                        new ImageCapture.OutputFileOptions.Builder(file).build();

                imageCapture.takePicture(executor, new ImageCapture.OnImageCapturedCallback() {
                    @Override
                    public void onCaptureSuccess(@NonNull ImageProxy image) {
                        super.onCaptureSuccess(image);
                        //Run Inference for Breed Classification
                        MLClass inf = new MLClass();
                        Bitmap btmImg = ImageUtils.rotateImage(ImageUtils.convertImageProxyToBitmap(image), 90);

                        SimpleDateFormat dataFormat = new SimpleDateFormat(timeFormat, Locale.KOREA);
                        MediaStore.Images.Media.insertImage(getContentResolver(), btmImg, dataFormat.format(new Date()) + ".png", "taken by petEver");

                        String breed = inf.runBreedClassification(btmImg, CameraActivity.this);
                        Handler mHandler = new Handler(Looper.getMainLooper());
                        Log.d("RESULT", "RESULT : " + breed);
                        if (breed.equals("Retry")) {
                            mHandler.postDelayed(() ->
                                    Toast.makeText(CameraActivity.this, getResources().getString(R.string.invalid_value),
                                            Toast.LENGTH_SHORT).show(), 0
                            );
                            image.close();
                            return;
                        }
                        runOnUiThread(() -> updateViewByBreed(btmImg, breed));
                        image.close();
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
        }
    }

    private void updateViewByBreed(Bitmap btmImg, String breed) {
        imgPreview.setVisibility(View.VISIBLE);
        imgPreview.setImageBitmap(btmImg);
        textCameraGuide.setVisibility(View.INVISIBLE);
        btnRetry.setVisibility(View.VISIBLE);
        btnCamera.setVisibility(View.INVISIBLE);
        btnCharacter.setBackgroundResource(R.drawable.button_yellow);
        btnCharacter.setTextColor(getResources().getColor(R.color.black_text));
        textBreed.setText(setBreed(breed));
        textBreed.setVisibility(View.VISIBLE);
        imgBreedBubble.setVisibility(View.VISIBLE);
        viewDimmed.setVisibility(View.VISIBLE);
    }

    private void updateViewRetry() {
        imgPreview.setVisibility(View.INVISIBLE);
        textCameraGuide.setVisibility(View.VISIBLE);
        btnRetry.setVisibility(View.INVISIBLE);
        btnCamera.setVisibility(View.VISIBLE);
        btnCharacter.setBackgroundResource(R.drawable.button_gray);
        btnCharacter.setTextColor(getResources().getColor(R.color.gray_dimmed));
        textBreed.setVisibility(View.INVISIBLE);
        imgBreedBubble.setVisibility(View.INVISIBLE);
        viewDimmed.setVisibility(View.INVISIBLE);
    }

    private String setBreed(String breed) { // TODO : convert to enum
        switch (breed) {
            case "Pome":
                return getResources().getString((R.string.breed_pome));
            case "Maltese":
                return getResources().getString((R.string.breed_maltese));
            default:
                return breed;
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


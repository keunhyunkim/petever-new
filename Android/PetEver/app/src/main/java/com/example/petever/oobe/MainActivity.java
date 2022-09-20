package com.example.petever.oobe;


import android.content.Context;
import android.content.Intent;
import android.graphics.Bitmap;
import android.graphics.ImageDecoder;
import android.net.Uri;
import android.os.Build;
import android.os.Bundle;
import android.provider.MediaStore;
import android.util.Log;
import android.view.View;
import android.widget.Button;
import android.widget.Toast;


import androidx.activity.result.ActivityResultLauncher;
import androidx.activity.result.PickVisualMediaRequest;
import androidx.activity.result.contract.ActivityResultContracts;
import androidx.annotation.Nullable;
import androidx.appcompat.app.AppCompatActivity;

import com.example.petever.R;
import com.example.petever.util.IntentUtil;

import java.io.IOException;

public class MainActivity extends AppCompatActivity {
    private static final String TAG = "MainActivity";
    private ActivityResultLauncher<PickVisualMediaRequest> pickMedia;
    private static Context context;


    @Override
    protected void onCreate(@Nullable Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        context = this;
        setContentView(R.layout.activity_main);
        initView();
        registerMediaPick();
    }

    private void photoPicked(Uri uri) {
        {
            // Callback is invoked after the user selects a media item or closes the
            // photo picker.
            if (uri != null) {
                Log.d("PhotoPicker", "Selected URI: " + uri);
                Bitmap bitmap = null;
                try {
                    if (Build.VERSION.SDK_INT >= Build.VERSION_CODES.P) {
                        bitmap = ImageDecoder.decodeBitmap(ImageDecoder.createSource(getContentResolver(), uri));

                    } else {
                        bitmap = MediaStore.Images.Media.getBitmap(getContentResolver(), uri);
                    }
                    MLClass inf = new MLClass();
                    String breed = inf.runBreedClassification(bitmap, MainActivity.this);
                    if (breed.equals("Retry")) {
                        Toast.makeText(MainActivity.this, "Invalid, Try Again T.T",
                                Toast.LENGTH_SHORT).show();
                    } else {
                        IntentUtil.intentBreedActivity(TAG, context, uri, breed);
                    }
                } catch (IOException e) {
                    e.printStackTrace();
                }

            } else {
                Log.d("PhotoPicker", "No media selected");
            }
        }
    }

    private void registerMediaPick() {
        // https://developer.android.com/training/data-storage/shared/photopicker#custom-select-single
        // Registers a photo picker activity launcher in single-select mode.
        pickMedia = registerForActivityResult(new ActivityResultContracts.PickVisualMedia(), uri -> photoPicked(uri));
    }

    private void initView() {
        Button btn_camera = findViewById(R.id.btn_camera);
        btn_camera.setOnClickListener(view -> {
            Intent intent = new Intent(MainActivity.this, CameraActivity.class);
            startActivity(intent);
        });


        Button btn_album = findViewById(R.id.btn_album);
        btn_album.setOnClickListener(view -> {
            // Launch the photo picker and allow the user to choose only images.
            pickMedia.launch(new PickVisualMediaRequest.Builder()
                    .setMediaType(ActivityResultContracts.PickVisualMedia.ImageOnly.INSTANCE)
                    .build());
        });
    }

    //https://developer.android.com/training/system-ui/immersive#java
    @Override
    public void onWindowFocusChanged(boolean hasFocus) {
        super.onWindowFocusChanged(hasFocus);
        if (hasFocus) {
            hideSystemUI();
        }
    }

    private void hideSystemUI() {
        // Enables regular immersive mode.
        // For "lean back" mode, remove SYSTEM_UI_FLAG_IMMERSIVE.
        // Or for "sticky immersive," replace it with SYSTEM_UI_FLAG_IMMERSIVE_STICKY
        View decorView = getWindow().getDecorView();
        decorView.setSystemUiVisibility(
                View.SYSTEM_UI_FLAG_IMMERSIVE_STICKY
                        // Set the content to appear under the system bars so that the
                        // content doesn't resize when the system bars hide and show.
                        | View.SYSTEM_UI_FLAG_LAYOUT_STABLE
                        | View.SYSTEM_UI_FLAG_LAYOUT_HIDE_NAVIGATION
                        | View.SYSTEM_UI_FLAG_LAYOUT_FULLSCREEN
                        // Hide the nav bar and status bar
                        | View.SYSTEM_UI_FLAG_HIDE_NAVIGATION
                        | View.SYSTEM_UI_FLAG_FULLSCREEN);
    }


}

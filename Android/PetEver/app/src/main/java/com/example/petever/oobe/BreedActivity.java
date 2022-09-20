package com.example.petever.oobe;

import android.content.Context;
import android.content.Intent;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.net.Uri;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.ImageView;
import android.widget.TextView;

import androidx.annotation.Nullable;
import androidx.appcompat.app.AppCompatActivity;

import com.example.petever.R;

public class BreedActivity extends AppCompatActivity {
    private static final String TAG = "BreedActivity";
    private TextView textBreed;
    private ImageView imgPreview;
    private Button btnRetry;

    @Override
    protected void onCreate(@Nullable Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_breed);
        initView();


    }


    private void initView() {
        btnRetry = findViewById(R.id.btn_retry);
        imgPreview = findViewById(R.id.previewImage);
        textBreed = findViewById(R.id.text_breed);
        btnRetry.setOnClickListener(view -> {
            finish();
        });
        Intent extras = getIntent();
        if (extras != null) {
            String imagePath = extras.getStringExtra("image");
            String breed = extras.getStringExtra("breed");
            Uri fileUri = Uri.parse(imagePath);
            imgPreview.setImageURI(fileUri);
            textBreed.setText(setBreed(breed));
        }
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


}

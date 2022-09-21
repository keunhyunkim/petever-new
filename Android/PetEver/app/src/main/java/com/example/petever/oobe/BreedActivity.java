package com.example.petever.oobe;

import android.content.Intent;
import android.net.Uri;
import android.os.Bundle;
import android.util.Log;
import android.widget.Button;
import android.widget.ImageView;
import android.widget.TextView;

import androidx.annotation.Nullable;
import androidx.appcompat.app.AppCompatActivity;

import com.example.petever.R;
import com.example.petever.domain.Breed;

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
        processIntentData();
    }


    private void initView() {
        btnRetry = findViewById(R.id.btn_retry);
        imgPreview = findViewById(R.id.previewImage);
        textBreed = findViewById(R.id.text_breed);
        btnRetry.setOnClickListener(view -> {
            finish();
        });
    }

    private void processIntentData() {
        Intent extras = getIntent();
        if (extras != null) {
            String activity = extras.getStringExtra("activity");
            String imagePath = extras.getStringExtra("image");
            String breed = extras.getStringExtra("breed");
            Uri fileUri = Uri.parse(imagePath);
            imgPreview.setImageURI(fileUri);
            try {
                int textCode = Breed.get(breed).getBubbleTextCode();
                if (textCode != 0) {
                    textBreed.setText(getResources().getString(textCode));
                }
            } catch (NullPointerException e) {
                Log.e(TAG, e.toString());
            }

            if ("MainActivity".equals(activity)) {
                btnRetry.setText(R.string.btn_retry_album);
            }
        }
    }


}

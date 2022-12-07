package com.example.petever.oobe;

import android.content.Intent;
import android.content.SharedPreferences;
import android.net.Uri;
import android.os.Bundle;
import android.util.Log;
import android.widget.Button;
import android.widget.ImageView;
import android.widget.TextView;
import android.widget.Toast;

import androidx.annotation.Nullable;
import androidx.appcompat.app.AppCompatActivity;

import com.example.petever.R;
import com.example.petever.domain.Breed;
import com.unity3d.player.UnityPlayerActivity;

public class BreedActivity extends AppCompatActivity {
    private static final String TAG = "BreedActivity";
    private TextView textBreed;
    private ImageView imgPreview;
    private Button btnRetry;
    private Button btnCharacter;
    private String activity;
    private String imagePath;
    private String breed;
    private String petName;
    private String petRelationship;
    private Uri fileUri;
    private SharedPreferences pf;


    @Override
    protected void onCreate(@Nullable Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_breed);
        processIntentData();
        initView();
        processView();
    }


    private void initView() {
        btnCharacter = findViewById(R.id.btn_character);
        btnRetry = findViewById(R.id.btn_retry);
        imgPreview = findViewById(R.id.previewImage);
        textBreed = findViewById(R.id.text_breed);
        btnRetry.setOnClickListener(view -> {
            finish();
        });
        btnCharacter.setOnClickListener(view -> {
            if (Breed.get(breed).getMLCode() >= 3) {
                Toast.makeText(this, "Not Support T.T",
                        Toast.LENGTH_SHORT).show();
            } else {
                Intent intent = new Intent(this, UnityPlayerActivity.class);
                intent.putExtra("breed", breed);
                petName = pf.getString("PetName", "NoName");
                petRelationship = pf.getString("PetRelationship", "NoRelationship");
                intent.putExtra("petname", petName);
                intent.putExtra("petrelationship", petRelationship);
                startActivity(intent);
            }
        });
    }

    private void processIntentData() {
        Intent extras = getIntent();
        if (extras != null) {
            activity = extras.getStringExtra("activity");
            imagePath = extras.getStringExtra("image");
            breed = extras.getStringExtra("breed");
            fileUri = Uri.parse(imagePath);
        }

        pf = getSharedPreferences("PetInfo", MODE_PRIVATE);
    }

    private void processView() {
        imgPreview.setImageURI(fileUri);
        try {
            int textCode = Breed.get(breed).getBubbleStringCode();
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

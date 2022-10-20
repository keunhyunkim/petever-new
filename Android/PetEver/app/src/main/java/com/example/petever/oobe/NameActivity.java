package com.example.petever.oobe;

import androidx.appcompat.app.AppCompatActivity;
import androidx.camera.core.TorchState;
import androidx.constraintlayout.widget.ConstraintLayout;
import androidx.core.view.WindowInsetsCompat;
import androidx.core.view.WindowInsetsControllerCompat;

import android.content.Intent;
import android.content.SharedPreferences;
import android.graphics.Color;
import android.os.Bundle;
import android.view.KeyEvent;
import android.view.MotionEvent;
import android.view.View;
import android.view.Window;
import android.view.inputmethod.EditorInfo;
import android.view.inputmethod.InputMethodManager;
import android.widget.Button;
import android.widget.EditText;
import android.widget.TextView;

import com.example.petever.R;

public class NameActivity extends AppCompatActivity {

    private String petName;
    private String petRelationship;
    private ConstraintLayout hidden_layer;
    private EditText name_new;
    private EditText relation_new;
    private Button btn_name;
    private ConstraintLayout nameLayout;
    private float dimRatio = 0.9f;
    private InputMethodManager InputManager;
    private boolean isNameSet = false;
    private boolean isRelationSet = false;

    private SharedPreferences pf;

    public String getPetName() {
        return this.petName;
    }

    public String getPetRelationship() {
        return this.petRelationship;
    }

    void keyBoardHide() {
        Window window = getWindow();
        new WindowInsetsControllerCompat(window, window.getDecorView()).hide(WindowInsetsCompat.Type.ime());
    }

    void keyBoardShow() {
        Window window = getWindow();
        new WindowInsetsControllerCompat(window, window.getDecorView()).show(WindowInsetsCompat.Type.ime());
    }

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_name);


        name_new = findViewById(R.id.name_new);
        relation_new = findViewById(R.id.name_relation_new);
        hidden_layer = findViewById(R.id.hidden_layer);
        btn_name = findViewById(R.id.btn_name);
        nameLayout = findViewById(R.id.name_layout);

        name_new.setTextColor(Color.BLACK);
        relation_new.setTextColor(Color.BLACK);

        pf = getSharedPreferences("PetInfo", MODE_PRIVATE);

        nameLayout.setOnTouchListener(new View.OnTouchListener() {
            @Override
            public boolean onTouch(View v, MotionEvent event) {
                switch (event.getAction()) {
                    case MotionEvent.ACTION_DOWN:
                    case MotionEvent.ACTION_UP:
                        hidden_layer.setVisibility(View.INVISIBLE);
                        keyBoardHide();
                }
                return false;
            }
        });

        InputManager = (InputMethodManager)getSystemService(INPUT_METHOD_SERVICE);

        name_new.setOnFocusChangeListener(new View.OnFocusChangeListener() {
            @Override
            public void onFocusChange(View v, boolean hasFocus) {
                if (hasFocus) {
                    if (isNameSet == false) {
                        name_new.setText("");
                    }
                    hidden_layer.setVisibility(View.VISIBLE);
                    relation_new.setTranslationZ(-1);
                    name_new.setTranslationZ(100);
                }
            }
        });

        relation_new.setOnFocusChangeListener(new View.OnFocusChangeListener() {
            @Override
            public void onFocusChange(View v, boolean hasFocus) {
                if (hasFocus) {
                    if (isRelationSet == false) {
                        relation_new.setText("");
                    }
                    hidden_layer.setVisibility(View.VISIBLE);
                    name_new.setTranslationZ(-1);
                    relation_new.setTranslationZ(100);
                }
            }
        });

        name_new.setOnEditorActionListener(new TextView.OnEditorActionListener() {
            @Override
            public boolean onEditorAction(TextView v, int actionId, KeyEvent event) {
                switch (actionId) {
                    case EditorInfo.IME_ACTION_DONE:
                        keyBoardHide();
                    case EditorInfo.IME_ACTION_NEXT:
                    default:
                        // Save the Pet Name into global variable, petName
                        petName = v.getText().toString();
                        isNameSet = true;
                        SharedPreferences.Editor editor = pf.edit();
                        editor.putString("PetName", petName);
                        editor.commit();
                        hidden_layer.setVisibility(View.INVISIBLE);
                }
                name_new.setTranslationZ(-1);
                return false;
            }
        });

        relation_new.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                hidden_layer.setVisibility(View.VISIBLE);
                name_new.setTranslationZ(-1);
                relation_new.setTranslationZ(100);
            }
        });

        relation_new.setOnEditorActionListener(new TextView.OnEditorActionListener() {
            @Override
            public boolean onEditorAction(TextView v, int actionId, KeyEvent event) {
                switch (actionId) {
                    case EditorInfo.IME_ACTION_NEXT:
                    case EditorInfo.IME_ACTION_DONE:
                        keyBoardHide();
                    default:
                        // Save the Relationship into global variable, petRelationship
                        petRelationship = v.getText().toString();
                        isRelationSet = true;
                        SharedPreferences.Editor editor = pf.edit();
                        editor.putString("PetRelationship", petRelationship);
                        editor.commit();
                        hidden_layer.setVisibility(View.INVISIBLE);
                }
                relation_new.setTranslationZ(-1);
                return false;
            }
        });

        btn_name.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Intent intent = new Intent(NameActivity.this, MainActivity.class);
                startActivity(intent);
            }
        });
    }
}
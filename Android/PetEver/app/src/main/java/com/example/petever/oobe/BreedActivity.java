package com.example.petever.oobe;

import android.content.Intent;
import android.content.SharedPreferences;
import android.database.Cursor;
import android.net.Uri;
import android.os.Bundle;
import android.provider.MediaStore;
import android.util.Log;
import android.widget.Button;
import android.widget.ImageView;
import android.widget.TextView;
import android.widget.Toast;

import androidx.annotation.Nullable;
import androidx.appcompat.app.AppCompatActivity;

import com.example.petever.R;
import com.example.petever.domain.Breed;
import com.example.petever.util.FaceColor;
import com.example.petever.util.NetworkService;
import com.unity3d.player.UnityPlayerActivity;

import org.json.JSONException;
import org.json.JSONObject;

import java.io.File;
import java.io.IOException;
import java.util.concurrent.TimeUnit;

import okhttp3.MediaType;
import okhttp3.MultipartBody;
import okhttp3.OkHttpClient;
import okhttp3.RequestBody;
import okhttp3.ResponseBody;
import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;
import retrofit2.Retrofit;
import retrofit2.converter.gson.GsonConverterFactory;

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

    private String section1Color;
    private String section2Color;

    private static String baseUrl = "http://3.35.242.182:3200/";
    private NetworkService networkService;

    Retrofit retrofit;
    Call<FaceColor> call;

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
                // Send the image to Server, Server will give the breed and color
                sendImageToServer(fileUri);

                // After extracting the color, launch the unity
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

    private void sendImageToServer(Uri imageUri) {
        // timeout setting
        OkHttpClient okHttpClient = new OkHttpClient().newBuilder()
                .connectTimeout(30, TimeUnit.SECONDS)
                .readTimeout(30, TimeUnit.SECONDS)
                .writeTimeout(30, TimeUnit.SECONDS)
                .build();

        File imageFile = new File(getRealPathFromURI(imageUri));
        RequestBody requestBody = RequestBody.create(MediaType.parse("image/*"), imageFile);
        MultipartBody.Part multipartBody = MultipartBody.Part.createFormData("image", imageFile.getName(), requestBody);

        Retrofit retrofit = new Retrofit.Builder()
                .baseUrl(baseUrl)
                .client(okHttpClient)
                .addConverterFactory(GsonConverterFactory.create())
                .build();

        NetworkService apiEndpointInterface = retrofit.create(NetworkService.class);

        Call<ResponseBody> call = apiEndpointInterface.uploadImage(multipartBody);

        call.enqueue(new Callback<ResponseBody>() {
            @Override
            public void onResponse(Call<ResponseBody> call, Response<ResponseBody> response) {
                if (response.isSuccessful()) {
                    try {
                        JSONObject jsonObject = new JSONObject(response.body().string());
                        String status = jsonObject.getString("status");
                        String message = jsonObject.getString("message");
                        String breedName = jsonObject.getString("breed");

                        if (status.equals("success")) {
                            JSONObject breedObject = jsonObject.getJSONObject("content");
//                                    String breedName = breedObject.getString("name");
                            section1Color = breedObject.getString("section1");
                            section2Color = breedObject.getString("section2");

                            String toastMessage = "Breed: " + breedName + "\n"
                                    + "Section 1: " + section1Color + "\n"
                                    + "Section 2: " + section2Color;
                            Log.d("RETROFIT", toastMessage);
                        } else {
                            Log.d("RETROFIT", message);
                        }

                    } catch (JSONException | IOException e) {
                        e.printStackTrace();
                        Log.d("RETROFIT", "Error1: "  + e.getMessage());
                    }
                } else {
                    Log.d("RETROFIT", "Error2: " + response.message());
                }
            }

            @Override
            public void onFailure(Call<ResponseBody> call, Throwable t) {
                t.printStackTrace();
                Log.d("RETROFIT", "Error3: " + t.getMessage());
            }
        });
    }

    // Get the real path of an image URI
    public String getRealPathFromURI(Uri uri) {
        String[] projection = {MediaStore.Images.Media.DATA};
        Cursor cursor = getContentResolver().query(uri, projection, null, null, null);
        int columnIndex = cursor.getColumnIndexOrThrow(MediaStore.Images.Media.DATA);
        cursor.moveToFirst();
        String filePath = cursor.getString(columnIndex);
        cursor.close();
        return filePath;
    }
}

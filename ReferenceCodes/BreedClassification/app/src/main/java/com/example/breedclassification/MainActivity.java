package com.example.breedclassification;

import android.Manifest;
import android.app.Activity;
import android.content.Intent;
import android.content.pm.PackageManager;
import android.content.res.AssetFileDescriptor;
import android.graphics.Bitmap;
import android.graphics.Color;
import android.graphics.ImageDecoder;
import android.net.Uri;
import android.os.Build;
import android.os.Bundle;
import android.os.Environment;
import android.os.SystemClock;
import android.provider.MediaStore;
import android.util.Log;
import android.view.View;
import android.widget.ImageView;
import android.widget.TextView;
import android.widget.Toast;

import androidx.activity.result.ActivityResult;
import androidx.activity.result.ActivityResultCallback;
import androidx.activity.result.ActivityResultLauncher;
import androidx.activity.result.contract.ActivityResultContracts;
import androidx.annotation.Nullable;
import androidx.appcompat.app.AppCompatActivity;
import androidx.core.app.ActivityCompat;
import androidx.core.content.FileProvider;

import org.tensorflow.lite.Interpreter;
import org.tensorflow.lite.Tensor;

import java.io.File;
import java.io.FileInputStream;
import java.io.FileNotFoundException;
import java.io.IOException;
import java.nio.ByteBuffer;
import java.nio.ByteOrder;
import java.nio.MappedByteBuffer;
import java.nio.channels.FileChannel;
import java.text.SimpleDateFormat;
import java.util.Date;
import java.util.Objects;


public class MainActivity extends AppCompatActivity {
    final static int REQUEST_TAKE_PHOTO = 1;
    final static int REQUEST_GALLERY = 2;

    final static String[] breeds = {"Maltese", "Golden", "Pug", "Pome", "Poodle"};

    final static int breedCount = 5;

    private int argmax(float[][] target) {
        int idx = 0;
        int maxIdx = idx;
        float max = target[0][0];
        for(idx = 1; idx < target[0].length; idx++) {
            if (target[0][idx] > max) {
                maxIdx = idx;
                max = target[0][idx];
            }
        }
        return maxIdx;
    }

    private void checkPermission() {
        if (Build.VERSION.SDK_INT >= Build.VERSION_CODES.M) {
            if (checkSelfPermission(Manifest.permission.CAMERA) == PackageManager.PERMISSION_GRANTED &&
                    checkSelfPermission(Manifest.permission.WRITE_EXTERNAL_STORAGE) == PackageManager.PERMISSION_GRANTED) {
                Log.d("PERM", "±ÇÇÑ Œ³Á€ ¿Ï·á");
            } else {
                //ToDo
                ActivityCompat.requestPermissions(MainActivity.this, new String[]{Manifest.permission.CAMERA, Manifest.permission.WRITE_EXTERNAL_STORAGE}, 1);
            }
        }
    }

    int imgsize = 299; //flower 224
    private ByteBuffer preprocessImg(Bitmap bmp) {
        Bitmap bitmap = Bitmap.createScaledBitmap(bmp, imgsize, imgsize, true);
        Bitmap tmp = bitmap.copy(Bitmap.Config.RGBA_F16, true);
        ByteBuffer input = ByteBuffer.allocateDirect(299 * 299 * 3 * 4).order(ByteOrder.nativeOrder());
        for (int y = 0; y < imgsize; y++) {
            for (int x = 0; x < imgsize; x++) {
                int px = tmp.getPixel(x, y);

                // Get channel values from the pixel value.
                int r = Color.red(px);
                int g = Color.green(px);
                int b = Color.blue(px);

                // Normalize channel values to [-1.0, 1.0]. This requirement depends
                // on the model. For example, some models might require values to be
                // normalized to the range [0.0, 1.0] instead.
                float rf = (r - 127) / 255.0f;
                float gf = (g - 127) / 255.0f;
                float bf = (b - 127) / 255.0f;

                input.putFloat(rf);
                input.putFloat(gf);
                input.putFloat(bf);
            }
        }
        return input;
    }

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        checkPermission();

        findViewById(R.id.button_1).setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                openCamera();
            }
        });

        findViewById(R.id.button_2).setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                // Read the data(Picture) from the Gallery
                Intent intent = new Intent(Intent.ACTION_PICK);
                intent.setType("image/*");
                startActivityForResult(intent, REQUEST_GALLERY);
            }
        });
    }

    // Function to Create TF Interpreter with model
    private Interpreter getTfliteInterpreter(String modelPath) {
        try {
            return new Interpreter(loadModelFile(MainActivity.this, modelPath));
        } catch (Exception e) {
            e.printStackTrace();
        }
        return null;
    }

    // To read the tflite model
    // If passs the MappedByteBuffer to Interpreter, we can analyze the model
    private MappedByteBuffer loadModelFile(Activity activity, String modelPath) throws IOException {
        AssetFileDescriptor fileDescriptor = activity.getAssets().openFd(modelPath);
        FileInputStream inputStream = new FileInputStream(fileDescriptor.getFileDescriptor());
        FileChannel fileChannel = inputStream.getChannel();
        long startOffset = fileDescriptor.getStartOffset();
        long declaredLength = fileDescriptor.getDeclaredLength();
        return fileChannel.map(FileChannel.MapMode.READ_ONLY, startOffset, declaredLength);
    }

    static final int REQUEST_IMAGE_CAPTURE = 1;
    private String mPhotoFileName = null;
    private File mPhotoFile = null;
    private Uri imageUri;

    String mCurrentPhotoPath;

    private File createImageFile() throws IOException {
        String timeStamp = new SimpleDateFormat("yyyyMMdd_HHmmss").format(new Date());
        String imageFileName = "JPEG_" + timeStamp + "_";
        File storageDir = getExternalFilesDir(Environment.DIRECTORY_PICTURES);
        File image = File.createTempFile(
                imageFileName,
                ".jpg",
                storageDir
        );

        mCurrentPhotoPath = image.getAbsolutePath();
        return image;
    }

    private void openCamera() {
        Intent cameraIntent = new Intent(MediaStore.ACTION_IMAGE_CAPTURE);

        if (cameraIntent.resolveActivity(getPackageManager()) != null) {
            File photoFile = null;

            try {
                photoFile = createImageFile();
            } catch (IOException ex) {
            }
            if (photoFile != null) {
                Uri photoURI = FileProvider.getUriForFile(Objects.requireNonNull(getApplicationContext()), "com.example.breedclassification" + ".provider", photoFile);
//                Uri photoURI = FileProvider.getUriForFile(Objects.requireNonNull(getApplicationContext()), BuildConfig.APPLICATION_ID + ".provider", photoFile);
                cameraIntent.putExtra(MediaStore.EXTRA_OUTPUT, photoURI);
                startActivityForResult(cameraIntent, REQUEST_TAKE_PHOTO);
            }
        }
    }

    @Override
    protected void onActivityResult(int requestCode, int resultCode, @Nullable Intent data) {
        super.onActivityResult(requestCode, resultCode, data);
        ImageView capturedImg = findViewById(R.id.capturedImg);
        Bitmap bitmap = null;
        try {
            switch (requestCode) {
                case REQUEST_TAKE_PHOTO: {
                    if (resultCode == RESULT_OK) {
                        File file = new File(mCurrentPhotoPath);
                        if (Build.VERSION.SDK_INT >= 29) {
                            ImageDecoder.Source source = ImageDecoder.createSource(getContentResolver(), Uri.fromFile(file));
                            try {
                                bitmap = ImageDecoder.decodeBitmap(source);
                                if (bitmap != null) {
                                    capturedImg.setImageBitmap(bitmap);
                                }
                            } catch (IOException e) {
                                e.printStackTrace();
                            }
                        } else {
                            try {
                                bitmap = MediaStore.Images.Media.getBitmap(getContentResolver(), Uri.fromFile(file));
                                if (bitmap != null) {
                                    capturedImg.setImageBitmap(bitmap);
                                }
                            } catch (IOException e) {
                                e.printStackTrace();
                            }
                        }
                    }
                    break;
                }
                case REQUEST_GALLERY: {
                    if (resultCode == RESULT_OK) {
                        Uri selectedImageUri = data.getData();
                        capturedImg.setImageURI(selectedImageUri);
                        if (Build.VERSION.SDK_INT >= 29) {
                            ImageDecoder.Source source = ImageDecoder.createSource(getContentResolver(), selectedImageUri);
                            try {
                                bitmap = ImageDecoder.decodeBitmap(source);
                            } catch (IOException e) {
                                e.printStackTrace();
                            }
                        } else {
                            try {
                                bitmap = MediaStore.Images.Media.getBitmap(getContentResolver(),selectedImageUri);
                            } catch (IOException e) {
                                e.printStackTrace();
                            }
                        }
                    } else {
                        Log.d("GAL", "GAL FAIL :  " + requestCode);
                    }
                    break;
                }
            }
        }  catch (Exception error) {
            error.printStackTrace();
        }

        float[][] modelOutput = new float[1][breedCount];
        ByteBuffer input = preprocessImg(bitmap);

        Interpreter tflite = getTfliteInterpreter("breed.tflite");
        if (tflite == null) {
            Log.d("TFLITE", "MODEL NULL!!!");
        }

        Tensor outputTensor = tflite.getOutputTensor(0);
        int[] outputShape = outputTensor.shape();
        int modelOutputClasses = outputShape[1];

        long startTime = SystemClock.uptimeMillis();
        tflite.run(input, modelOutput);
        long endTime = SystemClock.uptimeMillis();
        Log.d("TIME", "Inference Time: " + Long.toString(endTime - startTime));

        Toast.makeText(getApplicationContext(), "Result : " + String.format("%.3f", modelOutput[0][0]) + " "+ String.format("%.3f", modelOutput[0][1]) + " "+ String.format("%.3f", modelOutput[0][2]) + " "+ String.format("%.3f", modelOutput[0][3]) + " "+ String.format("%.3f", modelOutput[0][4]) + " ", Toast.LENGTH_LONG).show();

        final TextView tv_output = findViewById(R.id.tv_output);
        tv_output.setText(breeds[argmax(modelOutput)]);
    }
}

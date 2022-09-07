package com.example.petever.oobe;

import android.annotation.SuppressLint;
import android.app.Activity;
import android.content.res.AssetFileDescriptor;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.graphics.Color;
import android.graphics.ImageFormat;
import android.graphics.Rect;
import android.graphics.YuvImage;
import android.media.Image;
import android.os.SystemClock;
import android.util.Log;

import androidx.camera.core.ImageProxy;
import androidx.camera.core.internal.utils.ImageUtil;

import java.io.ByteArrayOutputStream;
import java.io.FileInputStream;
import java.io.IOException;
import java.nio.ByteBuffer;
import java.nio.ByteOrder;
import java.nio.MappedByteBuffer;
import java.nio.channels.FileChannel;

import org.tensorflow.lite.Interpreter;
import org.tensorflow.lite.Tensor;

public class MLClass {
    final static String[] breeds = {"Maltese", "Golden", "Pug", "Pome", "Poodle"};
    final static int imgsize = 299;
    final static int breedCount = 5;

    private int breedargmax(float[][] target) {
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

    private ByteBuffer preprocessImg(Bitmap bmp) {
        Bitmap bitmap = Bitmap.createScaledBitmap(bmp, imgsize, imgsize, true);
        Bitmap tmp = null;
        if (android.os.Build.VERSION.SDK_INT >= android.os.Build.VERSION_CODES.O) {
            tmp = bitmap.copy(Bitmap.Config.RGBA_F16, true);
        }
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

    // Function to Create TF Interpreter with model
    private Interpreter getTfliteInterpreter(String modelPath) {
        try {
            return new Interpreter(loadModelFile((Activity) CameraActivity.context, modelPath));
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

    private Bitmap convertImageProxyToBitmap(ImageProxy image) {
        ByteBuffer byteBuffer = image.getPlanes()[0].getBuffer();
        byteBuffer.rewind();
        byte[] bytes = new byte[byteBuffer.capacity()];
        byteBuffer.get(bytes);
        byte[] clonedBytes = bytes.clone();
        return BitmapFactory.decodeByteArray(clonedBytes, 0, clonedBytes.length);
    }

    public String runBreedClassification(ImageProxy img) {
        float[][] modelOutput = new float[1][breedCount];

        Bitmap bitmap = convertImageProxyToBitmap(img);
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

        return breeds[breedargmax(modelOutput)];
    }
}

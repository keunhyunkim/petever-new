package com.example.petever.oobe;

import android.app.Activity;
import android.content.res.AssetFileDescriptor;
import android.graphics.Bitmap;
import android.os.SystemClock;
import android.util.Log;


import com.example.petever.domain.Breed;
import com.example.petever.util.ImageUtil;

import java.io.FileInputStream;
import java.io.IOException;
import java.nio.ByteBuffer;
import java.nio.MappedByteBuffer;
import java.nio.channels.FileChannel;

import org.tensorflow.lite.Interpreter;
import org.tensorflow.lite.Tensor;

public class MLClass {
    //Breed order for Breed Classification Model : MALTESE, POME_LONG, POME_SHORT
    final static int imgsize = 299;
    final static int breedCount = 3;
    final static float breedThreshold = 0.6F;

    private int getBreedArgmax(float[][] target) {
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

    // Function to Create TF Interpreter with model
    private Interpreter getTfliteInterpreter(String modelPath, Activity activity) {
        try {
            return new Interpreter(loadModelFile(activity, modelPath));
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

    public String runBreedClassification(Bitmap bitmap, Activity activity) {
        float[][] modelOutput = new float[1][breedCount];

        ByteBuffer input = ImageUtil.preprocessImg(bitmap, imgsize);

        Interpreter tflite = getTfliteInterpreter("breed3_short.tflite", activity);
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

        int breedArgmax = getBreedArgmax(modelOutput);
        if (modelOutput[0][breedArgmax] > breedThreshold) {
            Log.d("BREED", Breed.getNameWithMLCode(breedArgmax) + " : " + modelOutput[0][breedArgmax] );
            return Breed.getNameWithMLCode(breedArgmax);
        } else {
//            Log.d("NO_BREED", "Invalid : " + modelOutput[0][breedArgmax]);
            Log.d("NO_BREED", "Invalid : " + modelOutput[0][0]+ modelOutput[0][1]+ modelOutput[0][2]);
            return "Retry";
        }

    }
}

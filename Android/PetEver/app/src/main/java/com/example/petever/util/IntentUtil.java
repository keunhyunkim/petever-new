package com.example.petever.util;

import android.content.Context;
import android.content.Intent;
import android.net.Uri;
import android.util.Log;

import com.example.petever.oobe.BreedActivity;

public class IntentUtil {
    public final static void intentBreedActivity(String TAG, Context context, Uri uri, String breed) {
        Intent intent = new Intent(context, BreedActivity.class);
        intent.putExtra("activity", TAG);
        intent.putExtra("image", uri.toString());
        intent.putExtra("breed", breed);
        context.startActivity(intent);
    }
}

package com.example.petevervoice;

import android.Manifest;
import android.app.Activity;
import android.content.Context;
import android.content.Intent;
import android.content.pm.PackageManager;
import android.os.Bundle;
import android.speech.RecognitionListener;
import android.speech.RecognizerIntent;
import android.speech.SpeechRecognizer;
import android.util.Log;
import android.widget.Toast;

import androidx.core.app.ActivityCompat;
import androidx.core.content.ContextCompat;

import java.util.ArrayList;

public class PetEverVoice {
    private static SpeechRecognizer mRecognizer = null;
    private static PetEverVoice vInstance = null;
    private static Intent voiceIntent;
    private static Context voiceCtx;
    private static String voiceResultStr = "오류";
    private static boolean isRecogDone = false;

    public PetEverVoice() {}

    public static synchronized PetEverVoice getInstance(Activity unityActivity) {
        mRecognizer = SpeechRecognizer.createSpeechRecognizer(voiceCtx);
        mRecognizer.setRecognitionListener(listener);

        if (vInstance == null) {
            return new PetEverVoice();
        }
        return vInstance;
    }

    public static void startVoiceRecognition(Activity unityActivity) {
        final int MY_PERMISSIONS_RECORD_AUDIO = 1;

        unityActivity.runOnUiThread(new Runnable() {
            @Override
            public void run() {
                isRecogDone = false;
                voiceCtx = unityActivity.getApplicationContext();
                if (voiceCtx != null) {
                    if (ContextCompat.checkSelfPermission(voiceCtx, Manifest.permission.RECORD_AUDIO)!= PackageManager.PERMISSION_GRANTED) {
                        ActivityCompat.requestPermissions(unityActivity, new String[]{Manifest.permission.RECORD_AUDIO}, MY_PERMISSIONS_RECORD_AUDIO);
                        Toast.makeText(voiceCtx, "Permission Denied", Toast.LENGTH_SHORT).show();
                    } else {
                        if (mRecognizer == null) {
                            mRecognizer = SpeechRecognizer.createSpeechRecognizer(voiceCtx);
                            mRecognizer.setRecognitionListener(listener);
                        }
                        voiceIntent = new Intent(RecognizerIntent.ACTION_RECOGNIZE_SPEECH);
                        voiceIntent.putExtra(RecognizerIntent.EXTRA_CALLING_PACKAGE, voiceCtx.getPackageName());
                        voiceIntent.putExtra(RecognizerIntent.EXTRA_LANGUAGE,"ko-KR");
                        voiceIntent.putExtra(RecognizerIntent.EXTRA_PARTIAL_RESULTS, false);
                        mRecognizer.startListening(voiceIntent);
                    }
                } else {
                    Log.d("voice", "No Context");
                }
            }
        });
    }

    private static void analyzeSpeech(String speech) {
        // Keyword
        // 산책, 밥 or 간식, 빵(개인기), 기다려, 손(개인기)
        if (speech.contains("산책") == true) {
            voiceResultStr = "산책";
            Toast.makeText(voiceCtx, "산책",
                    Toast.LENGTH_SHORT).show();
        } else if (speech.contains("밥") == true || speech.contains("간식") == true) {
            voiceResultStr = "밥";
            Toast.makeText(voiceCtx, "밥/간식",
                    Toast.LENGTH_SHORT).show();
        } else if (speech.contains("빵") == true) {
            voiceResultStr = "빵";
            Toast.makeText(voiceCtx, "빵",
                    Toast.LENGTH_SHORT).show();
        } else if (speech.contains("기다려") == true) {
            voiceResultStr = "기다려";
            Toast.makeText(voiceCtx, "기다려",
                    Toast.LENGTH_SHORT).show();
        } else if (speech.contains("손") == true) {
            voiceResultStr = "손";
            Toast.makeText(voiceCtx, "손",
                    Toast.LENGTH_SHORT).show();
        } else {
            voiceResultStr = "오류";
            Log.d("voice", "Unrecognizable command");
        }
    }

    private static String returnVoiceStr(String str) {
        return voiceResultStr;
    }

    private static RecognitionListener listener = new RecognitionListener() {
        @Override
        public void onReadyForSpeech(Bundle bundle) {
            System.out.println("onReadyForSpeech.........................");
        }

        @Override
        public void onBeginningOfSpeech() {
            Toast.makeText(voiceCtx, "지금부터 말을 해주세요!", Toast.LENGTH_SHORT).show();
        }

        @Override
        public void onRmsChanged(float v) {
            System.out.println("onRmsChanged.........................");
        }

        @Override
        public void onBufferReceived(byte[] bytes) {
            System.out.println("onBufferReceived.........................");
        }

        @Override
        public void onEndOfSpeech() {
            System.out.println("onEndOfSpeech.........................");
        }

        @Override
        public void onError(int i) {
            String errmsg = "Error";
            if (isRecogDone == true) return;
            switch (i) {
                case SpeechRecognizer.ERROR_AUDIO:
                    errmsg = "오디오 에러";
                    break;
                case SpeechRecognizer.ERROR_CLIENT:
                    errmsg = "클라이언트 에러";
                    break;
                case SpeechRecognizer.ERROR_INSUFFICIENT_PERMISSIONS:
                    errmsg = "권한 에러";
                    break;
                case SpeechRecognizer.ERROR_NETWORK:
                    errmsg = "네트워크 에러";
                    break;
                case SpeechRecognizer.ERROR_NETWORK_TIMEOUT:
                    errmsg = "네트워크 타임아웃";
                    break;
                case SpeechRecognizer.ERROR_NO_MATCH:
                    errmsg = "찾을 수 없음";
                    break;
                case SpeechRecognizer.ERROR_RECOGNIZER_BUSY:
                    errmsg = "바쁨";
                    break;
                case SpeechRecognizer.ERROR_SERVER:
                    errmsg = "서버 에러";
                    break;
                case SpeechRecognizer.ERROR_SPEECH_TIMEOUT:
                    errmsg = "말하는 시간 초과";
                    break;
                default:
                    errmsg = "알 수 없는 오류";
            }
            Toast.makeText(voiceCtx,"천천히 다시 말해주세요.", Toast.LENGTH_SHORT).show();
//            mRecognizer.stopListening();
        }

        @Override
        public void onResults(Bundle results) {

            String key = "";
            key = SpeechRecognizer.RESULTS_RECOGNITION;
            ArrayList<String> mResult = results.getStringArrayList(key);
            String[] rs = new String[mResult.size()];
            mResult.toArray(rs);
            analyzeSpeech(rs[0]);

            isRecogDone = true;
            mRecognizer.cancel();
            mRecognizer.destroy();
        }

        @Override
        public void onPartialResults(Bundle bundle) {
            System.out.println("onPartialResults.........................");
        }

        @Override
        public void onEvent(int i, Bundle bundle) {
            System.out.println("onEvent.........................");
        }
    };
}

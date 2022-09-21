package com.example.petever.domain;

import com.example.petever.R;

import java.util.HashMap;
import java.util.Map;

public enum Breed {
    // 동물 코드 + 종 코드 + 특징 코드

    // 강아지 : 10
    // 고양이 : 11

    // 포메 : 001
    // 말티즈 : 002

    // long컷 : 1
    // 곰돌이컷 : 2
    // 말티즈 기본 : 1

    MALTESE(100021, "MALTESE", R.string.breed_maltese), POME_LONG(100011, "POME_LONG", R.string.breed_pome), POME_SHORT(100012, "POME_SHORT", R.string.breed_pome);
    private int code;
    private String name;
    private int bubbleText;

    Breed(int code, String name, int bubbleText){
        this.code = code;
        this.name = name;
        this.bubbleText = bubbleText;
    }


    public int getCode() {
        return code;
    }

    public String getName() {
        return name;
    }

    public int getBubbleTextCode() {
        return bubbleText;
    }

    private static final Map<String, Breed> lookup = new HashMap<>();

    static {
        for (Breed d : Breed.values()) {
            lookup.put(d.getName(), d);
        }
    }

    public static Breed get(String name) {
        return lookup.get(name);
    }

}

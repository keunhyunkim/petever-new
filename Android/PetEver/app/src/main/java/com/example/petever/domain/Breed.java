package com.example.petever.domain;

import com.example.petever.R;

import java.util.HashMap;
import java.util.Map;

public enum Breed {
    // 동물 코드 + 종 코드 + 특징 코드

    // AABBBC
    // AA : 동물 코드 (강아지 : 10, 고양이 11)
    // BBB : 종 코드 (강아지-포메 : 001, 말티즈 : 002)
    // C : 특징 코드
    // |   동물    |        종          |       특징       |
    // ---------------------------------------------------
    // | 강아지(10) | 포메라니안(001)     |  long컷(1)       |
    // |           |                   | 곰돌이컷(2)       |
    // ---------------------------------------------------
    // | 강아지(10) | 말티즈(002)        | 기본컷(1)         |
    // ---------------------------------------------------
    // | 고양이(11) |                   |                  |
    // ---------------------------------------------------

    MALTESE(100021, "MALTESE", R.string.breed_maltese, 0), POME_LONG(100011, "POME_LONG", R.string.breed_pome, 1), POME_SHORT(100012, "POME_SHORT", R.string.breed_pome, 2);
    private int code;
    private String name;
    private int bubbleStringCode;
    private int mlCode;

    Breed(int code, String name, int bubbleStringCode, int mlCode) {
        this.code = code;
        this.name = name;
        this.bubbleStringCode = bubbleStringCode;
        this.mlCode = mlCode;
    }


    public int getCode() {
        return code;
    }

    public String getName() {
        return name;
    }

    public int getMLCode() { return mlCode; }

    public int getBubbleStringCode() {
        return bubbleStringCode;
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

    public static String getNameWithMLCode(int code) {
        for (Breed b : Breed.values()) {
            if (b.getMLCode() == code) {
                return b.getName();
            }
        }
        return "Retry";
    }
}

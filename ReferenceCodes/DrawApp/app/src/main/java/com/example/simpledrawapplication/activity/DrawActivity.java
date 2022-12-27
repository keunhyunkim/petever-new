package com.example.simpledrawapplication.activity;

import android.app.AlertDialog;
import android.content.Context;
import android.graphics.Bitmap;
import android.graphics.Canvas;
import android.graphics.Color;
import android.graphics.Paint;
import android.graphics.PorterDuff;
import android.os.Bundle;
import android.provider.MediaStore;
import android.util.AttributeSet;
import android.util.Log;
import android.view.MotionEvent;
import android.view.View;

import androidx.annotation.Nullable;
import androidx.appcompat.app.AppCompatActivity;
import androidx.constraintlayout.widget.ConstraintLayout;

import com.example.simpledrawapplication.R;
import com.example.simpledrawapplication.util.CanvasIO;
import com.google.android.material.floatingactionbutton.FloatingActionButton;

import java.io.File;
import java.io.FileNotFoundException;
import java.io.FileOutputStream;
import java.io.IOException;
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Date;
import java.util.Locale;

/**
 * Draw the Line
 * (Draw, Delete, Save, Call)
 */
public class DrawActivity extends AppCompatActivity {
    private DrawCanvas drawCanvas;
    private FloatingActionButton fbPen;             //Pen mode Button
    private FloatingActionButton fbEraser;          //Eraser mode Button
    private FloatingActionButton fbSave;            //Save Button
    private FloatingActionButton fbOpen;            //Call Button
    private FloatingActionButton fbEraseAll;         //EraseAll Button
    private ConstraintLayout canvasContainer;       //Canvas root view
    private static final String timeFormat = "yyyy-MM-dd HH:mm:ss";

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_draw);
        findId();
        canvasContainer.addView(drawCanvas);
        setOnClickListener();
    }

    /**
     * Set the View Id
     */
    private void findId() {
        canvasContainer = findViewById(R.id.lo_canvas);
        fbPen = findViewById(R.id.fb_pen);
        fbEraser = findViewById(R.id.fb_eraser);
        fbSave = findViewById(R.id.fb_save);
        fbOpen = findViewById(R.id.fb_open);
        fbEraseAll = findViewById(R.id.fb_eraseall);
        drawCanvas = new DrawCanvas(this);
    }

    private void saveBitmapToJpeg(Bitmap bitmap, String name) {

        File storage = getCacheDir();
        String fileName = name + ".jpg";

        File tempFile = new File(storage, fileName);

        try {
            tempFile.createNewFile();
            FileOutputStream out = new FileOutputStream(tempFile);
            bitmap.compress(Bitmap.CompressFormat.JPEG, 100, out);
            out.close();

        } catch (FileNotFoundException e) {
            Log.e("MyTag","FileNotFoundException : " + e.getMessage());
        } catch (IOException e) {
            Log.e("MyTag","IOException : " + e.getMessage());
        }
    }

    /**
     * OnClickListener Setting
     */
    private void setOnClickListener() {
        fbPen.setOnClickListener((v)->{
            drawCanvas.changeTool(DrawCanvas.MODE_PEN);
        });

        fbEraser.setOnClickListener((v)->{
            drawCanvas.changeTool(DrawCanvas.MODE_ERASER);
        });

        fbSave.setOnClickListener((v)->{
            drawCanvas.invalidate();

            Bitmap saveBitmap = drawCanvas.getCurrentCanvas();

            SimpleDateFormat dataFormat = new SimpleDateFormat(timeFormat, Locale.KOREA);
            String path = MediaStore.Images.Media.insertImage(getContentResolver(), saveBitmap, dataFormat.format(new Date()) + ".png", "taken by petEver");
            CanvasIO.saveBitmap(this, saveBitmap);
        });

        fbOpen.setOnClickListener((v)->{
            //Open cached canvas
            drawCanvas.init();
            drawCanvas.loadDrawImage = CanvasIO.openBitmap(this);
            drawCanvas.invalidate();

        });

        fbEraseAll.setOnClickListener((v)->{
            //Clear all Canvas
            drawCanvas.init();
            drawCanvas.invalidate();
        });
    }

    /**
     * Class for Pen
     */
    class Pen {
        public static final int STATE_START = 0;        //Pen status(Move start)
        public static final int STATE_MOVE = 1;         //Pen status(Moving now)
        float x, y;                                     //Pen Position
        int moveStatus;                                 //Current Move Status
        int color;                                      //Pen Color
        int size;                                       //Pen thickness

        public Pen(float x, float y, int moveStatus, int color, int size) {
            this.x = x;
            this.y = y;
            this.moveStatus = moveStatus;
            this.color = color;
            this.size = size;
        }

        /**
         * Check whether the current pen state is moving
         */
        public boolean isMove() {
            return moveStatus == STATE_MOVE;
        }
    }

    /**
     * Canvas view (drawing)
     */
    class DrawCanvas extends View {
        public static final int MODE_PEN = 1;                     //Pen Mode
        public static final int MODE_ERASER = 0;                  //Eraser Mode
        final int PEN_SIZE = 3;                                   //Pen size
        final int ERASER_SIZE = 30;                               //Eraser size

        ArrayList<Pen> drawCommandList;                           //The list of the drawing path
        Paint paint;                                              //Pen
        Bitmap loadDrawImage;                                     //Image which called
        int color;                                                //Current Pen Color
        int size;                                                 //Current Pen Size

        public DrawCanvas(Context context) {
            super(context);
            init();
        }

        public DrawCanvas(Context context, @Nullable AttributeSet attrs) {
            super(context, attrs);
            init();
        }

        public DrawCanvas(Context context, @Nullable AttributeSet attrs, int defStyleAttr) {
            super(context, attrs, defStyleAttr);
            init();
        }

        /**
         * Initialize the drawing
         */
        private void init() {
            paint = new Paint(Paint.ANTI_ALIAS_FLAG);
            drawCommandList = new ArrayList<>();
            loadDrawImage = null;
            color = Color.BLACK;
            size = PEN_SIZE;
        }

        /**
         * Return the image to Bitmap
         */
        public Bitmap getCurrentCanvas() {
            Bitmap bitmap = Bitmap.createBitmap(this.getWidth(), this.getHeight(), Bitmap.Config.ARGB_8888);
            Canvas canvas = new Canvas(bitmap);
            this.draw(canvas);
            return bitmap;
        }

        /**
         * Change the Tool type : Pen or Eraser
         * */
        private void changeTool(int toolMode) {
            if (toolMode == MODE_PEN) {
                this.color = Color.BLACK;
                size = PEN_SIZE;
            } else {
                this.color = Color.WHITE;
                size = ERASER_SIZE;
            }
            paint.setColor(color);
        }

        @Override
        protected void onDraw(Canvas canvas) {
            canvas.drawColor(Color.WHITE);

            if (loadDrawImage != null) {
                canvas.drawBitmap(loadDrawImage, 0, 0, null);
            }

            for (int i = 0; i < drawCommandList.size(); i++) {
                Pen p = drawCommandList.get(i);
                paint.setColor(p.color);
                paint.setStrokeWidth(p.size);

                if (p.isMove()) {
                    Pen prevP = drawCommandList.get(i - 1);
                    canvas.drawLine(prevP.x, prevP.y, p.x, p.y, paint);
                }
            }
        }

        @Override
        public boolean onTouchEvent(MotionEvent e) {
            int action = e.getAction();
            int state = action == MotionEvent.ACTION_DOWN ? Pen.STATE_START : Pen.STATE_MOVE;
            drawCommandList.add(new Pen(e.getX(), e.getY(), state, color, size));
            invalidate();
            return true;
        }
    }
}
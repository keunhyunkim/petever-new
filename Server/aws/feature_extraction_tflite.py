#tflite
import os
import cv2
import numpy as np
import tensorflow as tf
from PIL import Image
import matplotlib.pyplot as plt
from keras.applications.xception import Xception, preprocess_input
import matplotlib.image as image
from tensorflow.keras.preprocessing.image import ImageDataGenerator
import time
import sys

import dlib
from imutils import face_utils
import face_recognition

from sklearn.cluster import KMeans
from collections import Counter
import time

detector = dlib.cnn_face_detection_model_v1('./model/models/dogHeadDetector.dat')
predictor = dlib.shape_predictor('./model/models/landmarkDetector.dat')
# isModelLoaded = False

clt = KMeans(n_clusters=2)
rgb_hex = []
rgb = []
shapes = []
distance_head = []
distance_nose= []
min_head = []
min_nose = []

pome_color=[[0, 0, 0],
            [148, 88, 38],
            [198, 134, 66],
            [240, 221, 207],
            [255, 255, 255]]
yorkshir_color=[[141, 85, 36],
                [198, 134, 66],
                [224, 172, 105],
                [241, 194, 125],
                [255, 219, 172]]
pug_color=[[0, 0, 0],
           [198, 158, 125],
           [244, 237, 222]]

color_table = [pome_color, yorkshir_color, pug_color]

maltese_color = [255, 255, 255]
retriever_color = [[213,136,79],
                   [207,176,131]]
poodle_color = [[151, 148, 141],
                [172, 120, 91], 
                [235, 205, 176],   
                [255, 255, 255]]


# load the image and convert the color BGR to RGB
def load_image (img_path):
    # filename, ext = os.path.splitext(os.path.basename(img_path))
    img = cv2.imread(img_path)
    img = cv2.cvtColor(img, cv2.COLOR_BGR2RGB)
    
    return img


# Detect the dog's face and five landmarks.
def detect_dog_face (img):
    dets = detector(img, upsample_num_times=1)
    img_result = img.copy()

    # face detect
    for i, d in enumerate(dets): 
        x1, y1 = d.rect.left(), d.rect.top()
        x2, y2 = d.rect.right(), d.rect.bottom()    

    # nose, eyes, ears etc detect
    xy = np.empty((0,2), int)
    for i, d in enumerate(dets):
        shape = predictor(img, d.rect)
        shpae = face_utils.shape_to_np(shape)    
        for i, p in enumerate(shpae):
            shapes.append(shpae)
            xy = np.append(xy, np.array([p]), axis=0)
        
    img_out = cv2.cvtColor(img_result, cv2.COLOR_RGB2BGR)
    
    # plt.figure(figsize=(10,10))
   
    return img_out, x1, x2, y1, y2, xy


# crop image to find the color for pome, yorkshir, pug
def crop_image_1(img, x1, x2, y1, y2, xy):
    # boxes of forehead (coordinate X:(5+0)/2 ~ (0+2)/2, Y:0~2)
    crop_forehead_1 = img[xy[0][1]:xy[5][1]-(xy[3][1]-xy[5][1]), (x1+xy[0][0]-xy[5][0]):int((xy[0][0]+xy[5][0])/2)]
    crop_forehead_2 = img[xy[0][1]:xy[2][1], int((xy[0][0]+xy[5][0])/2):int((xy[0][0]+xy[2][0])/2)]
    crop_forehead_3 = img[xy[0][1]:xy[2][1]-(xy[3][1]-xy[2][1]), int((xy[0][0]+xy[2][0])/2):xy[2][0]+(xy[2][0]-xy[0][0])]

    # boxes of nose (coordinate X:4~(5+3)/2, Y: (5+3)/2 ~ 5+2*(5-3))
    crop_nose_1 = img[int((xy[5][1]+xy[3][1])/2):(xy[5][1]+2*(xy[3][1]-xy[5][1])), int((x1+xy[4][0])/2):int((xy[5][0]+xy[3][0])/2)]
    crop_nose_2 = img[int((xy[5][1]+xy[3][1])/2):(xy[5][1]+2*(xy[3][1]-xy[5][1])), int((xy[2][0]+xy[3][0])/2):int((xy[1][0]+x2)/2)]
   
    return crop_forehead_1, crop_forehead_2, crop_forehead_3, crop_nose_1, crop_nose_2


# crop image to find the color for poodle
def crop_image_2(img, x1, x2, y1, y2, xy):
    # boxes of forehead (coordinate X:(5+0)/2 ~ (0+2)/2, Y:0~2)
    crop_forehead = img[xy[0][1]:xy[2][1], int((xy[0][0]+xy[5][0])/2):int((xy[0][0]+xy[2][0])/2)]
   
    return crop_forehead


# Extract two colors for each box and Select a color with a large ratio
def palette_perc(k_cluster):
    width = 300
    palette = np.zeros((50, width, 3), np.uint8)
    
    n_pixels = len(k_cluster.labels_)
    counter = Counter(k_cluster.labels_) # count how many pixels per cluster
    perc = {}
    for i in counter:
        perc[i] = np.round(counter[i]/n_pixels, 2)
    perc = dict(sorted(perc.items()))
    
    if perc[0] > perc[1]:
#         rgb_hex.append(rgb_to_hex(k_cluster.cluster_centers_[0][0],k_cluster.cluster_centers_[0][1], k_cluster.cluster_centers_[0][2]))
        rgb.extend([k_cluster.cluster_centers_[0][0],k_cluster.cluster_centers_[0][1], k_cluster.cluster_centers_[0][2]])
        
    else:
#         rgb_hex.append(rgb_to_hex(k_cluster.cluster_centers_[1][0],k_cluster.cluster_centers_[1][1], k_cluster.cluster_centers_[1][2]))
        rgb.extend([k_cluster.cluster_centers_[1][0],k_cluster.cluster_centers_[1][1], k_cluster.cluster_centers_[1][2]])
    
    step = 0    
    for idx, centers in enumerate(k_cluster.cluster_centers_): 
        palette[:, step:int(step + perc[idx]*width+1), :] = centers
        step += int(perc[idx]*width+1)
        
    return palette

# Calculate distance between the specified color list and rgb of boxes for pome, yorkshir, pug
def calc_distance_1(rgb, color):
    rgb = list_chunk(rgb, 3)
    for j in range(0, 3):
        for i in range(len(color)):
            distance = (color[i][0] - rgb[j][0])**2+(color[i][1] - rgb[j][1])**2+(color[i][2] - rgb[j][2])**2
            distance_head.append(distance)    

    for j in range(3, 5):
        for i in range(len(color)):
            distance = (color[i][0] - rgb[j][0])**2+(color[i][1] - rgb[j][1])**2+(color[i][2] - rgb[j][2])**2
            distance_nose.append(distance)
            
    # cut by the number of colors and change to a multidimensional list
    distance_head_h = list_chunk(distance_head, len(color))
    distance_nose_h = list_chunk(distance_nose, len(color))
    
    # save the index of the closest color to the color list
    for i in range (0, 3):
        min_head.append(np.argmin(distance_head_h[i]))
    for i in range (0, 2):
        min_nose.append(np.argmin(distance_nose_h[i]))
    
    return min_head, min_nose


# Calculate distance between the specified color list and rgb of boxes for poodle
def calc_distance_2(rgb, color):
    for i in range(len(color)):
        distance = (color[i][0] - rgb[0])**2+(color[i][1] - rgb[1])**2+(color[i][2] - rgb[2])**2
        distance_head.append(distance)    

    # cut by the number of colors and change to a multidimensional list
    distance_head_h = list_chunk(distance_head, len(color))
    
    # save the index of the closest color to the color list
    min_head.append(np.argmin(distance_head_h))
    
    return min_head

# select two colors for each boxes  
def select_color (crop_image):
    clt_1 = clt.fit(crop_image.reshape(-1, 3))
    palette_perc(clt_1)
   
# Change the rgb to hex
def rgb_to_hex(r, g, b):
    r, g, b = int(r), int(g), int(b)
    return hex(r)[2:].zfill(2) + hex(g)[2:].zfill(2) + hex(b)[2:].zfill(2)

def list_chunk(lst, n):
    return [lst[i:i+n] for i in range(0, len(lst), n)]
    

def extract_color(filename, species):
    img = load_image (filename)
    
    img_out, x1, x2, y1, y2, xy = detect_dog_face(img)
    if ("yorkshir" in species) or ("pug" in species) or ("Pome" in species) :
    # if species == "pug":
        if ("Pome" in species):
            idx = 0
        elif species == "yorkshir":
            idx = 1
        else:
            idx = 2        
        crop_forehead_1, crop_forehead_2, crop_forehead_3, crop_nose_1, crop_nose_2 = crop_image_1(img, x1, x2, y1, y2, xy)
        select_color(crop_forehead_1)
        select_color(crop_forehead_2)
        select_color(crop_forehead_3)
        select_color(crop_nose_1)
        select_color(crop_nose_2)
        min_head, min_nose = calc_distance_1(rgb, color_table[idx])        
        # Select the median value among the 3 boxes
        forehead = color_table[idx][int(np.median(min_head))]
        # Select the brighter color
        nose = color_table[idx][max(min_nose)]
        
    elif ("Maltese" in species) :
        forehead = maltese_color
        nose = maltese_color
        print(forehead)
    
    elif ("retriever" in species):
        forehead = retriever_color[0]
        nose = retriever_color[1]
    
    elif ("poodle" in species):
        crop_forehead = crop_image_2(img, x1, x2, y1, y2, xy)
        select_color(crop_forehead)
        min_head = calc_distance_2(rgb, poodle_color)
        forehead = poodle_color[int(np.median(min_head))]
        nose = forehead

    print(rgb_to_hex(forehead[0], forehead[1], forehead[2]), rgb_to_hex(nose[0], nose[1], nose[2]), species)
    return rgb_to_hex(forehead[0], forehead[1], forehead[2]), rgb_to_hex(nose[0], nose[1], nose[2]), species


def breed_main(filename):
    start = time.time()
    img_path = filename
    isModelLoaded = False
    if isModelLoaded == False:
        interpreter = tf.lite.Interpreter(model_path = "./dog_breed_10.tflite")
        interpreter.allocate_tensors()
        isModelLoaded = True

    breed_list = ['Maltese', 'Pomeranian_long', 'Pomeranian_short', 'Chihuahua', 'Shih-Tzu', 'beagle', 'Yorkshire_terrier', 'golden_retriever', 'pug', 'toy_poodle']

    input_details = interpreter.get_input_details()
    input_shape = input_details[0]['shape']
    input_type = input_details[0]['dtype']

    output_details = interpreter.get_output_details()
    output_shape = output_details[0]['shape']
    output_type = output_details[0]['dtype']


    img = Image.open(img_path)
    img = img.convert('RGB')
    img = img.resize((299, 299))
    img.save(img_path)
    # predict
    img = image.imread(img_path)    
    img = preprocess_input(img)
    img = np.expand_dims(img, axis = 0)
     
    interpreter.set_tensor(input_details[0]['index'], img)
    interpreter.invoke()

    output_data = interpreter.get_tensor(output_details[0]['index']) 
    m = np.argmax(output_data)
     
    # print(breed_list[m])
    return extract_color(filename, breed_list[m])
    # print("time@:", time.time() - start)

if __name__ == '__main__':
    breed_main(sys.argv[1])
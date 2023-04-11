#h5
import sys
import numpy as np
import tensorflow as tf
from PIL import Image
import matplotlib.pyplot as plt
import cv2
from keras.applications.xception import Xception, preprocess_input
import matplotlib.image as image
from tensorflow.keras.preprocessing.image import ImageDataGenerator

#model load(if model is not loaded)
isModelLoaded = False
if isModelLoaded == False:
    model = tf.keras.models.load_model("./breed_10_model.h5")
    isModelLoaded = True

breed_list = ['Maltese', 'Pomeranian_long', 'Pomeranian_short', 'Chihuahua', 'Shih-Tzu', 'beagle', 'Yorkshire_terrier', 'golden_retriever', 'pug', 'toy_poodle']
label_maps = {}
label_maps_rev = {}
for i, v in enumerate(breed_list):
    label_maps.update({v: i})
    label_maps_rev.update({i : v})
    
def download_and_predict(filename):

    img = Image.open(filename)
    img = img.convert('RGB')
    img = img.resize((299, 299))
    img.save(filename)
    # predict
    img = image.imread(filename)
    img = preprocess_input(img)
    probs = model.predict(np.expand_dims(img, axis=0))
#     for idx in probs.argsort()[0][::-1][:5]:
#         print("{:.2f}%".format(probs[0][idx]*100), "\t", label_maps_rev[idx].split("-")[-1])
    pr = list(probs[0])
    label_index = pr.index(max(pr))
    print(label_maps_rev[label_index])

if __name__ == '__main__':
    download_and_predict(sys.argv[1])

import cv2
import os
from pascal_voc_writer import Writer

image_file_path = './input/pome_short_'
annotation_file_path = './annotation/pome_short_'
breed_name = 'pomeranian'

#print(os.getcwd())
#파일 읽어오는 부분
for i in range(1,5):
    im = cv2.imread(image_file_path + str(i) + '.jpg')
    #print(type(im))
    #print(im.shape)
    #print(type(im.shape))
	
#크기 설정하는 부분
    height, width, depth = im.shape
    #print(height, width, depth)
#xml 파일로 annotation 저장
    writer = Writer(annotation_file_path + str(i) + '.jpg', width, height, depth)
    writer.addObject(breed_name ,193, 47, 340, 150)

    writer.save(annotation_file_path + str(i))


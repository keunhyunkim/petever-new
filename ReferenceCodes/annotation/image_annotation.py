import cv2
import os
from pascal_voc_writer import Writer

#print(os.getcwd())
#파일 읽어오는 부분
for i in range(1,5):
    im = cv2.imread('.\data\oxford_pomeranian\pomeranian_' + str(i) + '.jpg')
    #print(type(im))
    #print(im.shape)
    #print(type(im.shape))
#크기 설정하는 부분
    height, width, depth = im.shape
    #print(height, width, depth)
#xml 파일로 annotation 저장
    writer = Writer('.\data\oxford_pomeranian\pomeranian_' + str(i) + '.jpg', width, height, depth)
    writer.addObject('pomeranian',193, 47, 340, 150)

    writer.save('.\data\pomeranian_' + str(i) + '.xml')


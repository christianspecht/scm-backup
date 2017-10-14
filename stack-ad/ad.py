from PIL import Image, ImageFont, ImageDraw, ImageEnhance
import os

# create dir for images
dir = 'img'
if not os.path.exists(dir):
    os.makedirs(dir)


# empty image
img = Image.new('RGB', (600,500), 'white')

# blue box
dr = ImageDraw.Draw(img)
dr.rectangle(((0, 250), (600, 350)), fill='#239FE6')


# 2px black border
dr.rectangle(((0, 0), (599, 499)), outline='black')
dr.rectangle(((1, 1), (598, 498)), outline='black')


img.save(dir + '/ad.png')



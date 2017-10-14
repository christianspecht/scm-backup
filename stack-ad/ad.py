from PIL import Image, ImageFont, ImageDraw, ImageEnhance
import os

# create dir for images
dir = 'img'
if not os.path.exists(dir):
    os.makedirs(dir)


img = Image.new('RGB', (600,500), 'white')

img.save(dir + '/ad.png')



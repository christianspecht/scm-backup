from PIL import Image, ImageFont, ImageDraw, ImageEnhance
import os

# create dir for images
dir = 'img'
if not os.path.exists(dir):
    os.makedirs(dir)


# empty image
img = Image.new('RGBA', (600,500), 'white')


# logo
logo = Image.open('../img/logo200x200.png').convert("RGBA")
img.paste(logo, (35,35), logo)


# blue box
dr = ImageDraw.Draw(img)
dr.rectangle(((0, 250), (600, 350)), fill='#239FE6')


# 2px black border
dr.rectangle(((0, 0), (599, 499)), outline='black')
dr.rectangle(((1, 1), (598, 498)), outline='black')


img.save(dir + '/ad.png')



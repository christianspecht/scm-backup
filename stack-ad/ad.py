from PIL import Image, ImageFont, ImageDraw, ImageEnhance
import os

# create dir for images
dir = 'img'
if not os.path.exists(dir):
    os.makedirs(dir)


# empty image
img = Image.new('RGBA', (600,500), 'white')
dr = ImageDraw.Draw(img)


# logo
logo = Image.open('../img/logo200x200.png').convert("RGBA")
img.paste(logo, (35,35), logo)


# logo text
font_logo = ImageFont.truetype('calibrib.ttf', 64)
dr.text((250,100), 'SCM Backup', font=font_logo, fill='black')


# blue box
dr.rectangle(((0, 250), (600, 350)), fill='#239FE6')


# 2px black border
dr.rectangle(((0, 0), (599, 499)), outline='black')
dr.rectangle(((1, 1), (598, 498)), outline='black')


img.save(dir + '/ad.png')



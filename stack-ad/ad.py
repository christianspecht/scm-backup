from PIL import Image, ImageFont, ImageDraw, ImageEnhance
import os


# create dir for images
dir = 'img'
if not os.path.exists(dir):
    os.makedirs(dir)


# define fonts
font_logo = ImageFont.truetype('calibrib.ttf', 64)
font = ImageFont.truetype('calibri.ttf', 36)


# empty image
img = Image.new('RGBA', (600,500), 'white')
dr = ImageDraw.Draw(img)


# logo
logo = Image.open('../img/logo200x200.png').convert("RGBA")
img.paste(logo, (35,35), logo)


# logo text
dr.text((250,100), 'SCM Backup', font=font_logo, fill='black')


# blue box
dr.rectangle(((0, 250), (600, 350)), fill='#239FE6')


# "Contribute" text
dr.text((100,435), 'Contribute on GitHub', font=font, fill='black')

# GitHub logo (download from https://github.com/logos, put into this directory)
ghlogo = Image.open('GitHub-Mark-64px.png')
img.paste(ghlogo, (430,420), ghlogo)



# 2px black border
dr.rectangle(((0, 0), (599, 499)), outline='black')
dr.rectangle(((1, 1), (598, 498)), outline='black')


img.save(dir + '/ad.png')



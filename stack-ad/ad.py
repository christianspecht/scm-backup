from PIL import Image, ImageFont, ImageDraw, ImageEnhance


# define fonts
font_logo = ImageFont.truetype('calibrib.ttf', 64)
font = ImageFont.truetype('calibri.ttf', 30)


# empty image
img = Image.new('RGBA', (600,500), 'white')
dr = ImageDraw.Draw(img)


# logo
logo = Image.open('../img/logo128x128.png').convert("RGBA")
img.paste(logo, (35,25), logo)


# logo text
dr.text((220,65), 'SCM Backup', font=font_logo, fill='black')


# blue box
dr.rectangle(((0, 170), (600, 250)), fill='#239FE6')
dr.text((75,180), 'Makes offline backups of your cloud', font=font, fill='white')
dr.text((75,210), 'hosted source code repositories', font=font, fill='white')


# "Contribute" text
dr.text((100,435), 'Contribute on GitHub', font=font, fill='black')

# GitHub logo (download from https://github.com/logos, put into this directory)
ghlogo = Image.open('GitHub-Mark-64px.png')
img.paste(ghlogo, (430,420), ghlogo)



# 2px black border
dr.rectangle(((0, 0), (599, 499)), outline='black')
dr.rectangle(((1, 1), (598, 498)), outline='black')


img.save('ad.png')



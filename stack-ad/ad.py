from PIL import Image, ImageFont, ImageDraw, ImageEnhance


# define fonts
font_logo = ImageFont.truetype('calibrib.ttf', 64)
font_cont = ImageFont.truetype('calibrib.ttf', 40)
font = ImageFont.truetype('calibri.ttf', 30)


# empty image
img = Image.new('RGBA', (600,500), 'white')
dr = ImageDraw.Draw(img)


# logo
logo = Image.open('../img/logo128x128.png').convert("RGBA")
img.paste(logo, (35,35), logo)


# logo text
dr.text((220,75), 'SCM Backup', font=font_logo, fill='black')


# blue box
dr.rectangle(((0, 190), (600, 270)), fill='#239FE6')
dr.text((75,200), 'Makes offline backups of your cloud', font=font, fill='white')
dr.text((75,230), 'hosted source code repositories', font=font, fill='white')


# "Contribute" text
dr.text((75,310), 'Help us implement support (in C#) for', font=font, fill='black')
dr.text((75,340), 'backing up from more hosting sites!', font=font, fill='black')
dr.text((50,430), 'Contribute on GitHub', font=font_cont, fill='black')


# GitHub logo (download from https://github.com/logos, put into this directory)
ghlogo = Image.open('GitHub-Mark-64px.png')
img.paste(ghlogo, (450,420), ghlogo)



# 2px black border
dr.rectangle(((0, 0), (599, 499)), outline='black')
dr.rectangle(((1, 1), (598, 498)), outline='black')


img.save('ad.png')



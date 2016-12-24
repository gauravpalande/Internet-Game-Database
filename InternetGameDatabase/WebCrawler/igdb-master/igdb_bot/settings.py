# Scrapy settings for igdb project
import os
from PIL import Image
path = os.getcwd().split('WebCrawler')[0]

SPIDER_MODULES = ['igdb_bot.spiders']
NEWSPIDER_MODULE = 'igdb_bot.spiders'
DEFAULT_ITEM_CLASS = 'igdb_bot.items.Website'

ITEM_PIPELINES = {'igdb_bot.pipelines.FilterWordsPipeline': 1, 'scrapy.pipelines.images.ImagesPipeline': 1}
IMAGES_STORE = path + '//Images//Games'
IMAGES_EXPIRES = 90 #days
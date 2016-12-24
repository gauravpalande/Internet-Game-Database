from scrapy.item import Item, Field


class Website(Item):

    name = Field()
    description = Field()
    image_urls = Field()
    images = Field()
    rating = Field()
    genre = Field()
    platform = Field()
    num_of_players = Field()
    

from scrapy.spiders import Spider
from scrapy.selector import Selector
from scrapy.http.request import Request
from igdb_bot.items import Website
import urlparse

class igdbSpider(Spider):
    name = "igdb"
    allowed_domains = ["metacritic.com", "boardgamegeek.com"]
   #    Starting at the main game page to crawl through
    start_urls = [
        "http://www.metacritic.com/game",
        "https://boardgamegeek.com/browse/boardgame",
        #"https://boardgamegeek.com/blogpost/47806/app-news-forbidden-desert-coming-ipad-sentinels-mu",
        #"http://www.metacritic.com/game/pc/starcraft-ii-legacy-of-the-void","http://boardgaming.com/games/board-games/caverna-the-cave-farmers", "https://boardgamegeek.com/boardgame/121921/robinson-crusoe-adventures-cursed-island", "https://boardgamegeek.com/boardgame/132531/roll-galaxy",
    ]
    
    #   Check which site to parse
    def parse(self, response):
        site = response.url
        if site == "http://www.metacritic.com/game":
            yield Request(site, callback=self.parse_videogames)
        else:
            yield Request(site, callback=self.parse_boardgames)
            #yield Request(site, callback=self.parse_boardgames)
    
    #   Crawl through each individual table top game than check for a next page
    def parse_boardgames(self, response):
        asdf = Selector(response)
        print asdf.xpath('//div[@id="main_content"]/div[@id="collection"]/table//tr[@id="row_"]/td[@class="collection_thumbnail"]/a/@href')
        for site in response.xpath('//div[@id="main_content"]/div[@id="collection"]/table//tr[@id="row_"]/td[@class="collection_thumbnail"]/a/@href'):
            url = response.urljoin(site.extract())
            print url
            yield Request(url, callback=self.parse_ttcontents)
       # next_page = response.xpath('//div[@id="main_content"]/form/div/div/a[@title="next page"]/@href')
       # print next_page
       # if next_page:
       #     url = response.urljoin(next_page.extract())
       #     yield Request(url, callback=self.parse_boardgames)
            
    #   Crawl through the contents of the table top games
    def parse_ttcontents(self, response):
        sel = Selector(response)
        items = []
        item = Website()
        print sel.xpath('//div[@id="module_2"]//div[@class="mt5"]/a/img/@src')
        item['name'] = sel.xpath('//h1[@class="geekitem_title"]/a/span/text()').extract() or sel.xpath('/html/body/div[1]/div[3]/div/div[6]/div[1]/div[1]/div[2]/h1/text()').extract() or ['']
        item['description'] = sel.xpath('//div[@id="module_3"]//div[@id="editdesc"]/p/text()').extract() or ['None']
        item['image_urls'] = [urlparse.urljoin(response.url, u) for u in sel.xpath('//div[@id="module_2"]//div[@class="mt5"]/a/img/@src').extract()] or [''] #sel.xpath('//div[@id="module_2"]//div[@class="mt5"]/a/img/@src').extract()
        item['rating'] = sel.xpath('//span[@property="v:average"]/text()').extract() or ['']
        item['genre'] = sel.xpath('normalize-space(//table[@class="geekitem_infotable"]/tbody/tr[13]/td[2]/div/div[1]/a/text())').extract() or ['']
        item['platform'] = ['Board Game']
        item['num_of_players'] = sel.xpath('normalize-space(//div[@id="edit_players"]/div[2]/text())').extract() or ['']
        items.append(item)
          
        return items      
        
    #   Crawl through all the links in the Browse by platform panel on the main game page
    def parse_videogames(self, response):
        for site in response.xpath('//div[@class="platforms current_platforms"]/div/ul/li/div/span/a/@href'):
             #joins the href paths with the main url to create a url request to a specific page
             url = response.urljoin(site.extract())
             print url
             #  Check if a link links to a current generation platforms or legacy (ps1, gamecube, wii, etc.)
             if url == "http://www.metacritic.com/game/legacy":
                #   Request the current page/link and call the parser to use
                 yield Request(url, callback=self.parse_legacy)
             else:
                 yield Request(url, callback=self.parse_currentgen)
    
                 
    #   Crawl through the current top games of current generation platforms       
    def parse_currentgen(self, response):
        for site in response.xpath('//ol[@class="list_products list_product_summaries"]/li/div/div[@class="product_basics stats"]/div/div/a/@href'):
            url = response.urljoin(site.extract())
            print url
            yield Request(url, callback=self.parse_vgcontents)
    
    
    #   Crawl through the page with the legacy platforms to aquire the individual platforms
    def parse_legacy(self, response):
        for site in response.xpath('//div[@class="module products_module list_product_titles_ranked_module "]/div[@class="foot"]/div/div/span/a/@href'):
            url = response.urljoin(site.extract())
            print url
            yield Request(url, callback=self.parse_legacy_page)
    
            
    #   Crawl through the list of games for the current legacy platform      
    def parse_legacy_page(self, response):
        for site in response.xpath('//div[@class="body"]/div[@class="body_wrap"]/div/ol/li/div/div[@class="basic_stat product_title"]/a/@href'):
            url = response.urljoin(site.extract())
            print url
            yield Request(url, callback=self.parse_vgcontents)      
   
   
   #    Scrape the specified information from the current video game page. 
   #    More items can be added by adding a new Field in items.py before adding it here. 
   #    ***Reminder*** Use firefox/firebug to find xpaths.
   #    ***Future Use*** Check out http://doc.scrapy.org/en/latest/topics/media-pipeline.html#topics-media-pipeline-enabling to download images.
    def parse_vgcontents(self, response):
        sel = Selector(response)
        items = []
        item = Website()
        item['name'] = sel.xpath('normalize-space(//h1[@class="product_title"]/a/span/text())').extract()
        item['description'] = sel.xpath('//li[@class="summary_detail product_summary"]/span[@class="data"]/span[@class="inline_expand_collapse inline_collapsed"]/span[2]/text()').extract() or sel.xpath('//li[@class="summary_detail product_summary"]/span[@class="data"]/span/text()').extract() or ['None']
        item['image_urls'] = sel.xpath('//div[@class="module_wrap has_image has_large_image"]/div/div[@class="product_image large_image"]/img/@src').extract() or ['']
        item['rating'] = sel.xpath('//span[@itemprop="ratingValue"]/text()').extract() or ['']
        item['genre'] = sel.xpath('//div[@class="details side_details"]/ul/li/span[@itemprop="genre"]/text()').extract() or ['']
        item['platform'] = sel.xpath('normalize-space(//span[@itemprop="device"]/text())').extract() or ['']#or sel.xpath('normalize-space(//h1[@class="product_title"]/span/a/span/text())').extract()
        item['num_of_players'] = sel.xpath('/html/body/div[1]/div[2]/div[1]/div[2]/div/div/div/div/div/div[1]/div/div[1]/div[3]/div/div[2]/div[2]/div[2]/ul/li[3]/span[2]/text()').extract() or ['']
        items.append(item)
          
        return items
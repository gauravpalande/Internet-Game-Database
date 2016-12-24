======
igdb
======

This is a Scrapy project to scrape websites from public web directories.

This project is only meant for educational purposes.

Items
=====

The items scraped by this project are websites, and the item is defined in the
class::

    igdb_bot.items.Website

See the source code for more details.

Spiders
=======

This project contains one spider called ``igdb`` that you can see by running::

    scrapy list

Spider: igdb
------------

The ``igdb`` spider scrapes the Open Directory Project (metacritic.com/game), and it's
based on the dmoz spider described in the `Scrapy tutorial`_

.. _Scrapy tutorial: http://doc.scrapy.org/en/latest/intro/tutorial.html

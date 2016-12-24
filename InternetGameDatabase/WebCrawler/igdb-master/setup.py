from setuptools import setup, find_packages

setup(
    name='igdb',
    version='1.0',
    packages=find_packages(),
    entry_points={'scrapy': ['settings = igdb_bot.settings']},
)

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Xml;
using System.IO;
using System.Text;
using System.Reflection;
using System.Collections;

namespace InternetGameDatabase.Models
{
    public class InternetGameDatabaseInitializer : /*DropCreateDatabaseAlways<GameContext>*/DropCreateDatabaseIfModelChanges<GameContext>
    {
        //Genre table to keep track of all genres
        private static Hashtable genres = new Hashtable();
        //Platform table to keep track of all platforms
        private static Hashtable platforms = new Hashtable();

        static int debug = 0;
        static string titles = "";
        protected override void Seed(GameContext context)
        {
            string file = "\\WebCrawler\\igdb-master\\bte58.xml";
            string path = System.AppDomain.CurrentDomain.BaseDirectory + file;
            iniGenreHash(GetGenres());
            iniPlatformHash(GetPlatforms());
            //GetPlatforms().ForEach(plat => context.Platforms.Add(plat));
            //getgames2(path+file).ForEach(game => context.Games.Add(game));

            foreach (Game game in GetGames(path))
            {
                context.Games.Add(game);
            }
        }

        private static List<Game> GetGames(string fileName)
        {
            int curGenreID = 6, curPlatID = 3;    //Temp to keep track of genre id and platform id for now, Fix and DELETE ME
            List<Game> gameList = new List<Game>();
            using (FileStream fileSteam = File.OpenRead(fileName))
            {
                XmlReaderSettings settings = new XmlReaderSettings();
                settings.ConformanceLevel = ConformanceLevel.Document;
                using (XmlReader reader = XmlReader.Create(fileSteam, settings))
                {
                    //Iterates sequentally through each element in the xml file
                    while (reader.Read())
                    {
                        debug++;
                        if (debug == 76)
                        {
                            debug = debug + 0;
                        }
                        //Must read to a new item element each time before grabbing information
                        if (!reader.Name.ToString().Equals("item"))
                        {
                            reader.ReadToFollowing("item");
                        }
                        if (reader.IsStartElement())
                        {
                            Game game = new Game();
                            Genre genre = new Genre();
                            Platform plat = new Platform();
                            reader.ReadToFollowing("rating");
                            double rating = GetRateValue(reader);
                            reader.ReadToFollowing("name");
                            string name = GetStringValue(reader);
                            titles = name;
                            reader.ReadToFollowing("images");
                            string imageurl = GetImagePath(reader);
                            reader.ReadToFollowing("platform");
                            plat.PlatformName = GetStringValue(reader);
                            //Check if current platform already exists
                            if (platforms.ContainsKey(plat.PlatformName))
                            {
                                //Get the existing platform and assign it
                                Platform p = (Platform)platforms[plat.PlatformName];
                                plat = p;
                            }
                            //If not assign an ID and add to table
                            else
                            {
                                plat.PlatformID = ++curPlatID;
                                platforms[plat.PlatformName] = plat;

                            }

                            reader.ReadToFollowing("num_of_players");
                            string players = GetStringValue(reader);
                            reader.ReadToFollowing("genre");
                            genre.GenreName = GetGenreValue(reader);
                            //Check if current genre already exists
                            if (genres.ContainsKey(genre.GenreName))
                            {
                                //Get the existing genre and assign it
                                Genre g = (Genre)genres[genre.GenreName];
                                genre = g;
                            }
                            //If not assign an ID and add to table
                            else
                            {
                                genre.GenreID = ++curGenreID;
                                genres[genre.GenreName] = genre;
                            }
                            reader.ReadToFollowing("description");
                            string desc = GetDescValue(reader);

                            //gameList.Any(g => g.GameName == game.GameName);
                            //Check if game already exists in the list. *May want to do a better search method for performance increase*
                            Game temp = gameList.Find(g => g.GameName.ToLower() == name.ToLower());
                            if (temp == null)
                            {
                                game.GameName = name;
                                game.GameRating = rating;
                                game.GenreID = genre.GenreID;
                                game.Genre = genre;
                                //Create new list of platforms and add current platform to it
                                game.Platforms = new List<Platform>() { plat };
                                game.ImagePath = imageurl;
                                game.GameDescription = desc;
                                gameList.Add(game);
                            }
                            else
                            {
                                //check if platform exists already if not add to list of platforms *May not need check, since duplicate game should have a new platform
                                if (!temp.Platforms.Any(p => p.PlatformName == plat.PlatformName))
                                {
                                    temp.Platforms.Add(plat);
                                }

                            }

                        }
                    }
                }
            }
            return gameList;
        }

        //Returns the rating as a double by extracting the information from the value element
        private static double GetRateValue(XmlReader reader)
        {
            double rate = 0;
            reader.ReadToFollowing("value");
            rate = reader.ReadElementContentAsDouble();
            if (rate < 10.0)
            {
                rate = rate * 10.0;
            }
            return rate;
        }

        //Returns a specific element value as a string by extracting the information from the value element
        private static string GetStringValue(XmlReader reader)
        {
            string text = "";
            reader.ReadToFollowing("value");
            text = reader.ReadElementContentAsString();
            return text;

        }

        private static string GetImagePath(XmlReader reader)
        {
            //string path = System.AppDomain.CurrentDomain.BaseDirectory;
            string url = "";
            reader.ReadToFollowing("path");
            url = reader.ReadElementContentAsString();
            return url;
        }

        private static string GetDescValue(XmlReader reader)
        {
            string endElement = "";
            string text = "";
            reader.ReadToFollowing("value");
            while (!endElement.Equals("description") && !endElement.Equals("item"))
            {
                text += reader.ReadElementContentAsString();
                text.Trim();
                text += " ";
                reader.ReadToNextSibling("value");
                endElement = reader.Name.ToString();
            }
            return text;
        }

        private static string GetGenreValue(XmlReader reader)
        {
            string genre = "";
            reader.ReadToFollowing("value");
            genre = reader.ReadElementContentAsString();
            if (genre.Equals(""))
            {
                genre = "Miscellaneous";
            }
            return genre;
        }

        private static void iniGenreHash(List<Genre> list)
        {
            //for each genre in the list, create the key as a string and assign the genre object to it 
            foreach (Genre genre in list)
            {
                genres[genre.GenreName] = genre;
            }
        }

        private static void iniPlatformHash(List<Platform> list)
        {
            foreach (Platform plat in list)
            {
                //for each genre in the list, create the key as a string and assign the platform object to it
                platforms[plat.PlatformName] = plat;
            }
        }



        private static List<Genre> GetGenres()
        {
            var genres = new List<Genre>
            {
                new Genre
                {
                    GenreID = 1,
                    GenreName = "Side Scroller",
                    GenreDescription = "Old school platformer, possibly with shooting or other item modifier."
                },
                new Genre
                {
                    GenreID = 2,
                    GenreName = "Turn-Based Strategy",
                    GenreDescription = "Strategy game where opponents alternate managing resources and disrupting other entities."
                },
                new Genre
                {
                    GenreID = 3,
                    GenreName = "Real-Time Strategy",
                    GenreDescription = "Strategy game where opponents constantly manage resources and disrupt other entities."
                },
                new Genre
                {
                    GenreID = 4,
                    GenreName = "Action",
                    GenreDescription = "ya know, explosions and stuff."
                },
                new Genre
                {
                    GenreID = 5,
                    GenreName = "Sports",
                    GenreDescription = "what's a sport? "
                },
                new Genre
                {
                    GenreID = 6,
                    GenreName = "Role-Playing",
                    GenreDescription = "stuff that has stats and learning new stuff"
                }

            };
            return genres;
        }

        private static List<Platform> GetPlatforms()
        {
            var platforms = new List<Platform>
            {
                new Platform
                {
                    PlatformID = 1,
                    PlatformName = "PC",
                    PlatformDescription = "Mouse and keyboard."

                },
                new Platform
                {
                    PlatformID = 2,
                    PlatformName = "Table Top",
                    PlatformDescription = "Touch screen."
                },
                new Platform
                {
                    PlatformID = 3,
                    PlatformName = "Console",
                    PlatformDescription = "Gamepad."
                }
            };

            return platforms;
        }

    }
}
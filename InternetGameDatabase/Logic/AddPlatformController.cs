using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InternetGameDatabase.Models;

namespace InternetGameDatabase.Logic
{
    internal static class AddPlatformController
    {
        private static Platform platform;
        public static void AddPlatform(String platformName, String platformDescription)
        {
            platform = new Platform();
            platform.PlatformName = platformName;
            platform.PlatformDescription = platformDescription;

            using (GameContext db = new GameContext())
            {
                db.Platforms.Add(platform);
                db.SaveChanges();
            }

        }
    }
}
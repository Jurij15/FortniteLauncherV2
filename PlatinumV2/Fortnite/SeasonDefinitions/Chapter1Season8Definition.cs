using PlatinumV2.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatinumV2.Fortnite.SeasonDefinitions
{
    public class Chapter1Season8Definition : FortniteSeasonBaseClass
    {
        public override string SeasonName { get => "C1S8";}

        public override string SeasonDisplayName { get => "Season 8";}
        public override string ChapterDisplayName { get => "Chapter 1"; }
        public override string SeasonDescription { get => "Season 8, with the slogan X Marks the Spot, was the eighth season of Fortnite: Battle Royale. It started on February 28th 2019, and ended on May 8th 2019. Season 8 was the first season that could have been purchased for free if you completed all the challenges during the Share The Love Event.\r\nThe theme revolved around pirates and adventure."; }
        public override string SplashImagePath { get => "ms-appx:///Assets\\Fortnite\\Splashes\\Chapter1Season6.bmp"; }
        public override string SeasonImagePath { get => "ms-appx:///Assets\\Fortnite\\Backgrounds\\Chapter1Season8Background.jpg"; }

        private List<string> builds = new List<string>() { "8.00", "8.01", "8.10", "8.11", "8.20", "8.32", "8.40", "8.50", "8.51" };

        public override List<string> BuildsTagsList { get => builds;}
    }
}

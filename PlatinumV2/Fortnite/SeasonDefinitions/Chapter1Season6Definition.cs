using PlatinumV2.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatinumV2.Fortnite.SeasonDefinitions
{
    public class Chapter1Season5Definition : FortniteSeasonBaseClass
    {
        public override string SeasonName { get => "C1S6";}

        public override string SeasonDisplayName { get => "Season 6";}
        public override string ChapterDisplayName { get => "Chapter 1"; }
        public override string SeasonDescription { get => "Fortnite Chapter 1 Season 6, themed “Corruption”, began on September 27th, 2018 and ended on December 5th, 2018. The season started with the Cube activating, lifting the central island of Loot Lake into the air, and creating an air vortex. New locations like Leaky Lake, Corrupted Areas, Floating Island, and Haunted Castle were introduced. The season ended with a famous Butterfly live event, where the Cube lost control, causing a cataclysmic explosion."; }
        public override string SplashImagePath { get => "ms-appx:///Assets\\Fortnite\\Splashes\\Chapter1Season6.bmp"; }
        public override string SeasonImagePath { get => "ms-appx:///Assets\\Fortnite\\Backgrounds\\Chapter1Season6Background.webp"; }

        private List<string> builds = new List<string>() { "6.00", "6.01", "6.02", "6.10", "6.20", "6.21", "6.22", "6.30", "6.31" };

        public override List<string> BuildsTagsList { get => builds;}
    }
}

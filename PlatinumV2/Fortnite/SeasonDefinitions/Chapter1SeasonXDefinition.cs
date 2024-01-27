using PlatinumV2.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatinumV2.Fortnite.SeasonDefinitions
{
    public class Chapter1SeasonXDefinition : FortniteSeasonBaseClass
    {
        public override string SeasonName { get => "C1SX";}

        public override string SeasonDisplayName { get => "Season X";}
        public override string ChapterDisplayName { get => "Chapter 1"; }
        public override string SeasonDescription { get => "Fortnite Season X, with the slogan Out of Time, was the tenth season of Fortnite: Battle Royale. It started on August 1st 2019, and was scheduled to end on October 6th 2019 but ended on October 13th 2019 after The End Live Event concluded. The theme of the season revolved around time, and was the final season of Chapter 1."; }
        public override string SplashImagePath { get => "ms-appx:///Assets\\Fortnite\\Splashes\\Chapter1Season6.bmp"; }
        public override string SeasonImagePath { get => "ms-appx:///Assets\\Fortnite\\Backgrounds\\Chapter1SeasonXBackground4KTest.webp"; }

        private List<string> builds = new List<string>() { "10.00", "10.10", "10.20", "10.20.2", "6.30", "6.31", "6.40", "6.40.1" };

        public override List<string> BuildsTagsList { get => builds;}
    }
}

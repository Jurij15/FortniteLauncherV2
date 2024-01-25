using PlatinumV2.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatinumV2.Fortnite.SeasonDefinitions
{
    public class Chapter1Season1Definition : FortniteSeasonBaseClass
    {
        public override string SeasonName { get => "C1S1";}

        public override string SeasonDisplayName { get => "Season 1";}
        public override string ChapterDisplayName { get => "Chapter 1"; }
        public override string SeasonDescription { get => "Season 1 was the first season of Fortnite: Battle Royale. It started on October 26, 2017 and concluded on December 13, 2017. It was the second shortest season in the game, lasting only 49 days."; }
        public override string SplashImagePath { get => "ms-appx:///Assets\\Fortnite\\Splashes\\Chapter1Season1.bmp"; }
        public override string SeasonImagePath { get => "ms-appx:///Assets\\Fortnite\\Backgrounds\\DefaultChapter1Background.jpg"; }

        private List<string> builds = new List<string>() { "1.8", "1.8.1", "1.8.2", "1.9", "1.9.1", "1.10", };

        public override List<string> BuildsTagsList { get => builds;}
    }
}

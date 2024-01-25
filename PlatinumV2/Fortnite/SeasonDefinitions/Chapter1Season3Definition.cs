using PlatinumV2.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatinumV2.Fortnite.SeasonDefinitions
{
    public class Chapter1Season3Definition : FortniteSeasonBaseClass
    {
        public override string SeasonName { get => "C1S3";}

        public override string SeasonDisplayName { get => "Season 3";}
        public override string ChapterDisplayName { get => "Chapter 1"; }
        public override string SeasonDescription { get => "Season 3, with the slogan Meteor Strike, was the third season of Fortnite: Battle Royale. It started on February 22nd 2018 and ended on April 30th 2018.\r\nThe season's theme revolved around space exploration, as well as being the season where the Fortnite storyline began."; }
        public override string SplashImagePath { get => "ms-appx:///Assets\\Fortnite\\Splashes\\Chapter1Season1.bmp"; }
        public override string SeasonImagePath { get => "ms-appx:///Assets\\Fortnite\\Backgrounds\\Chapter1Season3Background.jpg"; }

        private List<string> builds = new List<string>() { "3.0", "3.1.0", "3.2.0", "3.3.0", "3.3.1", "3.4.0", "3.5", "3.6", };

        public override List<string> BuildsTagsList { get => builds;}
    }
}

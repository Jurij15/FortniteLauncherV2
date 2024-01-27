using PlatinumV2.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatinumV2.Fortnite.SeasonDefinitions
{
    public class Chapter1Season9Definition : FortniteSeasonBaseClass
    {
        public override string SeasonName { get => "C1S9";}

        public override string SeasonDisplayName { get => "Season 9";}
        public override string ChapterDisplayName { get => "Chapter 1"; }
        public override string SeasonDescription { get => "Fortnite Season 9, with the slogan The Future Is Yours, was the ninth season of Fortnite: Battle Royale. It started on May 9th 2019, and ended on July 31st 2019.\r\nThe theme revolved around the future and reconstruction of various locations that were destroyed by the volcano during The Unvaulting."; }
        public override string SplashImagePath { get => "ms-appx:///Assets\\Fortnite\\Splashes\\Chapter1Season6.bmp"; }
        public override string SeasonImagePath { get => "ms-appx:///Assets\\Fortnite\\Backgrounds\\Chapter1Season9Background.jpg"; }

        private List<string> builds = new List<string>() { "9.00", "9.01", "9.10", "6.20", "6.21", "6.30", "6.40", "6.41" };

        public override List<string> BuildsTagsList { get => builds;}
    }
}

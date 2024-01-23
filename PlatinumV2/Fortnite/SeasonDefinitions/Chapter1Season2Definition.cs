using PlatinumV2.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatinumV2.Fortnite.SeasonDefinitions
{
    public class Chapter1Season2Definition : FortniteSeasonBaseClass
    {
        public override string SeasonName { get => "C1S2";}

        public override string SeasonDisplayName { get => "Chapter 1 Season 2";}
        public override string SeasonDescription { get => "Season 2, with the slogan Fort Knights, was the second season of Fortnite: Battle Royale. It started on December 14th 2017, and ended on February 21st 2018.\r\nThe season's theme revolved around knights and medieval times."; }
        public override string SplashImagePath { get => "ms-appx:///Assets\\Fortnite\\Splashes\\Chapter1Season2.bmp"; }
        public override string SeasonImagePath { get => "ms-appx:///Assets\\LogoBase1920x1080.jpg"; }

        private List<string> builds = new List<string>() { "1.11", "2.1.0", "2.2.0", "2.3.0", "2.4.0", "2.4.2", "2.5.0", };

        public override List<string> BuildsTagsList { get => builds;}
    }
}

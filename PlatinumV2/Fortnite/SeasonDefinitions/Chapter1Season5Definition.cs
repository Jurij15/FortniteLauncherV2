using PlatinumV2.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatinumV2.Fortnite.SeasonDefinitions
{
    public class Chapter1Season6Definition : FortniteSeasonBaseClass
    {
        public override string SeasonName { get => "C1S5";}

        public override string SeasonDisplayName { get => "Chapter 1 Season 5";}
        public override string SeasonDescription { get => "Season 5, with the slogan Worlds Collide, was the fifth season of Fortnite: Battle Royale. It started on July 12th 2018. It was scheduled to end on September 24th 2018 but was extended after Epic Games had extended the season to end with the 1 year anniversary of Fortnite being Free to Play.\r\nThe theme revolved around world collision, following the aftermath of Blast Off, which resulted in things being taken from other realities and put into Reality: Zero."; }
        public override string SplashImagePath { get => "ms-appx:///Assets\\SplashDefault-Vertical-Text.jpg"; }
        public override string SeasonImagePath { get => "ms-appx:///Assets\\LogoBase1920x1080.jpg"; }

        private List<string> builds = new List<string>() { "5.00", "5.10", "5.20", "5.21", "5.30", "5.40", "5.41"};

        public override List<string> BuildsTagsList { get => builds;}
    }
}

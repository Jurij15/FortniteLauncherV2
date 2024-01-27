using PlatinumV2.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatinumV2.Fortnite.SeasonDefinitions
{
    public class Chapter1Season7Definition : FortniteSeasonBaseClass
    {
        public override string SeasonName { get => "C1S7";}

        public override string SeasonDisplayName { get => "Season 7";}
        public override string ChapterDisplayName { get => "Chapter 1"; }
        public override string SeasonDescription { get => "Season 7, with the slogan You Better Watch Out, was the seventh season of Fortnite: Battle Royale. It started on December 6th 2018 and ended on February 28th 2019. It was originally supposed to end on February 14th 2019, but was extended for 2 weeks following Epic Games' holiday break. The theme of the season revolved around winter and Christmas.\r\nBattle Pass owners were also given early access to Fortnite: Creative, in the start of the season. The game mode would become available for everyone 1 week after the start of the season (December 13 2018)."; }
        public override string SplashImagePath { get => "ms-appx:///Assets\\Fortnite\\Splashes\\Chapter1Season6.bmp"; }
        public override string SeasonImagePath { get => "ms-appx:///Assets\\Fortnite\\Backgrounds\\Chapter1Season7Background.jpg"; }

        private List<string> builds = new List<string>() { "7.00", "7.01", "7.10", "7.20", "7.30", "7.31", "7.40" };

        public override List<string> BuildsTagsList { get => builds;}
    }
}

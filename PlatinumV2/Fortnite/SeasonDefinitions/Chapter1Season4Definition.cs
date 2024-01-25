using PlatinumV2.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatinumV2.Fortnite.SeasonDefinitions
{
    public class Chapter1Season4Definition : FortniteSeasonBaseClass
    {
        public override string SeasonName { get => "C1S4";}

        public override string SeasonDisplayName { get => "Season 4";}
        public override string ChapterDisplayName { get => "Chapter 1"; }
        public override string SeasonDescription { get => "Season 4, with the slogan Brace for Impact, was the fourth season of Fortnite: Battle Royale. It started on May 1st 2018 and ended on July 11th 2018. The theme of the season revolved around superheroes. It was also the first season to have a story cinematic trailer and introduce the lore."; }
        public override string SplashImagePath { get => "ms-appx:///Assets\\Fortnite\\Splashes\\Chapter1Season1.bmp"; }
        public override string SeasonImagePath { get => "ms-appx:///Assets\\Fortnite\\Backgrounds\\Chapter1Season4Background.jpg"; }

        private List<string> builds = new List<string>() { "4.0", "4.1", "4.2", "4.3", "4.4", "4.5", };

        public override List<string> BuildsTagsList { get => builds;}
    }
}

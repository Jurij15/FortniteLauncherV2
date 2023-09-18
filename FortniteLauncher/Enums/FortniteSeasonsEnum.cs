using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace FortniteLauncher.Enums
{
    //cant believe i did this
    public enum FortniteSeasons
    {
        [Description("Unknown")]
        Unknown = 0,
        [Description("Season 0")]
        Season0,
        [Description("Season 1")]
        Season1,
        [Description("Season 2")]
        Season2,
        [Description("Season 3")]
        Season3,
        [Description("Season 4")]
        Season4,
        [Description("Season 5")]
        Season5,
        [Description("Season 6")]
        Season6,
        [Description("Season 7")]
        Season7,
        [Description("Season 8")]
        Season8,
        [Description("Season 9")]
        Season9,
        [Description("Season X")]
        SeasonX,
        [Description("Chapter 2 Season 1")]
        Chapter2Season1,
        [Description("Chapter 2 Season 2")]
        Chapter2Season2,
        [Description("Chapter 2 Season 3")]
        Chapter2Season3,
        [Description("Chapter 2 Season 4")]
        Chapter2Season4,
        [Description("Chapter 2 Season 5")]
        Chapter2Season5,
        [Description("Chapter 2 Season 6")]
        Chapter2Season6,
        [Description("Chapter 2 Season 7")]
        Chapter2Season7,
        [Description("Chapter 2 Season 8")]
        Chapter2Season8,
        [Description("Chapter 3 Season 1")]
        Chapter3Season1,
        [Description("Chapter 3 Season 2")]
        Chapter3Season2,
        [Description("Chapter 3 Season 3")]
        Chapter3Season3,
        [Description("Chapter 3 Season 4")]
        Chapter3Season4,
    }
}

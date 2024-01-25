using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatinumV2.Classes
{
    public abstract class FortniteSeasonBaseClass
    {
        public abstract string SeasonName {  get;}

        public abstract string SeasonDisplayName { get;}
        public abstract string ChapterDisplayName {  get;}
        public abstract string SeasonDescription { get;}
        public abstract string SplashImagePath { get;}
        public abstract string SeasonImagePath { get;}

        public abstract List<string> BuildsTagsList { get;}
    }
}

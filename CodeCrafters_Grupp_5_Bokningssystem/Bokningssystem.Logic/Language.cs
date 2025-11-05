using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bokningssystem.Logic
{
    public enum Language
    {
        Swedish,
        English
    }

    public static class DisplayLanguage
    {
        private static Language _selected = Language.Swedish;

        public static Language Selected
        {
            get { return _selected; }
            set
            {
                _selected = value;
            }
        }
    }
}
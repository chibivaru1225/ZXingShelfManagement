using System;
using System.Collections.Generic;
using System.Text;

namespace ZXingShelfManagement
{
    public class Util
    {
        public static String BaseURL = "http://webhn.local.zoa.co.jp:60001/magic94Scripts/mgrqispi94.dll";
        public static String GetURL = BaseURL + "?APPNAME=WEBHNCTL&PRGNAME=GetBhtItemInfo&ARGUMENTS=-N";
    }
}

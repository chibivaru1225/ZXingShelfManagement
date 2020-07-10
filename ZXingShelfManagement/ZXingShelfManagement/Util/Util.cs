using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace ZXingShelfManagement
{
    public class Util
    {
        public static String BaseURL = "http://webhn.local.zoa.co.jp:60001/magic94Scripts/mgrqispi94.dll";
        public static String GetURL = BaseURL + "?APPNAME=WEBHNCTL&PRGNAME=GetBhtItemInfo&ARGUMENTS=-N";
        public static String PostURL = BaseURL;

        public static ObservableCollection<ShelfStatus> Statuses { get; set; } = new ObservableCollection<ShelfStatus>();

        public static String ConvertCSVString
        {
            get
            {
                String txt = String.Empty;

                foreach (var row in Statuses)
                {
                    if (row.IsSend == false || row.SelectStatus == Enum.SelectStatuses.NONE)
                        continue;

                    txt += row.JANCode + ",";
                    txt += row.SelectStatus.CSVValue + ",";
                    txt += row.SelectStatus.CSVBikoValue + "\r\n";
                }

                return txt;
            }
        }
    }

    public class HttpResult
    {
        public bool IsSuccess { get; }

        public HttpResult(bool Success)
        {
            this.IsSuccess = Success;
        }
    }
}

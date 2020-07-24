using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ZXingShelfManagement
{
    public class Enum
    {
        #region ItemStatus

        public enum ItemStatuses
        {
            OnStock,
            OnArrival,
            NotArrival,
            NONE,
        }

        public class ItemStatus
        {
            private ItemStatuses ItemStatuses;

            public ItemStatus(ItemStatuses types)
            {
                this.ItemStatuses = types;
            }

            public ItemStatuses GetTypes()
            {
                return this.ItemStatuses;
            }

            public String DispName
            {
                get
                {
                    return GetDispName(this.ItemStatuses);
                }
            }

            public static String GetDispName(ItemStatuses type)
            {
                switch (type)
                {
                    case ItemStatuses.OnStock:
                        return "在庫あり";
                    case ItemStatuses.OnArrival:
                        return "入荷予定あり";
                    case ItemStatuses.NotArrival:
                        return "入荷予定なし";
                    default:
                        return String.Empty;
                }
            }

            /// <summary>
            /// 静的型変換
            /// Class -> Enum
            /// </summary>
            /// <param name="shopType"></param>
            public static implicit operator ItemStatuses(ItemStatus getMode)
            {
                return getMode.ItemStatuses;
            }

            /// <summary>
            /// 静的型変換
            /// Enum -> Class
            /// </summary>
            /// <param name="shopTypes"></param>
            public static implicit operator ItemStatus(ItemStatuses getModes)
            {
                return new ItemStatus(getModes);
            }
        }

        #endregion

        #region SelectStatus

        public enum SelectStatuses
        {
            PopRemove,
            Transfer,
            Display,
            PopCreate,
            OPIncrease,
            OPDecrease,
            NONE,
        }

        public class SelectStatus
        {
            private SelectStatuses SelectStatuses;

            public SelectStatus(SelectStatuses types)
            {
                this.SelectStatuses = types;
            }

            public SelectStatuses GetTypes()
            {
                return this.SelectStatuses;
            }

            public String DispName
            {
                get
                {
                    return GetDispName(this.SelectStatuses);
                }
            }

            public static String GetDispName(SelectStatuses type)
            {
                switch (type)
                {
                    case SelectStatuses.PopRemove:
                        return "POPを外す";
                    case SelectStatuses.Transfer:
                        return "補充依頼する";
                    case SelectStatuses.Display:
                        return "陳列･在庫チェック";
                    case SelectStatuses.PopCreate:
                        return "POP出力";
                    case SelectStatuses.OPIncrease:
                        return "発点増申請";
                    case SelectStatuses.OPDecrease:
                        return "発点減申請";
                    default:
                        return "何もしない";
                }
            }

            public static bool ItemStatusEnable(SelectStatuses select, ItemStatuses item)
            {
                switch (select)
                {
                    case SelectStatuses.PopRemove:
                        return item == ItemStatuses.NotArrival;
                    case SelectStatuses.Transfer:
                        return item == ItemStatuses.NotArrival;
                    case SelectStatuses.Display:
                        return item == ItemStatuses.OnStock;
                    case SelectStatuses.PopCreate:
                        return item == ItemStatuses.OnStock;
                    case SelectStatuses.OPIncrease:
                        return item == ItemStatuses.OnStock || item == ItemStatuses.OnArrival;
                    case SelectStatuses.OPDecrease:
                        return item == ItemStatuses.OnStock;
                    default:
                        return true;
                }
            }

            public static void SetText(Button button, SelectStatuses select)
            {
                button.Text = GetDispName(select);
            }

            public static void SetEnable(Button button, SelectStatuses select, ItemStatuses item)
            {
                button.IsEnabled = ItemStatusEnable(select, item);
            }

            public String CSVValue
            {
                get
                {
                    return GetCSVValue(this.SelectStatuses);
                }
            }

            public static String GetCSVValue(SelectStatuses item)
            {
                switch (item)
                {
                    case SelectStatuses.PopRemove:
                        return "ＰＯＰを外す";
                    case SelectStatuses.Transfer:
                        return "補充依頼する";
                    case SelectStatuses.Display:
                        return "陳列在庫ﾁｪｯｸ";
                    case SelectStatuses.PopCreate:
                        return "ＰＯＰ出力";
                    case SelectStatuses.OPIncrease:
                        return "発注点変更";
                    case SelectStatuses.OPDecrease:
                        return "発注点変更";
                    default:
                        return String.Empty;
                }
            }

            public String CSVBikoValue
            {
                get
                {
                    return GetCSVBikoValue(this.SelectStatuses);
                }
            }

            public static String GetCSVBikoValue(SelectStatuses item)
            {
                switch (item)
                {
                    case SelectStatuses.OPIncrease:
                        return "増やす";
                    case SelectStatuses.OPDecrease:
                        return "減らす";
                    default:
                        return String.Empty;
                }
            }

            /// <summary>
            /// 静的型変換
            /// Class -> Enum
            /// </summary>
            /// <param name="shopType"></param>
            public static implicit operator SelectStatuses(SelectStatus getMode)
            {
                return getMode.SelectStatuses;
            }

            /// <summary>
            /// 静的型変換
            /// Enum -> Class
            /// </summary>
            /// <param name="shopTypes"></param>
            public static implicit operator SelectStatus(SelectStatuses getModes)
            {
                return new SelectStatus(getModes);
            }
        }

        #endregion
    }
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;

namespace ZXingShelfManagement
{
    public class ShelfStatus : INotifyPropertyChanged
    {
        public ShelfStatus()
        {
            this.issend = true;
        }

        [JsonProperty("JANCode")]
        public string JANCode { get; set; }

        [JsonProperty("ShohinBan")]
        public string ShohinBan { get; set; }

        [JsonProperty("ShohinCode")]
        public string ShohinCode { get; set; }

        [JsonProperty("Mise")]
        public string Mise { get; set; }

        [JsonProperty("Jyotai")]
        public string Jyotai { get; set; }

        [JsonProperty("JZaiko")]
        public int JZaiko { get; set; }

        [JsonProperty("JZaiHatsu")]
        public int JZaiHatsu { get; set; }

        [JsonProperty("JIso")]
        public int JIso { get; set; }

        [JsonProperty("JHatten")]
        public int JHatten { get; set; }

        [JsonProperty("JSyubai1")]
        public int JSyubai1 { get; set; }

        [JsonProperty("JSyubai2")]
        public int JSyubai2 { get; set; }

        [JsonProperty("JSyubai3")]
        public int JSyubai3 { get; set; }

        [JsonProperty("ZZaiko")]
        public int ZZaiko { get; set; }

        [JsonProperty("ZZaiHatsu")]
        public int ZZaiHatsu { get; set; }

        [JsonProperty("ZIso")]
        public int ZIso { get; set; }

        [JsonProperty("ZHatten")]
        public int ZHatten { get; set; }

        [JsonProperty("ZSyubai1")]
        public int ZSyubai1 { get; set; }

        [JsonProperty("ZSyubai2")]
        public int ZSyubai2 { get; set; }

        [JsonProperty("ZSyubai3")]
        public int ZSyubai3 { get; set; }

        [JsonProperty("Arari")]
        public int Arari { get; set; }

        private bool issend;

        public bool IsSend
        {
            get
            {
                return issend;
            }
            set
            {
                this.issend = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsSend)));
            }
        }

        public Enum.ItemStatus ItemStatus
        {
            get
            {
                if (this.JZaiko <= 0)
                {
                    if (this.JZaiHatsu <= 0 && this.JIso <= 0 && this.JHatten <= 0)
                    {
                        return Enum.ItemStatuses.OnArrival;
                    }
                    else
                    {
                        return Enum.ItemStatuses.NotArrival;
                    }
                }
                else
                {
                    return Enum.ItemStatuses.OnStock;
                }
            }
        }

        private Enum.SelectStatus select;

        public Enum.SelectStatus SelectStatus
        {
            get
            {
                return select;
            }
            set
            {
                this.select = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectStatus)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectStatusText)));
            }
        }

        public String ItemStatusText
        {
            get
            {
                return this.ItemStatus.DispName;
            }
        }

        public String SelectStatusText
        {
            get
            {
                return this.SelectStatus.GetDispName;
            }
        }

        public Color RowColor { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}

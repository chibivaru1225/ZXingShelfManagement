using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ZXingShelfManagement
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SelectPage : ContentPage
    {
        private ShelfStatus status;
        private Dictionary<Button, Enum.SelectStatuses> buttontags;

        public Page ParentPage { get; set; }

        public SelectPage(ShelfStatus status = null)
        {
            InitializeComponent();
            this.status = status;

            this.Title = status.ShohinCode;
            NavigationPage.SetBackButtonTitle(this, String.Empty);

            buttontags = new Dictionary<Button, Enum.SelectStatuses>();
            SetTags();

            SetShelfStatus(this.status);
        }

        private void SetShelfStatus(ShelfStatus status)
        {
            txtItemStatus.Text = status.ItemStatus.GetDispName;

            txtShelfSb.Text = String.Format("(商:{0})", status.ShohinBan);
            txtShelfJy.Text = String.Format("状態:{0}", status.Jyotai);
            txtShelfAr.Text = String.Format("粗:{0}", status.Arari);

            txtShelfJMise.Text = status.Mise;

            txtShelfJZaiko.Text = String.Format("{0}", status.JZaiko);
            txtShelfJZaiHatsu.Text = String.Format("{0}", status.JZaiHatsu);
            txtShelfJIso.Text = String.Format("{0}", status.JIso);
            txtShelfJHatten.Text = String.Format("{0}", status.JHatten);
            txtShelfJSyubai1.Text = String.Format("{0}", status.JSyubai1);
            txtShelfJSyubai2.Text = String.Format("{0}", status.JSyubai2);
            txtShelfJSyubai3.Text = String.Format("{0}", status.JSyubai3);

            txtShelfZZaiko.Text = String.Format("{0}", status.ZZaiko);
            txtShelfZZaiHatsu.Text = String.Format("{0}", status.ZZaiHatsu);
            txtShelfZIso.Text = String.Format("{0}", status.ZIso);
            txtShelfZHatten.Text = String.Format("{0}", status.ZHatten);
            txtShelfZSyubai1.Text = String.Format("{0}", status.ZSyubai1);
            txtShelfZSyubai2.Text = String.Format("{0}", status.ZSyubai2);
            txtShelfZSyubai3.Text = String.Format("{0}", status.ZSyubai3);

            foreach (var t in buttontags)
            {
                Enum.SelectStatus.SetText(t.Key, t.Value);
                Enum.SelectStatus.SetEnable(t.Key, t.Value, status.ItemStatus);

                t.Key.Clicked += ButtonSelected;
            }
        }

        private void SetTags()
        {
            buttontags.Add(btnDisplay, Enum.SelectStatuses.Display);
            buttontags.Add(btnNONE, Enum.SelectStatuses.NONE);
            buttontags.Add(btnOPDecrease, Enum.SelectStatuses.OPDecrease);
            buttontags.Add(btnOPIncrease, Enum.SelectStatuses.OPIncrease);
            buttontags.Add(btnPopCreate, Enum.SelectStatuses.PopCreate);
            buttontags.Add(btnPopRemove, Enum.SelectStatuses.PopRemove);
            buttontags.Add(btnTransfer, Enum.SelectStatuses.Transfer);
        }

        private void ButtonSelected(object sender, EventArgs e)
        {
            if (sender is Button btn && buttontags.ContainsKey(btn))
            {
                this.status.SelectStatus = buttontags[btn];

                if (this.ParentPage is ScanPage)
                {
                    Util.Statuses.Add(this.status);
                    Navigation.PushAsync(new NextOpePage(), true);
                }

                Navigation.RemovePage(this);
            }
        }
    }
}
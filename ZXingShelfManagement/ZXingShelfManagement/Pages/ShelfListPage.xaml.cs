using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Markup;
using Xamarin.Forms.Xaml;

namespace ZXingShelfManagement
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ShelfListPage : ContentPage, ITaskListener
    {
        public ShelfListPage()
        {
            InitializeComponent();

            this.Title = "スキャン履歴";
            NavigationPage.SetBackButtonTitle(this, String.Empty);

            TaskHttpPost.Instance.Listener = this;
            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                {
                    for (int i = 0; i < Util.Statuses.Count; i++)
                        Util.Statuses[i].RowColor = i % 2 == 0 ? Color.Black : Color.DimGray;

                    break;
                }
                case Device.Android:
                {
                    for (int i = 0; i < Util.Statuses.Count; i++)
                        Util.Statuses[i].RowColor = i % 2 == 0 ? Color.White : Color.LightGray;

                    break;
                }
            }

            this.ShelfStatusList.ItemsSource = Util.Statuses;
        }

        private void AllSelect_Clicked(object sender, EventArgs e)
        {
            foreach (var status in Util.Statuses)
            {
                status.IsSend = true;
            }
        }

        private void AllRelease_Clicked(object sender, EventArgs e)
        {
            foreach (var status in Util.Statuses)
            {
                status.IsSend = false;
            }
        }

        private void Send_Clicked(object sender, EventArgs e)
        {
            if (TaskHttpPost.Instance.IsBusy == false)
            {
                IsBusy = true;
                BtnSendSetEnabled();
                TaskHttpPost.Instance.Run();
            }
        }

        private void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (sender != null && sender is ListView view)
                Navigation.PushAsync(new SelectPage((ShelfStatus)view.SelectedItem), true);
        }

        public void OnSuccess()
        {
            IsBusy = false;
            Util.Statuses.Clear();
            BtnSendSetEnabled();
            DisplayAlert("送信完了", String.Format("{0}件 送信しました",TaskHttpPost.Instance.SuccessCount), "OK");
        }

        public void OnFailure()
        {
            IsBusy = false;
            BtnSendSetEnabled();
            DisplayAlert("送信失敗", "時間をおいて再送信してください", "OK");
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            BtnSendSetEnabled();
        }

        private void BtnSendSetEnabled()
        {
            btnSend.IsEnabled = IsBusy == false && Util.Statuses.Count(x => x.IsSend && x.SelectStatus != Enum.SelectStatuses.NONE) > 0;
        }
    }
}
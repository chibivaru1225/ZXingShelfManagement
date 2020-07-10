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
    public partial class ScanPage : ContentPage, ITaskListener
    {
        public ScanPage()
        {
            InitializeComponent();

            zxing.AutoFocus();
            TaskHttpGet.Instance.Listener = this;
            this.Title = "バーコードスキャン";
            NavigationPage.SetBackButtonTitle(this, String.Empty);
        }

        public void Handle_OnScanResult(ZXing.Result result)
        {
            this.IsBusy = true;
            zxing.IsAnalyzing = false;
            TaskHttpGet.Instance.Run(result.Text);
        }

        public void OnFailure()
        {
            this.IsBusy = false;
            zxing.IsAnalyzing = true;
        }

        public void OnSuccess()
        {
            this.IsBusy = false;
            zxing.IsAnalyzing = false;
            var p = new SelectPage(TaskHttpGet.Instance.LatestStatus);
            p.ParentPage = this;
            Navigation.PushAsync(p, true);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            zxing.IsScanning = true;

            if (zxing.IsAnalyzing == false)
                zxing.IsAnalyzing = true;
        }
    }
}
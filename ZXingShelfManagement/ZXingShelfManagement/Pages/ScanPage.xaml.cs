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

            TaskHttpGet.Instance.Listener = this;
            this.Title = "棚管理";
        }

        public void Handle_OnScanResult(ZXing.Result result)
        {
            zxing.IsScanning = false;
            TaskHttpGet.Instance.Run(result.Text);
        }

        public void OnFailure()
        {
            zxing.IsScanning = true;
        }

        public void OnSuccess()
        {
            Navigation.PushAsync(new SelectPage());
            //Navigation.PopAsync(true);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            zxing.IsScanning = true;
        }

        protected override void OnDisappearing()
        {
            zxing.IsScanning = false;
            base.OnDisappearing();
        }
    }
}
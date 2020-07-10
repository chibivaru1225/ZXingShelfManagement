using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ZXingShelfManagement
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ScanPage : ContentPage, ITaskListener
    {
        private BackgroundWorker autoFocus;
        private bool autoFocusing;

        public ScanPage()
        {
            InitializeComponent();

            zxing.AutoFocus();
            TaskHttpGet.Instance.Listener = this;
            this.Title = "バーコードスキャン";
            NavigationPage.SetBackButtonTitle(this, String.Empty);

            autoFocusing = true;

            autoFocus = new BackgroundWorker();
            autoFocus.DoWork += AutoFocus_DoWork;
            autoFocus.RunWorkerCompleted += AutoFocus_RunWorkerCompleted;
        }


        private void AutoFocus_DoWork(object sender, DoWorkEventArgs e)
        {
            Thread.Sleep(250);
            zxing?.AutoFocus();
        }

        private void AutoFocus_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (autoFocusing == true)
                autoFocus.RunWorkerAsync();
        }

        public void Handle_OnScanResult(ZXing.Result result)
        {
            autoFocusing = false;
            this.IsBusy = true;
            zxing.IsAnalyzing = false;
            TaskHttpGet.Instance.Run(result.Text);
        }

        public void OnFailure()
        {
            autoFocusing = true;
            this.IsBusy = false;
            zxing.IsAnalyzing = true;
            autoFocus.RunWorkerAsync();
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
            autoFocusing = true;
            autoFocus.RunWorkerAsync();

            if (zxing.IsAnalyzing == false)
                zxing.IsAnalyzing = true;
        }

        protected override void OnDisappearing()
        {
            autoFocusing = false;
            base.OnDisappearing();
        }
    }
}
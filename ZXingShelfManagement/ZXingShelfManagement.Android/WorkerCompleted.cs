using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Essentials;
using ZXingShelfManagement;
using static ZXingShelfManagement.TaskHttpGet;

[assembly: Xamarin.Forms.Dependency(typeof(ZXingShelfManagement.Android.WorkerCompleted))]
namespace ZXingShelfManagement.Android
{
    public class WorkerCompleted : ICompleted
    {
        private ITaskListener listener;

        public ICompleted SetListener(ITaskListener listener)
        {
            this.listener = listener;
            return this;
        }

        public void RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                if (e.Result == null || !(e.Result is HttpGetResult))
                {
                    this.listener?.OnFailure();
                }
                else if (e.Result != null && e.Result is HttpGetResult result)
                {
                    if (result.IsSuccess)
                        this.listener?.OnSuccess();
                    else
                        this.listener?.OnFailure();
                }
            });
        }
    }
}
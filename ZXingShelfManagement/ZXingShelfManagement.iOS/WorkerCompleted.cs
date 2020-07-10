using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using ZXingShelfManagement;
using static ZXingShelfManagement.TaskHttpGet;

[assembly: Xamarin.Forms.Dependency(typeof(ZXingShelfManagement.iOS.WorkerCompleted))]
namespace ZXingShelfManagement.iOS
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
            UIApplication.SharedApplication.InvokeOnMainThread(() =>
            {
                if (e.Result == null || !(e.Result is HttpResult))
                {
                    this.listener?.OnFailure();
                }
                else if (e.Result != null && e.Result is HttpResult result)
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
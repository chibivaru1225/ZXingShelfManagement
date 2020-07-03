using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Http;
using System.Text;
using Xamarin.Forms;

namespace ZXingShelfManagement
{
    public class TaskHttpGet
    {
        private BackgroundWorker worker;
        private ITaskListener listener;
        private static TaskHttpGet instance;
        private HttpClient client;

        public bool IsBusy => worker.IsBusy;

        public static TaskHttpGet Instance
        {
            get
            {
                if (instance == null)
                    instance = new TaskHttpGet();

                return instance;
            }
        }

        public ITaskListener Listener
        {
            set
            {
                this.listener = value;
            }
        }

        private TaskHttpGet()
        {
            worker = new BackgroundWorker();
            worker.DoWork += DoWork;
            worker.RunWorkerCompleted += DependencyService.Get<ICompleted>().RunWorkerCompleted;
            client = new HttpClient();
        }

        public void Run(string jancode)
        {
            if (!worker.IsBusy)
                worker.RunWorkerAsync(jancode);
        }

        private void DoWork(object sender, DoWorkEventArgs e)
        {
            DependencyService.Get<ICompleted>().SetListener(this.listener);

            if (e.Argument == null || !(e.Argument is String))
            {
                e.Result = new HttpGetResult(false);
                return;
            }
            else if (e.Argument != null && e.Argument is String jancode)
            {
                var r = client.GetAsync(Util.GetURL + jancode).Result;

                Console.WriteLine(r.Content.ReadAsStringAsync().Result);

                e.Result = new HttpGetResult(true);
                return;
            }
        }

        //private void RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        //{
        //    switch (Device.RuntimePlatform)
        //    {
        //        case Device.Android:
        //            if (e.Result == null || !(e.Result is HttpGetResult))
        //            {
        //                this.listener?.OnFailure();
        //            }
        //            else if (e.Result != null && e.Result is HttpGetResult result)
        //            {
        //                if (result.IsSuccess)
        //                    this.listener?.OnSuccess();
        //                else
        //                    this.listener?.OnFailure();
        //            }
        //            break;
        //        case Device.iOS:
        //            break;
        //    }
        //}

        public class HttpGetResult
        {
            public bool IsSuccess
            {
                get
                {
                    return issuccess;
                }
            }

            private bool issuccess;

            public HttpGetResult(bool Success)
            {
                this.issuccess = Success;
            }
        }
    }
}

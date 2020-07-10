using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Web;
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

        public ShelfStatus LatestStatus { get; set; }

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
                e.Result = new HttpResult(false);
                return;
            }
            else if (e.Argument != null && e.Argument is String jancode)
            {
                var url = Util.GetURL + HttpUtility.UrlEncode(jancode);
                var r = client.GetAsync(url).Result;
                var enc = Portable.Text.Encoding.GetEncoding("Shift-JIS");

                using (var stream = (r.Content.ReadAsStreamAsync().Result))
                using (var reader = (new StreamReader(stream, enc, true)) as TextReader)
                {
                    try
                    {
                        var t = reader.ReadToEnd();
                        Console.WriteLine(t);
                        this.LatestStatus = JsonConvert.DeserializeObject<ShelfStatus>(t);
                        e.Result = new HttpResult(true);
                    }
                    catch
                    {
                        e.Result = new HttpResult(false);
                    }
                }

                return;
            }
        }
    }
}

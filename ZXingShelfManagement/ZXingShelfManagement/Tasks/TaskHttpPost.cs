using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Web;
using Xamarin.Forms;

namespace ZXingShelfManagement
{
    public class TaskHttpPost
    {
        private BackgroundWorker worker;
        private ITaskListener listener;
        private static TaskHttpPost instance;
        //private HttpClient client;

        public bool IsBusy => worker.IsBusy;

        public static TaskHttpPost Instance
        {
            get
            {
                if (instance == null)
                    instance = new TaskHttpPost();

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

        public int SuccessCount { get; set; }

        private TaskHttpPost()
        {
            worker = new BackgroundWorker();
            worker.DoWork += DoWork;
            worker.RunWorkerCompleted += DependencyService.Get<ICompleted>().RunWorkerCompleted;
            //client = new HttpClient();
        }

        public void Run()
        {
            if (!worker.IsBusy)
                worker.RunWorkerAsync();
        }

        private void DoWork(object sender, DoWorkEventArgs e)
        {
            Thread.Sleep(1000);

            DependencyService.Get<ICompleted>().SetListener(this.listener);

            var enc = Portable.Text.Encoding.GetEncoding("Shift-JIS");

            var url = Util.PostURL;
            var content = new FormUrlEncodedContent(new Dictionary<String, String>
            {
                {"appname", "webhnctl"},
                {"prgname", "receiveTanaMenteData"},
                {"arguments", "sendData"},
                {"sendData", Util.ConvertCSVString},
                {"submit", "送信"},
            });

            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("User-Agent", "ZOA.Xamarin.ZXing.ShelfManagement");
                    client.DefaultRequestHeaders.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8");
                    client.DefaultRequestHeaders.Add("Accept-Language", "ja,en-US;q=0.7,en;q=0.3");
                    client.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate");
                    client.DefaultRequestHeaders.Add("Referer", "http://webhn/uploadtest.html");
                    client.DefaultRequestHeaders.Add("Connection", "keep-alive");
                    client.DefaultRequestHeaders.Add("Cache-Control", "no-cache");

                    var r = client.PostAsync(url, content).Result;

                    using (var stream = (r.Content.ReadAsStreamAsync().Result))
                    using (var reader = (new StreamReader(stream, enc, true)) as TextReader)
                    {
                        try
                        {
                            var t = reader.ReadToEnd();
                            Console.WriteLine(t);
                            this.SuccessCount = int.Parse(t);
                            e.Result = new HttpResult(true);
                        }
                        catch
                        {
                            e.Result = new HttpResult(false);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                e.Result = new HttpResult(false);
            }

            return;
        }
    }
}
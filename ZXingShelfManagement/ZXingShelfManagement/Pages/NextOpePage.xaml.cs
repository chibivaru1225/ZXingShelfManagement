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
    public partial class NextOpePage : ContentPage
    {
        public NextOpePage()
        {
            InitializeComponent();

            this.Title = "棚管理";
            NavigationPage.SetBackButtonTitle(this, String.Empty);
        }

        private void NextCode_Click(object sender, EventArgs e)
        {
            Navigation.PopAsync(true);
            //Navigation.PushAsync(new ScanPage());
        }

        private void ReturnMenu_Click(object sender, EventArgs e)
        {
            Navigation.PopToRootAsync(true);
        }
    }
}
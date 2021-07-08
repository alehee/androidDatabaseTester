using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace androidDatabaseTester
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new Main();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}

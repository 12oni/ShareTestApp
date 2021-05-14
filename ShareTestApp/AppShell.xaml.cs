using System;
using System.Collections.Generic;
using ShareTestApp.ViewModels;
using ShareTestApp.Views;
using Xamarin.Forms;

namespace ShareTestApp
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
        }

    }
}

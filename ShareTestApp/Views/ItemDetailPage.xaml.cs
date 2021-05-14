using System.ComponentModel;
using Xamarin.Forms;
using ShareTestApp.ViewModels;

namespace ShareTestApp.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}
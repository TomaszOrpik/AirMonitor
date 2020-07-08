using Laboratoria.ViewModels;
using Laboratoria.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Laboratoria
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        private HomeViewModel _viewModel => BindingContext as HomeViewModel;
        public HomePage()
        {
            InitializeComponent();

            BindingContext = new HomeViewModel(Navigation);
            
        }
        void ListView_ItemTapped(Object sender, ItemTappedEventArgs e)
        {
            _viewModel.NavigateToDetailsPageCommand.Execute(e.Item as Measurements);
        }

        void ListView_Refreshing(object sender, EventArgs e)
        {
            _viewModel.PullToRefreshCommand.Execute(e);
        }
    }
}
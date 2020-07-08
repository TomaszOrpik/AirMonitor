using Laboratoria.Models;
using Laboratoria.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Laboratoria
{
    public partial class DetailsPage : ContentPage
    {
        public DetailsPage(Measurements item)
        {
            InitializeComponent();

            (BindingContext as DetailsViewModel).Item = item;
            
        }

        private void Help_Clicked(object sender, EventArgs e)
        {
            DisplayAlert("Co to jest CAQI?", "Lorem ipsum.", "Zamknij");
        }
    }
}

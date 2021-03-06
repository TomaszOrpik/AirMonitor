﻿using Laboratoria.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Laboratoria.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MapPage : ContentPage
    {
        private HomeViewModel _viewModel => BindingContext as HomeViewModel;
        public MapPage()
        {
            InitializeComponent();
            BindingContext = new HomeViewModel(Navigation);
        }

        private void Pin_InfoWindowClicked(object sender, Xamarin.Forms.Maps.PinClickedEventArgs e)
        {
            _viewModel.InfoWindowClickedCommand.Execute((sender as Xamarin.Forms.Maps.Pin));
        }
    }
}
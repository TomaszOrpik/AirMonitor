using AirMonitor.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace AirMonitor.ViewModels
{
    class HomeViewModel
    {
        private readonly INavigation _navigation;
        public ICommand _goToDetailsCommand { get; private set; }

        public HomeViewModel(INavigation navigation)
        {
            _navigation = navigation;
            _goToDetailsCommand = new Command(OnGoToDetails);
        }

        public ICommand GoToDetailsCommand()
        {
            if (_goToDetailsCommand == null) _goToDetailsCommand = new Command(OnGoToDetails);
            return _goToDetailsCommand;
        }

        private async void OnGoToDetails()
        {
            await _navigation.PushAsync(new DetailsPage());
        }
    }
}

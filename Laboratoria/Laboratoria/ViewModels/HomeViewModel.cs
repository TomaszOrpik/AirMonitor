﻿using Laboratoria.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Laboratoria.ViewModels
{
    class HomeViewModel : BaseViewModel
    {
        readonly INavigation _navigation;
        private ICommand _navigateToDetailsPageCommand;
        private ICommand _pullToRefreshCommand;
        private ICommand _InfoWindowClickedCommand;
        private Location _location;
        private List<Measurements> Measurements;
        private List<MapLocation> _Locations;
        public List<MapLocation> Locations
        {
            get => _Locations;
            set => SetProperty(ref _Locations, value);
        }
        private List<Measurements> _MeasurmentsList;
        public List<Measurements> MeasurmentsList
        {
            get => _MeasurmentsList;
            set => SetProperty(ref _MeasurmentsList, value);
        }
        private bool _isBusy;
        public bool IsBusy
        {
            get => _isBusy;
            set => SetProperty(ref _isBusy, value);
        }
        private bool _isRefreshing;
        public bool IsRefreshing
        {
            get => _isRefreshing;
            set => SetProperty(ref _isRefreshing, value);
        }
        private DateTime TillDate;

        public ICommand NavigateToDetailsPageCommand =>
            _navigateToDetailsPageCommand ?? (_navigateToDetailsPageCommand = new Command<Measurements>(NavitageToDetailsPage));

        public ICommand PullToRefreshCommand =>
            _pullToRefreshCommand ?? (_pullToRefreshCommand = new Command(RefreshPage));

        public ICommand InfoWindowClickedCommand =>
            _InfoWindowClickedCommand ?? (_InfoWindowClickedCommand = new Command<Xamarin.Forms.Maps.Pin>(InfoWindowClicked));

        public HomeViewModel(INavigation navigation)
        {
            _navigation = navigation;
            _ = Initialize();
        }

        private async void RefreshPage()
        {
            IsRefreshing = true;
            await Initialize();
            IsRefreshing = false;
        }

        private void NavitageToDetailsPage(Measurements item)
        {
            _navigation.PushAsync(new DetailsPage(item));
        }

        private void InfoWindowClicked(Xamarin.Forms.Maps.Pin pin)
        {
            var address = pin.Address;
            Measurements item = this.Measurements.First<Measurements>(i => i.Installation.Address.Description.Equals(address));
            _navigation.PushAsync(new DetailsPage(item));
        }

        private async Task Initialize()
        {
            if (!IsRefreshing) 
                IsBusy = true;
         
            if(_location != await GetLocation() &&  
                DateTime.Now > DateTime.Now.Subtract(TimeSpan.FromHours(1)) ){
                _location = await GetLocation();
                var InstallationsList = await GetInstallations(_location, maxResults: 3);
                App.dbContext.SaveInstallationsList(InstallationsList); //Save data into database
                var data = await GetMeasurementsForInstallations(InstallationsList);
                if (App.dbContext.GetDate() == this.TillDate)
                {
                    MeasurmentsList = App.dbContext.GetAllMeasurement();
                }
                else
                {
                    App.dbContext.SaveAllMeasurements(data);
                    App.dbContext.UpdateDate(this.TillDate);
                    MeasurmentsList = new List<Measurements>(data); //itemContext for tableView
                }
                IsBusy = false;
            }
        }



        private async Task<IEnumerable<Installations>> GetInstallations(Location location, double maxDistanceInKm = 3, int maxResults = -1)
        {
            if (location == null)
            {
                System.Diagnostics.Debug.WriteLine("No location data.");
                return null;
            }

            var query = GetQuery(new Dictionary<string, object>
            {
                { "lat", location.Latitude },
                { "lng", location.Longitude },
                { "maxDistanceKM", maxDistanceInKm },
                { "maxResults", maxResults }
            });
            
            return await GetHttpResponseAsync<IEnumerable<Installations>>(GetAirlyApiUrl(App.AirlyApiInstallationUrl, query));
        }

        private async Task<IEnumerable<Measurements>> GetMeasurementsForInstallations(IEnumerable<Installations> InstallationsList)
        {
            if (InstallationsList == null)
            {
                System.Diagnostics.Debug.WriteLine("No installations data.");
                return null;
            }

            var measurements = new List<Measurements>();

            foreach (var installation in InstallationsList)
            {
                var query = GetQuery(new Dictionary<string, object>
                {
                    { "installationId", installation.Id }
                });
                var url = GetAirlyApiUrl(App.AirlyApiMeasurementUrl, query);

                var response = await GetHttpResponseAsync<Measurements>(url);

                if (response != null)
                {
                    response.Installation = installation;
                    response.CurrentDisplayValue = (int)Math.Round(response.Current?.Indexes?.FirstOrDefault()?.Value ?? 0);
                    //get Date from Request
                    this.TillDate = response.Current.TillDateTime;
                    measurements.Add(response);
                    this.Measurements = measurements;
                    Locations = measurements.Select(i => new MapLocation
                    {
                        Address = i.Installation.Address.Description,
                        Description = "CAQI: " + i.CurrentDisplayValue,
                        Position = new Xamarin.Forms.Maps.Position(i.Installation.Location.Latitude, i.Installation.Location.Longitude)
                    }).ToList();
                }
            }

            return measurements;
        }

        private async Task<T> GetHttpResponseAsync<T>(string url)
        {
            try
            {
                var client = GetClientWithHeaders();
                var response = await client.GetAsync(url);

                if (response.Headers.TryGetValues("X-RateLimit-Limit-day", out var dayLimit) &&
                    response.Headers.TryGetValues("X-RateLimit-Remaining-day", out var dayLimitRemaining))                
                    System.Diagnostics.Debug.WriteLine($"Day limit: {dayLimit?.FirstOrDefault()}, remaining: {dayLimitRemaining?.FirstOrDefault()}");
                
                if((int)response.StatusCode == 200)
                {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<T>(content);
                        return result;
               }
                else if((int)response.StatusCode == 429)
                        System.Diagnostics.Debug.WriteLine("Too many requests");
                else
                {
                        System.Diagnostics.Debug.WriteLine(await response.Content.ReadAsStringAsync());
                    return default;
                }

            }
            catch (JsonReaderException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }
            catch (WebException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }
            catch(JsonSerializationException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }

            return default;
        }
        private string GetQuery(IDictionary<string, object> args)
        {
            if (args == null) return null;

            var query = HttpUtility.ParseQueryString(string.Empty);

            foreach (var arg in args)
            {
                if (arg.Value is double number)                
                    query[arg.Key] = number.ToString(CultureInfo.InvariantCulture);
                
                else
                    query[arg.Key] = arg.Value?.ToString();
                
            }

            return query.ToString();
        }
        private static HttpClient GetClientWithHeaders()
        {
            var client = new HttpClient()
            {
                BaseAddress = new Uri(App.AirlyApiUrl),
            };

            client.DefaultRequestHeaders.Add("Accept", "application/json");
            client.DefaultRequestHeaders.Add("Accept-Encoding", "gzip");
            client.DefaultRequestHeaders.Add("apikey", App.AirlyApiKey);

            return client;
        }
        private string GetAirlyApiUrl(string path, string query)
        {
            var builder = new UriBuilder(App.AirlyApiUrl)
            {
                Port = -1,
                Query = query
            };
            builder.Path += path;

            return builder.ToString();
        }

        private async Task<Location> GetLocation()
        {
            try
            {
                return await Geolocation.GetLastKnownLocationAsync()
                   ?? await Geolocation.GetLocationAsync(new GeolocationRequest(GeolocationAccuracy.Medium));
            }
            catch (FeatureNotSupportedException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }
            catch (FeatureNotEnabledException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }
            catch (PermissionException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }

            return null;
        }
    
}
}

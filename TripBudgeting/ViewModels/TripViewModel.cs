using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.EntityFrameworkCore;
using TripBudgeting.Models;
using TripBudgeting.Views;

namespace TripBudgeting.ViewModels
{
    public class TripViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Trip> Trips { get; set; }
        public ICommand AddTripCommand { get; set; }
        public ICommand RefreshCommand { get; set; }

        private bool isRefreshing;
        public bool IsRefreshing
        {
            get => isRefreshing;
            set
            {
                if (isRefreshing != value)
                {
                    isRefreshing = value;
                    OnPropertyChanged();
                }
            }
        }

        private Trip selectedTrip;
        public Trip SelectedTrip
        {
            get => selectedTrip;
            set
            {
                if (selectedTrip != value)
                {
                    selectedTrip = value;
                    OnPropertyChanged();
                    if (selectedTrip != null)
                    {
                        // Navigate to TripDetailPage with the selected trip
                        Shell.Current.GoToAsync(nameof(TripDetailPage),true,
                            new Dictionary<string, object>
                            {
                                [nameof(Trip)] = selectedTrip
                            }) ;
                    }
                }
            }
        }

        public TripViewModel()
        {
            Trips = new ObservableCollection<Trip>();
            AddTripCommand = new Command(AddTrip);
            RefreshCommand = new Command(async () => await RefreshTrips());

            LoadTrips();
        }

        private void LoadTrips()
        {
            try
            {
                using (var db = new TripBudgetContext())
                {
                    var trips = db.Trips.Include(t => t.Expenses).ToList(); // Remove the reference to Budget
                    foreach (var trip in trips)
                    {
                        Trips.Add(trip);
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception
                System.Diagnostics.Debug.WriteLine($"LoadTrips failed: {ex.Message}");
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
        }

        private async Task RefreshTrips()
        {
            IsRefreshing = true;

            // Simulate a data refresh
            await Task.Delay(2000);

            Trips.Clear();
            LoadTrips();

            IsRefreshing = false;
        }

        private async void AddTrip()
        {
            // Navigate to AddTripPage
            await Shell.Current.GoToAsync(nameof(AddTripPage));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

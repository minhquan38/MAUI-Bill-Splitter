using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using TripBudgeting.Models;

namespace TripBudgeting.ViewModels
{
    public class AddTripViewModel : INotifyPropertyChanged
    {
        public string Destination { get; set; }
        public DateTime StartDate { get; set; } = DateTime.Today;
        public DateTime EndDate { get; set; } = DateTime.Today;
        public double InitialBudget { get; set; }
        public string ImagePath { get; set; }

        public ICommand SaveCommand { get; set; }
        public ICommand SelectImageCommand { get; set; }

        public AddTripViewModel()
        {
            SaveCommand = new Command(Save);
            SelectImageCommand = new Command(async () => await SelectImage());
        }

        private async Task SelectImage()
        {
            var result = await MediaPicker.PickPhotoAsync();
            if (result != null)
            {
                ImagePath = result.FullPath;
                OnPropertyChanged(nameof(ImagePath));
            }
        }

        private async void Save()
        {
            var newTrip = new Trip
            {
                Destination = Destination,
                StartDate = StartDate,
                EndDate = EndDate,
                InitialBudget = (decimal)InitialBudget,
                ImagePath = ImagePath
            };

            using (var db = new TripBudgetContext())
            {
                db.Trips.Add(newTrip);
                db.SaveChanges();
            }

            await Shell.Current.GoToAsync("..");
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}

using TripBudgeting.Views;

namespace TripBudgeting
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            // Register routes for navigation
            Routing.RegisterRoute(nameof(AddTripPage), typeof(AddTripPage));
            Routing.RegisterRoute(nameof(TripDetailPage), typeof(TripDetailPage));
        }
    }
}

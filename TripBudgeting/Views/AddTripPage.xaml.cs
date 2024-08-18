using TripBudgeting.ViewModels;

namespace TripBudgeting.Views;

public partial class AddTripPage : ContentPage
{
	public AddTripPage(AddTripViewModel vm)
	{
        InitializeComponent();
        BindingContext = vm;
    }
   
    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
    }
}
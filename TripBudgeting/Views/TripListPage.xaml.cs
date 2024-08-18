using TripBudgeting.ViewModels;

namespace TripBudgeting.Views;

public partial class TripListPage : ContentPage
{
    private TripViewModel viewModel;

    public TripListPage()
    {
        InitializeComponent();
        viewModel = BindingContext as TripViewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (viewModel != null)
        {
            viewModel.RefreshCommand.Execute(null);
        }
    }
}
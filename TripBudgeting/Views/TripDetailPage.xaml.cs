using TripBudgeting.Models;
using TripBudgeting.ViewModels;

namespace TripBudgeting.Views;

public partial class TripDetailPage : ContentPage
{
    public TripDetailPage(TripDetailViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm; 
    }
    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
        if (BindingContext is TripDetailViewModel viewModel)
        {
            viewModel.InitialBudget = (double)viewModel.Trip.InitialBudget;
            viewModel.RemainingBudget = viewModel.InitialBudget;

            viewModel.LoadExpenses();
        }
    }


    private void InputHandler(object sender, TextChangedEventArgs e)
    {
        if (BindingContext is TripDetailViewModel vm)
        {
            string newText = e.NewTextValue;
            string oldText = e.OldTextValue;

            // Validate input
            if (double.TryParse(newText, out double newExpense))
            {
                if (double.TryParse(oldText, out double oldExpense))
                {
                    // Add back the old value before subtracting the new one
                    vm.RemainingBudget += oldExpense;
                }

                // Subtract the new value
                vm.RemainingBudget -= newExpense;
            }
            else if (string.IsNullOrEmpty(newText) && double.TryParse(oldText, out double oldExpense))
            {
                // If the entry is cleared, add the old value back to the remaining budget
                vm.RemainingBudget += oldExpense;
            }
        }
    }

}
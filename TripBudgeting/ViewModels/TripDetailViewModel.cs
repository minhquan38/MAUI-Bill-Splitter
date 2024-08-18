using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using TripBudgeting.Models;

namespace TripBudgeting.ViewModels;
[QueryProperty (nameof(Trip), nameof(Trip))]
public partial class TripDetailViewModel : ObservableObject
{
    private TripBudgetContext _context;

    [ObservableProperty]
    Trip trip;

    [ObservableProperty]
    public double initialBudget;

    [ObservableProperty]
    public double remainingBudget;

    [ObservableProperty]
    public double transportBudget;

    [ObservableProperty]
    public double foodAndDrinkBudget;

    [ObservableProperty]
    public double accomodationBudget;

    [ObservableProperty]
    public double shoppingBudget;

    [ObservableProperty]
    public double tourBudget;

    [ObservableProperty]
    public double otherBudget;

    public ObservableCollection<ExpenseViewModel> Expenses { get; set; }

    public TripDetailViewModel(TripBudgetContext context)
    {
        _context = context;
        Expenses = new ObservableCollection<ExpenseViewModel>();
    }

    [RelayCommand]
    public async Task SaveAllExpenses()
    {
        foreach (var expenseVm in Expenses)
        {
            switch (expenseVm.Description)
            {
                case "Transportation":
                    expenseVm.Amount = (decimal)TransportBudget;
                    break;
                case "Food and Drink":
                    expenseVm.Amount = (decimal)FoodAndDrinkBudget;
                    break;
                case "Accommodation":
                    expenseVm.Amount = (decimal)AccomodationBudget;
                    break;
                case "Shopping":
                    expenseVm.Amount = (decimal)ShoppingBudget;
                    break;
                case "Tour(s)":
                    expenseVm.Amount = (decimal)TourBudget;
                    break;
                case "Other":
                    expenseVm.Amount = (decimal)OtherBudget;
                    break;
            }

            // Check if the entity is already being tracked
            var existingExpense = _context.Expenses.Local.FirstOrDefault(e => e.Id == expenseVm.Expense.Id);

            if (existingExpense != null)
            {
                // Update the existing tracked entity
                _context.Entry(existingExpense).CurrentValues.SetValues(expenseVm.Expense);
            }
            else
            {
                // If it's a new expense
                if (expenseVm.Expense.Id == 0)
                {
                    _context.Expenses.Add(expenseVm.Expense);
                }
                else
                {
                    // If it's an existing expense not tracked, attach and update
                    _context.Expenses.Attach(expenseVm.Expense);
                    _context.Entry(expenseVm.Expense).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }
            }
        }
        try
        {
            _context.SaveChanges();

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
        await Application.Current.MainPage.DisplayAlert("Saved", "The trip was saved successfully", "OK");
    }


    public void LoadExpenses()
    {
       
        var expenses = _context.Expenses.Where(e => e.TripId == Trip.Id).ToList();
        if (Trip.Expenses == null || !Trip.Expenses.Any())
        {
            // Initialize new expenses with specific descriptions
            var categories = new List<string> { "Transportation", "Food and Drink", "Accommodation", "Shopping", "Tour(s)", "Other" };

            foreach (var category in categories)
            {
                var newExpense = new Expense
                {
                    Description = category,
                    TripId = Trip.Id
                };

                Trip.Expenses.Add(newExpense);
                _context.Expenses.Add(newExpense);
                Expenses.Add(new ExpenseViewModel(newExpense));
             }
            _context.SaveChanges();
        }
        else
        {
            // Load existing expenses
            foreach (var expense in Trip.Expenses)
            {
                Expenses.Add(new ExpenseViewModel(expense));
            }
        }


        // Initialize budgets from existing expenses
        TransportBudget = (double)(Expenses.FirstOrDefault(e => e.Description == "Transportation")?.Amount ?? 0);
        FoodAndDrinkBudget = (double)(Expenses.FirstOrDefault(e => e.Description == "Food and Drink")?.Amount ?? 0);
        AccomodationBudget = (double)(Expenses.FirstOrDefault(e => e.Description == "Accommodation")?.Amount ?? 0);
        ShoppingBudget = (double)(Expenses.FirstOrDefault(e => e.Description == "Shopping")?.Amount ?? 0);
        TourBudget = (double)(Expenses.FirstOrDefault(e => e.Description == "Tour(s)")?.Amount ?? 0);
        OtherBudget = (double)(Expenses.FirstOrDefault(e => e.Description == "Other")?.Amount ?? 0);
    }

    [RelayCommand]
    private async Task DeleteTrip()
    {
        if (Trip != null)
        {
            // Detach any existing tracked instances of related Expenses
            var existingExpenses = _context.Expenses.Where(e => e.TripId == Trip.Id).ToList();

            foreach (var expense in existingExpenses)
            {
                _context.Entry(expense).State = Microsoft.EntityFrameworkCore.EntityState.Detached;
            }

            // Remove the trip and all related expenses
            _context.Trips.Remove(Trip);
            try
            {
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        await Shell.Current.GoToAsync("..");
    }
}

public class ExpenseViewModel : INotifyPropertyChanged
{
    private Expense _expense;

    public Expense Expense
    {
        get => _expense;
        set
        {
            if (_expense != value)
            {
                _expense = value;
                OnPropertyChanged();
            }
        }
    }

    public string Description
    {
        get => _expense.Description;
        set
        {
            if (_expense.Description != value)
            {
                _expense.Description = value;
                OnPropertyChanged();
            }
        }
    }

    public decimal Amount
    {
        get => _expense.Amount;
        set
        {
            if (_expense.Amount != value)
            {
                _expense.Amount = value;
                OnPropertyChanged();
            }
        }
    }

    public ExpenseViewModel(Expense expense)
    {
        _expense = expense;
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}

using BillSpliter.ViewModels;
using Plugin.Maui.OCR;

namespace BillSpliter.Views;

public partial class MainPage : ContentPage
{
	public MainPage(MainPageViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}

    protected async override void OnAppearing()
    {
        base.OnAppearing();

        await OcrPlugin.Default.InitAsync();
    }
}
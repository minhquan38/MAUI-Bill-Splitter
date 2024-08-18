using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using TripBudgeting.Models;
using TripBudgeting.ViewModels;
using TripBudgeting.Views;

namespace TripBudgeting
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif
            builder.Services.AddDbContext<TripBudgetContext>();
            builder.Services.AddTransient<AddTripViewModel>();
            builder.Services.AddTransient<TripViewModel>();
            builder.Services.AddTransient<TripDetailViewModel>();

            builder.Services.AddTransient<AddTripPage>();
            builder.Services.AddTransient<TripListPage>();
            builder.Services.AddTransient<TripDetailPage>();

            var dbContext=new TripBudgetContext();
            dbContext.Database.EnsureCreated();
            dbContext.Dispose();
            return builder.Build();
        }
    }
}

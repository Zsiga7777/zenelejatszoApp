using System.Runtime.ExceptionServices;

namespace MusicApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder.UseMauiApp<App>().UseMauiCommunityToolkit(options =>
            {
                options.SetShouldEnableSnackbarOnWindows(true);
            }) .UseMauiCommunityToolkitMarkup()
               .UseFontConfiguration()
               .UseAppConfigurations()
               .UseMauiCommunityToolkitMediaElement( true)
               .UseAppSettingsMapping()
               .UseDIConfiguration()
               .UseMsSqlServer();

            /*  AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            TaskScheduler.UnobservedTaskException += TaskScheduler_UnobservedTaskException;
            AppDomain.CurrentDomain.FirstChanceException += CurrentDomain_FirstChanceException;*/
#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
        private static async void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            FileService fileService = new FileService();
            try
            {
                if (e.ExceptionObject is Exception ex)
                {
                    await fileService.WriteLogsAsync(ex.Message);
                }
            }
            catch (Exception innerEx)
            {
                await fileService.WriteLogsAsync(innerEx.Message);
            }
        }

        private static async void TaskScheduler_UnobservedTaskException(object? sender, UnobservedTaskExceptionEventArgs e)
        {
            FileService fileService = new FileService();
            try
            {
                e.SetObserved(); // Prevents app crash
                await fileService.WriteLogsAsync(e.Exception.Message);
            }
            catch (Exception ex)
            {
                await fileService.WriteLogsAsync(ex.Message);
            }
        }

        private static async void CurrentDomain_FirstChanceException(object? sender, FirstChanceExceptionEventArgs e)
        {
            FileService fileService = new FileService();
            try
            {
                await fileService.WriteLogsAsync(e.Exception.Message);
            }
            catch (Exception innerEx)
            {
                await fileService.WriteLogsAsync(innerEx.Message);
            }
        }
    }
}

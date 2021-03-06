/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:ArrivalAlarm"
                           x:Key="Locator" />
  </Application.Resources>

  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using Microsoft.Practices.ServiceLocation;

namespace ArrivalAlarm.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            ConfigureNavigationService();

            SimpleIoc.Default.Register<MapViewModel>();
            SimpleIoc.Default.Register<MainViewModel>();
        }

        public MainViewModel Main => ServiceLocator.Current.GetInstance<MainViewModel>();

        public MapViewModel Map => ServiceLocator.Current.GetInstance<MapViewModel>();

        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }

        /// <summary>
        /// Creates and configures instance of navigation service
        /// </summary>
        private void ConfigureNavigationService()
        {
            // Create navigation service object
            NavigationService navigationService = new NavigationService();

            // Add views to navigation service
            navigationService.Configure(nameof(View.MainPage), typeof(View.MainPage));
            navigationService.Configure(nameof(View.MapPage), typeof(View.MapPage));

            // Register navigation service (object)
            SimpleIoc.Default.Register<INavigationService>(() => navigationService);
        }
    }
}
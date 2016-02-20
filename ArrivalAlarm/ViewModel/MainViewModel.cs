using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Windows.Input;
using GalaSoft.MvvmLight.Views;
using GalaSoft.MvvmLight.Ioc;
using System;
using Microsoft.Practices.ServiceLocation;

namespace ArrivalAlarm.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase, INavigable
    {
        INavigationService _navigationService;

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            _navigationService = ServiceLocator.Current.GetInstance<INavigationService>();

            CreateNavigateToSelectLocationPageCommand();
        }

        /// <summary>
        /// Returns command which navigates to selection page
        /// </summary>
        public ICommand NavigateToSelectLocationPage => _navigateToSelectLocationPage;

        private RelayCommand _navigateToSelectLocationPage;

        private void ExecuteNavigateToLocationPage()
        {
            _navigationService.NavigateTo(nameof(View.MapPage), "Graf acykliczny - mo¿e byæ reprezentowany jako drzewo.");
        }

        private void CreateNavigateToSelectLocationPageCommand()
        {
            _navigateToSelectLocationPage = new RelayCommand(ExecuteNavigateToLocationPage);
        }

        #region INavigable
        public void OnNavigatedTo(object parameter)
        {
           //Common.Logger.CreateLoggerAsync();
        }

        public void OnNavigatedFrom(object parameter)
        {

        }

        public void GoBack()
        {
            _navigationService.GoBack();
        }
        #endregion
    }
}
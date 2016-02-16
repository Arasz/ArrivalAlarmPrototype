using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArrivalAlarm.ViewModel
{
    public class MapViewModel: ViewModelBase, INavigable
    {
        private INavigationService _navigationService;

        public MapViewModel()
        {
            _navigationService = ServiceLocator.Current.GetInstance<INavigationService>();
        }

        public string MyProperty { get; set; }

        public void GoBack()
        {
            _navigationService.GoBack();
        }

        #region INavigable 
        public void OnNavigatedFrom(object parameter)
        {
            //MyProperty = (parameter as string) ?? string.Empty;
        }

        public void OnNavigatedTo(object parameter)
        {
            MyProperty = (parameter as string) ?? string.Empty;
        }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArrivalAlarm.ViewModel
{
    interface INavigable
    {
        void OnNavigatedTo(object parameter);
        void OnNavigatedFrom(object parameter);
        void GoBack();
    }
}

namespace ArrivalAlarm.ViewModel
{
    internal interface INavigable
    {
        void OnNavigatedTo(object parameter);

        void OnNavigatedFrom(object parameter);

        void GoBack();
    }
}
namespace ArrivalAlarm.ViewModel
{
    internal interface INavigable
    {
        void GoBack();

        void OnNavigatedFrom(object parameter);

        void OnNavigatedTo(object parameter);
    }
}
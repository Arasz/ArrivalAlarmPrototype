﻿using Windows.Phone.UI.Input;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace ArrivalAlarm.Common
{
    /// <summary>
    /// Page implementation with navigation event handlers consistent with MVVM pattern
    /// </summary>
    public class BindablePage : Page
    {
        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            HardwareButtons.BackPressed += HardwareButtons_BackPressed;

            var navigableViewModel = DataContext as ViewModel.INavigable;

            navigableViewModel?.OnNavigatedTo(e.Parameter);
        }

        private void HardwareButtons_BackPressed(object sender, BackPressedEventArgs e)
        {
            var navigableViewModel = DataContext as ViewModel.INavigable;

            e.Handled = true;
            navigableViewModel?.GoBack();
        }

        /// <summary>
        /// Invoked when this page is about to be removed from Frame
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);

            HardwareButtons.BackPressed -= HardwareButtons_BackPressed;

            var navigableViewModel = DataContext as ViewModel.INavigable;

            navigableViewModel?.OnNavigatedFrom(e.Parameter);
        }
    }
}
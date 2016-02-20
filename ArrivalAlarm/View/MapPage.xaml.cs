using ArrivalAlarm.Common;
using ArrivalAlarm.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace ArrivalAlarm.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MapPage : BindablePage
    {
        private MapViewModel _viewModel;  

        public MapPage()
        {
            InitializeComponent();

            _viewModel = DataContext as MapViewModel;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
        }

        async private void mapControl_CenterChanged(Windows.UI.Xaml.Controls.Maps.MapControl sender, object args)
        {
            /// TODO: Re factor this ( is there any need for binding ?)
            await sender.TrySetViewAsync(_viewModel.UserPosition, _viewModel.ZoomLevel, 0, 0, Windows.UI.Xaml.Controls.Maps.MapAnimationKind.Bow);
        }
    }
}

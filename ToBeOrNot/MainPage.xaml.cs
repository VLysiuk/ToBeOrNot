using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using ToBeOrNot.Resources;
using ToBeOrNot.ViewModels;

namespace ToBeOrNot
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var viewModel = DataContext as MainViewModel;

            if (viewModel != null)
                viewModel.NavigatedToCommand.Execute(null);
        }
    }
}
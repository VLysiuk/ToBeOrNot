using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using ToBeOrNot.ViewModels;

namespace ToBeOrNot.Views
{
    public partial class ProsAndConsPage : PhoneApplicationPage
    {
        public ProsAndConsPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var viewModel = DataContext as ProsAndConsViewModel;

            if(viewModel != null)
                viewModel.NavigatedToCommand.Execute(null);
        }
    }
}
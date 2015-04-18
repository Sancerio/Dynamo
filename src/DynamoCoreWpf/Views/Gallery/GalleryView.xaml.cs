using Dynamo.Models;
using Dynamo.ViewModels;
using Dynamo.Wpf.ViewModels.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Dynamo.Wpf.Views.Gallery
{
    /// <summary>
    /// Interaction logic for GalleryView.xaml
    /// </summary>
    public partial class GalleryView : Window
    {
        private GalleryViewModel viewModel = null;

        public GalleryView(DynamoViewModel dynamoViewModel)
        {
            InitializeComponent();

            viewModel = new GalleryViewModel(dynamoViewModel);
            this.DataContext = viewModel;
        }

        private void CloseGallery(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
﻿using Dynamo.Models;
using Dynamo.ViewModels;
using Dynamo.Wpf.ViewModels.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace Dynamo.Wpf.Views.Gallery
{
    /// <summary>
    /// Interaction logic for GalleryView.xaml
    /// </summary>
    public partial class GalleryView : Window
    {
        private GalleryViewModel viewModel = null;
        public GalleryViewModel ViewModel { get { return viewModel; } }
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
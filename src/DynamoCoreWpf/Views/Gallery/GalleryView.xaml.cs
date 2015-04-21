using Dynamo.Models;
using Dynamo.ViewModels;
using Dynamo.Wpf.ViewModels.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace Dynamo.Wpf.Views.Gallery
{
    /// <summary>
    /// Interaction logic for GalleryView.xaml
    /// </summary>
    public partial class GalleryView : UserControl
    {
        private GalleryViewModel viewModel;
        public GalleryViewModel ViewModel { get { return viewModel; } }
        public GalleryView(GalleryViewModel galleryViewModel)
        {
            InitializeComponent();
            DataContext = galleryViewModel;
            viewModel = galleryViewModel;
            this.Loaded += OnGalleryViewLoaded;
        }

        private void OnGalleryViewLoaded(object sender, RoutedEventArgs e)
        {
            viewModel.RequestCloseGallery += GalleryViewModelRequestCloseGallery;
            this.Focus();
        }

        void GalleryViewModelRequestCloseGallery()
        {
            Grid galleryUI = (Grid) this.Parent;
            galleryUI.Visibility = Visibility.Hidden;
            galleryUI.Children.Remove(this);
            viewModel.RequestCloseGallery -= GalleryViewModelRequestCloseGallery;
        }
    }
}
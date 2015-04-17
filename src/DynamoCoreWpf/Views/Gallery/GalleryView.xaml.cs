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
        //private List<GalleryContent> galleryUIContents;
        private List<Ellipse> bullets;
        private List<BitmapImage> galleryImages;
        private int current = 0;
        private GalleryViewModel viewModel = null;

        public GalleryView(DynamoViewModel dynamoViewModel)
        {
            InitializeComponent();

            viewModel = new GalleryViewModel(dynamoViewModel);
            this.DataContext = viewModel;
            bullets = new List<Ellipse>();
            Ellipse el = new Ellipse();
            bullets.Add(el);
            el = new Ellipse()
            {
                Width = 7,
                Height = 7
            };
            bullets.Add(el);
            this.GalleryBullets.ItemsSource = bullets;
            //galleryUIContents = dynamoViewModel.Model.GalleryContents.GalleryUIContents;
            
            galleryImages = new List<BitmapImage>();
            //viewModel = new GalleryViewModel();
            //addImages();
        }

        private void addImages()
        {
            /*for (int i = 0; i < galleryUIContents.Count; i++)
            { 
                string url = galleryUIContents[i].Image;
                Image image = new Image();
                galleryImages.Add(new BitmapImage(new Uri(url)));

                ColumnDefinition col = new ColumnDefinition();
                col.MinWidth = 20;
                col.MaxWidth = 20;
                GalleryBullets.ColumnDefinitions.Add(col);

                Ellipse el = new Ellipse();
                bullets.Add(el);
                el.Width = 7;
                el.Height = 7;
                el.Stroke = Brushes.Black;
                el.Fill = (i == 0) ? Brushes.Gray : Brushes.White;

                GalleryBullets.Children.Add(el);
                Grid.SetColumn(el, i);

            }*/
        }

        private void RightButtonClick(object sender, RoutedEventArgs e)
        {
            /*current = (current + 1) % (galleryImages.Count);
            currentImage.Source = galleryImages[current];
            bullets[((current - 1) + galleryImages.Count) % galleryImages.Count].Fill = Brushes.White;
            bullets[current].Fill = Brushes.Gray;
            GalleryContentTitle.Text = galleryUIContents[current].Header;
            GalleryContentText.Text = galleryUIContents[current].Body;*/
            //model.updateContent();
        }

        private void LeftButtonClick(object sender, RoutedEventArgs e)
        {
            /*current = (current - 1 + galleryImages.Count) % (galleryImages.Count);
            currentImage.Source = galleryImages[current];
            bullets[(current + 1) % galleryImages.Count].Fill = Brushes.White;
            bullets[current].Fill = Brushes.Gray;
            GalleryContentTitle.Text = galleryUIContents[current].Header;
            GalleryContentText.Text = galleryUIContents[current].Body;*/
        }

        private void CloseGallery(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}

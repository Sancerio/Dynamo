﻿using Dynamo.Core;
using Dynamo.Interfaces;
using Dynamo.UI.Commands;
using Dynamo.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using System.Linq;

namespace Dynamo.Wpf.ViewModels.Core
{
    public class GalleryContent : NotificationObject
    {
        public string Header { get; set; }
        public string Body { get; set; }
        public string ImagePath { get; set; }

        private bool isCurrent;
        public bool IsCurrent
        {
            get
            {
                return isCurrent;
            }
            set
            {
                isCurrent = value;
                RaisePropertyChanged("IsCurrent");
            }
        }
    }

    public class GalleryContents
    {
        public List<GalleryContent> GalleryUiContents { get; set; }

        public static GalleryContents Load(string filePath)
        {
            try
            {
                var galleryContents = new GalleryContents();
                var serializer = new XmlSerializer(typeof(GalleryContents));
                using (var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    galleryContents = serializer.Deserialize(fs) as GalleryContents;
                    fs.Close(); // Release file lock
                }
                return galleryContents;
            }
            catch (Exception)
            {
                return new GalleryContents();
            }
        }
    }

    public class GalleryViewModel: ViewModelBase
    {
        #region public members
        public string CurrentImagePath { get { return (currentContent == null) ? string.Empty : currentContent.ImagePath; } }
        public string CurrentHeader { get { return (currentContent == null) ? string.Empty : currentContent.Header; } }
        public string CurrentBody { get { return (currentContent == null) ? string.Empty : currentContent.Body; } }
        public IEnumerable<GalleryContent> Contents { get { return contents; } }
        public DelegateCommand MoveNextCommand { get; set; }
        public DelegateCommand MovePrevCommand { get; set; }
        public DelegateCommand CloseGalleryCommand { get; set; }
        #endregion

        public GalleryViewModel(DynamoViewModel dynamoViewModel) 
        {
            dvm = dynamoViewModel;
            var pathManager = dynamoViewModel.Model.PathManager;
            var galleryFilePath = pathManager.GalleryFilePath;
            var galleryDirectory = pathManager.GalleryDirectory;
            contents = GalleryContents.Load(galleryFilePath).GalleryUiContents;

            if (contents != null)
            {
                //Set image path relative to gallery Directory
                foreach (GalleryContent content in contents)
                {
                    content.ImagePath = Path.Combine(galleryDirectory, content.ImagePath);
                }

                currentContent = contents.FirstOrDefault();

                if (currentContent != null) //if contents is not empty
                {
                    currentContent.IsCurrent = true;
                    isAnyContent = true;
                }

                MoveNextCommand = new DelegateCommand(MoveNext, CanMoveNext);
                MovePrevCommand = new DelegateCommand(MovePrev, CanMovePrev);
                CloseGalleryCommand = new DelegateCommand(CloseGallery,CanCloseGallery);
            }
        }

        #region event handlers
        internal event RequestCloseGalleryHandler RequestCloseGallery;
        internal virtual void OnRequestCloseGallery()
        {
            if (RequestCloseGallery != null)
            {
                RequestCloseGallery();
            }
        }

        internal void CloseGallery(object parameters)
        {
            //forward CloseGallery to DynamoViewModel
            dvm.CloseGalleryCommand.Execute(null);
        }

        internal bool CanCloseGallery(object parameters)
        {
            return true;
        }

        internal void MoveNext(object parameters)
        {
            MoveIndex(true);
        }

        internal bool CanMoveNext(object parameters)
        {
            return true;
        }

        internal void MovePrev(object parameters)
        {
            MoveIndex(false);
        }

        internal bool CanMovePrev(object parameters)
        {
            return true;
        }
        #endregion

        /// <summary>
        /// Move the currentIndex of the Gallery Bullets
        /// </summary>
        /// <param name="forward">
        /// true for moving right
        /// false for moving left
        /// </param>
        private void MoveIndex(bool forward)
        {
            contents[currentIndex].IsCurrent = false;
            currentIndex = (currentIndex + (forward? 1:-1) + contents.Count) % (contents.Count);
            contents[currentIndex].IsCurrent = true;
            currentContent = contents[currentIndex];
            
            RaisePropertyChanged("CurrentImagePath");
            RaisePropertyChanged("CurrentHeader");
            RaisePropertyChanged("CurrentBody");
        }

        public static bool IsAnyContent { get {return isAnyContent;} }

        #region private fields
        private DynamoViewModel dvm;
        private GalleryContent currentContent;
        private List<GalleryContent> contents;
        private int currentIndex = 0;
        private static bool isAnyContent = false;
        #endregion
    }
}

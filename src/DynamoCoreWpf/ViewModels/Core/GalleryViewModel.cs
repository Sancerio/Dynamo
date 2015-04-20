using Dynamo.Core;
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
            if (string.IsNullOrEmpty(filePath) || (!File.Exists(filePath)))
                return null;

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
                return null;
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
        #endregion

        public GalleryViewModel(DynamoViewModel dynamoViewModel) 
        {          
            var pathManager = dynamoViewModel.Model.PathManager;
            var galleryFilePath = pathManager.GalleryFilePath;
            var galleryDirectory = pathManager.GalleryDirectory;
            contents = GalleryContents.Load(galleryFilePath).GalleryUiContents;

            if (contents != null)
            {
                currentContent = contents.FirstOrDefault();

                if(currentContent != null) //if contents is empty
                    currentContent.IsCurrent = true;

                MoveNextCommand = new DelegateCommand(MoveNext, CanMoveNext);
                MovePrevCommand = new DelegateCommand(MovePrev, CanMovePrev);
            }
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

        #region private fields
        private GalleryContent currentContent;
        private List<GalleryContent> contents;
        private int currentIndex = 0;
        #endregion
    }
}

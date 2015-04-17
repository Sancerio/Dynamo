using Dynamo.UI.Commands;
using Dynamo.ViewModels;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Dynamo.Wpf.ViewModels.Core
{
    public class GalleryViewModel: ViewModelBase
    {
        #region public members
        public string CurrentImage { get { return currentContent.Image; } }
        public string CurrentHeader { get { return currentContent.Header; } }
        public string CurrentBody { get { return currentContent.Body; } }
        public DelegateCommand MoveNextCommand { get; set; }
        public DelegateCommand MovePrevCommand { get; set; }
        #endregion

        public GalleryViewModel(DynamoViewModel dynamoViewModel) { 
            
            contents = dynamoViewModel.Model.GalleryContents.GalleryUIContents;

            currentContent = new GalleryContent();
            if (contents != null)
            {
                currentContent.Image = contents[0].Image;
                currentContent.Header = contents[0].Header;
                currentContent.Body = contents[0].Body;
            }
            MoveNextCommand = new DelegateCommand(MoveNext, CanMoveNext);
            MovePrevCommand = new DelegateCommand(MovePrev, CanMovePrev);
        }

        public void MoveNext(object parameters)
        {
            currentIndex = (currentIndex + 1) % (contents.Count);

            currentContent.Image = contents[currentIndex].Image;
            currentContent.Header = contents[currentIndex].Header;
            currentContent.Body = contents[currentIndex].Body;
            
            RaisePropertyChanged("CurrentImage");
            RaisePropertyChanged("CurrentHeader");
            RaisePropertyChanged("CurrentBody");
        }

        internal bool CanMoveNext(object parameters)
        {
            return true;
        }

        public void MovePrev(object parameters)
        {
            currentIndex = (currentIndex - 1 + contents.Count) % (contents.Count);

            currentContent.Image = contents[currentIndex].Image;
            currentContent.Header = contents[currentIndex].Header;
            currentContent.Body = contents[currentIndex].Body;

            RaisePropertyChanged("CurrentImage");
            RaisePropertyChanged("CurrentHeader");
            RaisePropertyChanged("CurrentBody");
        }

        internal bool CanMovePrev(object parameters)
        {
            return true;
        }

        #region private fields
        private GalleryContent currentContent;
        private List<GalleryContent> contents;
        private int currentIndex = 0;
        #endregion
    }
}

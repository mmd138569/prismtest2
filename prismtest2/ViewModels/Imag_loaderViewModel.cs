using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using Prism.Dialogs;
using WINFORM = System.Windows.Forms;
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
using System.Windows.Media;
using prismtest2.Models.Entity;
using System.Windows.Media.Imaging;

namespace prismtest2.ViewModels
{
    public class Imag_loaderViewModel : BindableBase
    {
        public DelegateCommand folder_selection { get; set; }
        private IRegionManager _regionManager;
        public Imag_loaderViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            folder_selection = new DelegateCommand(OnClick);
        }
        public void OnClick()
        {
            WINFORM.FolderBrowserDialog dialog = new WINFORM.FolderBrowserDialog();
            dialog.InitialDirectory = "C:\\Users\\AFRACO\\Source\\Repos\\prismtest2\\prismtest2";
            WINFORM.DialogResult result = dialog.ShowDialog();
            if (result == WINFORM.DialogResult.OK)
            {
                string folder = dialog.SelectedPath;
                ImageCollection = LoadImagesFromFolder(folder);
                //write this in the selected item
                var imageFiles = Directory.GetFiles(folder, "*.*", SearchOption.AllDirectories);

                image = new BitmapImage(new Uri(imageFiles[myImages]));

            }
        }
        private int _myImages;
        public int myImages
        {
            get { return _myImages; }
            set { SetProperty(ref _myImages, value); }
        }
        private ObservableCollection<My_Image> _imageCollection;

        public ObservableCollection<My_Image> ImageCollection
        {
            get => _imageCollection;
            set => SetProperty(ref _imageCollection, value);
        }
        private ImageSource _image;
        public ImageSource image
        {
            get { return _image; }
            set { SetProperty(ref _image, value); }
        }

        private ObservableCollection<My_Image> LoadImagesFromFolder(string folderPath)
        {
            var imageFiles = Directory.GetFiles(folderPath, "*.*", SearchOption.AllDirectories)
                .Where(file => IsImageFile(file))
                .Select(file => new My_Image { ImagePath = file });
           // var image = imageFiles;
            return new ObservableCollection<My_Image>(imageFiles);
        }
        
        private bool IsImageFile(string filePath)
        {
            var extension = Path.GetExtension(filePath).ToLower();
            return extension == ".jpg" || extension == ".png" || extension == ".bmp";
        }
    }
}

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
using Microsoft.VisualBasic.Logging;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using System.Text.RegularExpressions;

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
            sharpening = new DelegateCommand(sharpeninginClick);

        }
        public DelegateCommand sharpening { get; set; }
        public void sharpeninginClick()
        {
            //Image<Bgr, byte> image1 = new Image<Bgr, byte>(image);
            //Image<Gray, byte> grayImage = image1.Convert<Gray, byte>();
            Image<Gray, byte> inputImage = new Image<Gray, byte>(image);
            Image<Gray, byte> sharpenedImage=Sharpen(inputImage, 601, 321, 1.5, 1.5, 2);
           
                CvInvoke.Imshow("sharpenedImage",inputImage);
                // Your code to save the image here
                // For example:
                // sharpenedImage.Save("output_image.jpg");
           
        }
        public static Image<Gray, byte> Sharpen(Image<Gray, byte> image, int w, int h, double sigma1, double sigma2, int k)
        {
            // Ensure odd window sizes
            w = (w % 2 == 0) ? w - 1 : w;
            h = (h % 2 == 0) ? h - 1 : h;

            // Apply Gaussian smoothing
            var gaussianSmooth = image.SmoothGaussian(w, h, sigma1, sigma2);

            // Create the mask by subtracting the smoothed image from the original
            var mask = image - gaussianSmooth;

            // Multiply the mask by the weighted value 'k'
            mask *= k;

            // Add the mask back to the original image
            image += mask;

            return image;
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


            }
        }
        private My_Image _myImages;
        public My_Image myImages
        {
            get { return _myImages; }
            set
            {
                if (SetProperty(ref _myImages, value))
                {
                    // Update the selected document's image
                    image = _myImages?.ImagePath; // Replace with your logic
                }
            }
        }
        private string _image;
        public string image
        {
            get => _image;
            set => SetProperty(ref _image, value);
        }

        private ObservableCollection<My_Image> _imageCollection;

        public ObservableCollection<My_Image> ImageCollection
        {
            get => _imageCollection;
            set => SetProperty(ref _imageCollection, value);
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
        private string fieldName;
        public string PropertyName
        {
            get { return fieldName; }
            set { SetProperty(ref fieldName, value); }
        }
    }
}

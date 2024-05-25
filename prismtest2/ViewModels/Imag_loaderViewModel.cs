using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using Prism.Dialogs;
using WINFORM = System.Windows.Forms;
using System.Collections.ObjectModel;
using System.Diagnostics.Metrics;
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
using System.Diagnostics;
using Microsoft.OData.UriParser;
using Prism.Navigation;

namespace prismtest2.ViewModels
{
    public class Imag_loaderViewModel : BindableBase
    {
        public Gray pixelValue;
        public DelegateCommand folder_selection { get; set; }
        private IRegionManager _regionManager;
        public ObservableCollection<double> listofinputs;
        public Imag_loaderViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            folder_selection = new DelegateCommand(OnClick);
            Conterast = new DelegateCommand(ContrastClick);
            sharpening = new DelegateCommand(sharpeninginClick);
            Brightness = new DelegateCommand(BrightnessClick);
            HistogramPage = new DelegateCommand(OnHistogramClick);

        }
        public DelegateCommand HistogramPage { set; get; }
        public void OnHistogramClick()
        {
            if (image != null)
            {
                using (Image<Gray, byte> inputImages = new Image<Gray, byte>(image))
                {
                    listofinputs = new ObservableCollection<double>();
                    var parameter = new NavigationParameters();

                    var doubleValues = new List<double>();
                    for (int y = 0; y < inputImages.Height; y++)
                    {

                        for (int x = 0; x < inputImages.Width; x++)
                        {
                            pixelValue = inputImages[y, x]; // Access the pixel value
                                                            // Perform further analysis or computations based on the pixel value
                                                            //Trace.WriteLine("=====================================");
                                                            //Trace.WriteLine(pixelValue);
                            double doubleValue = pixelValue.Intensity;
                            //listofinputs.Add(doubleValue);
                            doubleValues.Add(doubleValue);
                        }
                    }

                    parameter.Add("mydata", doubleValues);

                    // PrismUserControl2ViewModel prismUser = new PrismUserControl2ViewModel(list of inputs);
                    //we should not navigate the view and then send the data ,but we should use INavigate
                    _regionManager.RequestNavigate("ContentRegion", "PrismUserControl2", parameter);

                }
            }
            // Console.WriteLine(listofinputs);


        }
        public DelegateCommand sharpening { get; set; }
        public void sharpeninginClick()
        {
            //Image<Bgr, byte> image1 = new Image<Bgr, byte>(image);
            //Image<Gray, byte> grayImage = image1.Convert<Gray, byte>();
            if (image != null)
            {
                using (Image<Bgr, byte> inputImage = new Image<Bgr, byte>(image))
                {
                    using (Image<Bgr, byte> sharpenedImage = Sharpen(inputImage, 601, 321, 1.5, 1.5, 2))
                    {
                        CvInvoke.Imshow("sharpenedImage", sharpenedImage);
                    }
                }
                // Your code to save the image here
                // For example:
                // sharpenedImage.Save("output_image.jpg");
            }

        }
        public DelegateCommand Conterast { set; get; }
        public void ContrastClick()
        {
            if (image != null)
            {
                using (Image<Bgr, byte> inputImage = new Image<Bgr, byte>(image))
                {
                    inputImage._EqualizeHist();
                    inputImage._GammaCorrect(1.5d);
                    CvInvoke.Imshow("sharpenedImage", inputImage);
                }
            }
        }
        public DelegateCommand Brightness { set; get; }

        public void BrightnessClick()
        {
            if (image != null)
            {
                using (Image<Bgr, byte> inputImage = new Image<Bgr, byte>(image))
                {
                    double brightnessFactor = 150;
                    inputImage._Mul(1 + brightnessFactor / 255.0);
                    CvInvoke.Imshow("sharpenedImage", inputImage);
                }
            }
        }
        public static Image<Bgr, byte> Sharpen(Image<Bgr, byte> image, int w, int h, double sigma1, double sigma2, int k)
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
            using (WINFORM.FolderBrowserDialog dialog = new WINFORM.FolderBrowserDialog())
            {
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

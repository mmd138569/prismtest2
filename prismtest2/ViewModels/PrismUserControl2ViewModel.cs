using Emgu.CV.Structure;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using prismtest2.ViewModels;
using Telerik.Windows.Controls;
using Telerik.Windows.Documents.Spreadsheet.Expressions.Functions;
using Emgu.CV;
using SharpDX.Direct2D1.Effects;
using DelegateCommand = Prism.Commands.DelegateCommand;

namespace prismtest2.ViewModels
{
    public class PrismUserControl2ViewModel : BindableBase,INavigationAware
    {
        
        private IRegionManager _regionManager;
        public ObservableCollection<double> listofInputs { get; set; }
        public DelegateCommand prevpage { get; set; }

        public PrismUserControl2ViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            //Trace.WriteLine(listofinputs);
            prevpage = new DelegateCommand(prevbtn);

        }

        public ObservableCollection<double> ArrivalsPerHour1;

        //private static Random rnd = new Random(1234567890);

        // public ObservableCollection<int> ArrivalsPerHour { get; set; }
        private ObservableCollection<double> _ArrivalsPerHour;

        public ObservableCollection<double> ArrivalsPerHour
        {
            get
            {
                return _ArrivalsPerHour;
            }
            set
            {
                SetProperty(ref _ArrivalsPerHour, value);
            }
        }

        public void prevbtn()
        {
            _regionManager.RequestNavigate("ContentRegion", "Imag_loader");
        }
        public ObservableCollection<PrismUserControl2ViewModel.ScatterBarInfo> ArrivalsPerHourAlternative { get; set; }
        public ObservableCollection<double> Ticks { get; set; }



        public class ScatterBarInfo
        {
            public double HorizontalLow { get; set; }
            public double HorizontalHigh { get; set; }
            public double VerticalLow { get; set; }
            public double VerticalHigh { get; set; }

            public ScatterBarInfo(double horizontalLow, double horizontalHigh, double verticalLow, double verticalHigh)
            {
                VerticalLow = verticalLow;
                VerticalHigh = verticalHigh;
                HorizontalLow = horizontalLow;
                HorizontalHigh = horizontalHigh;
            }
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            //listofInputs = listofinputs;
            ArrivalsPerHour = new ObservableCollection<double>();
            ArrivalsPerHour1 = new ObservableCollection<double>();

            listofInputs = new ObservableCollection<double>();
            var listofinputs = (List<double>)navigationContext.Parameters["mydata"];
            listofInputs.AddRange(listofinputs);
            double[] a = new double[256];
            for (int i = 0; i < 256; i++)
            {
                a[i] = 0;
            }
            for (int hour = 0; hour < listofinputs.Count; hour++)
            {
                if (listofInputs.Count != 0)
                {
                    int convertor = (int)listofInputs[hour];
                    a[convertor] = a[convertor] + 1;
                }
            }

          //  if (a[6] != 0)
          //  {
               ArrivalsPerHour1.AddRange(a);
            //}

            for (int hour = 0; hour < ArrivalsPerHour1.Count; hour++)
            { 
                for (int k = 0; k < ArrivalsPerHour1[hour]; k++)
                {
                    ArrivalsPerHour.Add(hour);
                }
            }
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {

        }
    }
}

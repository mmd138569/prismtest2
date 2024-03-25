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

namespace prismtest2.ViewModels
{
    public class PrismUserControl2ViewModel : BindableBase
    {
        private IRegionManager _regionManager;
        public ObservableCollection<Gray> listofInputs;

        public PrismUserControl2ViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            //Trace.WriteLine(listofinputs);
        }


        private static Random rnd = new Random(1234567890);

        public ObservableCollection<double> ArrivalsPerHour { get; set; }
        public ObservableCollection<PrismUserControl2ViewModel.ScatterBarInfo> ArrivalsPerHourAlternative { get; set; }
        public ObservableCollection<double> Ticks { get; set; }


        public PrismUserControl2ViewModel(ObservableCollection<double> listofinputs)
        {
            //listofInputs = listofinputs;
            ArrivalsPerHour = new ObservableCollection<double>();
            ArrivalsPerHour.AddRange(listofinputs);

            for (int hour = 0; hour <= 255; hour++)
            {
                int itemsCount = rnd.Next(1, 5);
                if (hour >= 8 && hour < 16)
                {
                    itemsCount = rnd.Next(5, 10);
                }

                for (int k = 0; k < itemsCount; k++)
                {
                    ArrivalsPerHour.Add(hour);
                }
            }

            ArrivalsPerHourAlternative = new ObservableCollection<PrismUserControl2ViewModel.ScatterBarInfo>();
            ArrivalsPerHourAlternative.Add(new PrismUserControl2ViewModel.ScatterBarInfo(0, 25, 0, 300000));
            ArrivalsPerHourAlternative.Add(new PrismUserControl2ViewModel.ScatterBarInfo(25, 50, 0, 2000000));
            ArrivalsPerHourAlternative.Add(new PrismUserControl2ViewModel.ScatterBarInfo(50, 70, 0, 2000000));
            ArrivalsPerHourAlternative.Add(new PrismUserControl2ViewModel.ScatterBarInfo(70, 90, 0, 2000000));
            ArrivalsPerHourAlternative.Add(new PrismUserControl2ViewModel.ScatterBarInfo(90, 120, 0, 2000000));
            ArrivalsPerHourAlternative.Add(new PrismUserControl2ViewModel.ScatterBarInfo(120, 150, 0, 2000000));
            ArrivalsPerHourAlternative.Add(new PrismUserControl2ViewModel.ScatterBarInfo(150, 200, 0, 2000000));
            ArrivalsPerHourAlternative.Add(new PrismUserControl2ViewModel.ScatterBarInfo(200, 220, 0, 2000000));
            ArrivalsPerHourAlternative.Add(new PrismUserControl2ViewModel.ScatterBarInfo(220, 230, 0, 2000000));
            ArrivalsPerHourAlternative.Add(new PrismUserControl2ViewModel.ScatterBarInfo(230, 240, 0, 2000000));
            ArrivalsPerHourAlternative.Add(new PrismUserControl2ViewModel.ScatterBarInfo(0, 250, 0, 2000000));

            Ticks = new ObservableCollection<double>();
            Ticks.Add(ArrivalsPerHourAlternative[0].HorizontalLow);
            foreach (var item in ArrivalsPerHourAlternative)
            {
                Ticks.Add(item.HorizontalHigh);
            }
        }

        public class ScatterBarInfo
        {
            public double HorizontalLow { get; set; }
            public double HorizontalHigh { get; set; }
            public double VerticalLow { get; set; }
            public double VerticalHigh { get; set; }

            public ScatterBarInfo(double horizontalLow, double horizontalHigh, double verticalLow, double verticalHigh)
            {
                this.VerticalLow = verticalLow;
                this.VerticalHigh = verticalHigh;
                this.HorizontalLow = horizontalLow;
                this.HorizontalHigh = horizontalHigh;
            }
        }
    }
}

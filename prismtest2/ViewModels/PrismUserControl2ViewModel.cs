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

namespace prismtest2.ViewModels
{
    public class PrismUserControl2ViewModel : BindableBase
    {
        private IRegionManager _regionManager;
        public ObservableCollection<Gray> listofinputs;

        public PrismUserControl2ViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            //Trace.WriteLine(listofinputs);
        }


        private static Random rnd = new Random(1234567890);

        public ObservableCollection<double> ArrivalsPerHour { get; set; }
        public ObservableCollection<PrismUserControl2ViewModel.ScatterBarInfo> ArrivalsPerHourAlternative
        {
            get;
            set;
        }

        public ObservableCollection<double> Ticks { get; set; }


        public PrismUserControl2ViewModel(ObservableCollection<Gray> listofinputs)
        {
            //Trace.WriteLine(listofinputs);
            ArrivalsPerHour = new ObservableCollection<double>();
            for (int hour = 0; hour <= 24; hour++)
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
            ArrivalsPerHourAlternative.Add(new PrismUserControl2ViewModel.ScatterBarInfo(0, 3));
            ArrivalsPerHourAlternative.Add(new PrismUserControl2ViewModel.ScatterBarInfo(0, 8));
            ArrivalsPerHourAlternative.Add(new PrismUserControl2ViewModel.ScatterBarInfo(0, 2));
            ArrivalsPerHourAlternative.Add(new PrismUserControl2ViewModel.ScatterBarInfo(0, 7));
            ArrivalsPerHourAlternative.Add(new PrismUserControl2ViewModel.ScatterBarInfo(0, 3));
            ArrivalsPerHourAlternative.Add(new PrismUserControl2ViewModel.ScatterBarInfo(0, 2));
            ArrivalsPerHourAlternative.Add(new PrismUserControl2ViewModel.ScatterBarInfo(0, 5));
            ArrivalsPerHourAlternative.Add(new PrismUserControl2ViewModel.ScatterBarInfo(0, 1));
            ArrivalsPerHourAlternative.Add(new PrismUserControl2ViewModel.ScatterBarInfo(0, 4));
            ArrivalsPerHourAlternative.Add(new PrismUserControl2ViewModel.ScatterBarInfo(0, 6));
            ArrivalsPerHourAlternative.Add(new PrismUserControl2ViewModel.ScatterBarInfo(0, 10));

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

            public ScatterBarInfo( double verticalLow, double verticalHigh)
            {
                this.VerticalLow = verticalLow;
                this.VerticalHigh = verticalHigh;
            }
        }
    }
}

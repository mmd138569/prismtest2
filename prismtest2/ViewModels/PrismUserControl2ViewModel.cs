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

namespace prismtest2.ViewModels
{
    public class PrismUserControl2ViewModel : BindableBase
    {
        private IRegionManager _regionManager;
        public ObservableCollection<double> listofInputs { get; set; }

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
            listofInputs = new ObservableCollection<double>();

                listofInputs.AddRange(listofinputs);
                int[] a = new int[256];
                for (int i = 0; i < 256; i++)
                {
                    a[i] = 0;
                }
            for (int hour = 0; hour <= listofinputs.Count; hour++)
            {
                //int itemsCount = rnd.Next(1, 5);
                //if (hour >= 8 && hour < 16)
                //{
                //    itemsCount = rnd.Next(5, 10);
                //}

                //for (int k = 0; k < itemsCount; k++)
                //{
                //    ArrivalsPerHour.Add(hour);
                //}
                if (listofInputs.Count != 0)
                {
                    int convertor = (int)listofInputs[hour];
                    a[convertor] = a[convertor] + 1;
                }
            }

            if (a[0] != 0)
            {
                ArrivalsPerHour.AddRange(a);
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

using Emgu.CV.Structure;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;

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
        public PrismUserControl2ViewModel(  ObservableCollection<Gray> listofinputs)
        {
            Trace.WriteLine(listofinputs);
        }
    }
}

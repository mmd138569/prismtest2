using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation.Regions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace prismtest2.ViewModels
{
    public class PrismUserControl2ViewModel : BindableBase
    {
        private IRegionManager _regionManager;

        public PrismUserControl2ViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;

        }
    }
}

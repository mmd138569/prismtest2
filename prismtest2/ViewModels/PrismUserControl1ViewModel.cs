using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation.Regions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace prismtest2.ViewModels
{
    public class PrismUserControl1ViewModel : BindableBase
    {
        private IRegionManager _regionManager;
        public DelegateCommand Login { get; set; }
        public PrismUserControl1ViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            Login = new DelegateCommand(LoginClick);
        }

        private void LoginClick()
        {
            _regionManager.RequestNavigate("ContentRegion", "Imag_loader");
        }
    }
}

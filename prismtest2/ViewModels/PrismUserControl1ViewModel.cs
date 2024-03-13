using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using prismtest2.Models.Services;

namespace prismtest2.ViewModels
{
    public class PrismUserControl1ViewModel : BindableBase
    {
        public AddUser add_User;
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

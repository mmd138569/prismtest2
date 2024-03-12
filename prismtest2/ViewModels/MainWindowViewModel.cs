using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation.Regions;
using System;

namespace prismtest2.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string _title = "Prism Application";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private IRegionManager _regionManager;
        public DelegateCommand Signup { get; set; }
        public DelegateCommand Login { get; set; }

        public MainWindowViewModel(IRegionManager regionManager)
        {
            _regionManager=regionManager;
            Signup = new DelegateCommand(OnClick);
            Login = new DelegateCommand(LoginClick);
        }


        private void LoginClick()
        {
            _regionManager.RequestNavigate("ContentRegion", "Imag_loader");
        }

        public void OnClick()
        {
            _regionManager.RequestNavigate("ContentRegion", "PrismUserControl1");
        }


    }
}

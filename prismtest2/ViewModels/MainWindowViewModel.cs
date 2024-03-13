using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation.Regions;
using System;
using prismtest2.Models.Services;

namespace prismtest2.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        public CreateUser createuser;
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
            createuser = new CreateUser();
        }


        private void LoginClick()
        {
            _regionManager.RequestNavigate("ContentRegion", "Imag_loader");
        }

        public void OnClick()
        {
            _regionManager.RequestNavigate("ContentRegion", "PrismUserControl1");
        }
        private string _checking_username;

        public string checking_username
        {
            get { return _checking_username; }
            set
            {
                if (_checking_username != value)
                {
                    _checking_username = value;
                    RaisePropertyChanged();
                }
            }
        }
        private string _cheking_pasword;

        public string cheking_pasword
        {
            get { return _cheking_pasword; }
            set
            {
                if (_cheking_pasword != value)
                {
                    _cheking_pasword = value;
                    RaisePropertyChanged();
                }
            }
        }



    }
}

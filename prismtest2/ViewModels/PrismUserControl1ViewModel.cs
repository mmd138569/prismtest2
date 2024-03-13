using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using prismtest2.Models.Entity;
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
            //dont forger the create the new instance form your object if you dont you ll got the nullExeption Error
            add_User=new AddUser();
        }
        private string _username_txtbox;

        public string username_txtbox
        {
            get { return _username_txtbox; }
            set
            {
                if (_username_txtbox != value)
                {
                    _username_txtbox = value;
                    RaisePropertyChanged(); 
                }
            }
        }
        private string _password_txtbox;

        public string password_txtbox
        {
            get { return _password_txtbox; }
            set
            {
                if (_password_txtbox != value)
                {
                    _password_txtbox = value;
                    RaisePropertyChanged();
                }
            }
        }
        private string _Email_txtbox;

        public string Email_txtbox
        {
            get { return _Email_txtbox; }
            set
            {
                if (_Email_txtbox != value)
                {
                    _Email_txtbox = value;
                    RaisePropertyChanged();
                }
            }
        }

        private void LoginClick()
        {
            Users users = new Users()
            {
                Username = username_txtbox,
                Email = Email_txtbox,
                Password = password_txtbox,
            };
            add_User.adduser(users);
            _regionManager.RequestNavigate("ContentRegion", "Imag_loader");
        }
    }
}

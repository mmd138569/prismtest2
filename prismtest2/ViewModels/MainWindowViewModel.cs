using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation.Regions;
using System;
using Microsoft.VisualBasic.ApplicationServices;
using prismtest2.Models.Entity;
using prismtest2.Models.Services;
using System.Collections.ObjectModel;
using System.DirectoryServices.ActiveDirectory;
using System.Windows;

namespace prismtest2.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        public static bool accessing = false;
        public CreateUser createuser;
        ObservableCollection<Users> user_list = new ObservableCollection<Users>();
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
            _regionManager = regionManager;
            Signup = new DelegateCommand(OnClick);
            Login = new DelegateCommand(LoginClick);
            createuser = new CreateUser();
        }
        private void LoginClick()
        {
            fillData(checking_username, cheking_pasword);
            if (accessing == true && checking_username!=null&& cheking_pasword!=null)
            {
                _regionManager.RequestNavigate("ContentRegion", "Imag_loader");
            }
            else
            {
                errortxtblock = "THE USER IS NOT EXIST";
                errorvisibility = Visibility.Visible;
            }
        }
        private Visibility _errorvisibility;
        public Visibility errorvisibility
        {
            get { return _errorvisibility; }
            set { SetProperty(ref _errorvisibility, value); }
        }
        private string _errortxtblock;
        public string errortxtblock
        {
            get { return _errortxtblock; }
            set { SetProperty(ref _errortxtblock, value); }
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
        public void fillData(string cheking_username, string cheking_password)
        {
            // employees = EmployeeDataAccess.employees;
            user_list.Clear();
            foreach (var customerlistDTO in createuser.userListDtos())
            {
                Users customer = new Users()
                {
                    Id = customerlistDTO.id,
                    Username = customerlistDTO.User_name,
                    Password = customerlistDTO.Password,
                    Email = customerlistDTO.Email
                };
                if (customer.Password == cheking_password && customer.Username == cheking_username)
                {
                    accessing = true;
                }
                user_list.Add(customer);
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

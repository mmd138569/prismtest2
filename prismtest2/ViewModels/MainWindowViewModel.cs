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
using System.Security.Cryptography;
using SharpDX.Text;
using System.IO;
using System.Linq;
using System.Windows.Input;
using prismtest2.Models.Decrypting;
using Telerik.Windows.Controls;

namespace prismtest2.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        public Decrypt decrypting = new Decrypt();
        public static bool accessing = false;
        public CreateUser createuser;
        ObservableCollection<Users> user_list = new ObservableCollection<Users>();
        private string _title = "Prism Application";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }
        private Prism.Commands.DelegateCommand _minimizeCommand;
        public Prism.Commands.DelegateCommand MinimizeCommand =>

            _minimizeCommand ?? (_minimizeCommand = new Prism.Commands.DelegateCommand(ExecuteMinimizeCommand));

        void ExecuteMinimizeCommand()
        {
            Application.Current.MainWindow.WindowState = WindowState.Minimized;
        }
        private Prism.Commands.DelegateCommand _exitCommand;
        public Prism.Commands.DelegateCommand ExitCommand =>
            _exitCommand ?? (_exitCommand = new Prism.Commands.DelegateCommand(ExecuteExitCommand));

        private IRegionManager _regionManager;
        public Prism.Commands.DelegateCommand Signup { get; set; }
        public ICommand LoginCommand { get; set; }
        public Prism.Commands.DelegateCommand Login { get; set; }
        public MainWindowViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            Signup = new Prism.Commands.DelegateCommand(OnClick);
            LoginCommand = new DelegateCommand<RadPasswordBox>(OnLoginCommandExecuted);
            createuser = new CreateUser();
            RestoreDownCommand = new Prism.Commands.DelegateCommand(RestoreDown);
            Resizer_Height = 510;
            Resizer_width = 310;
        }
        void ExecuteExitCommand()
        {
            Application.Current.Shutdown();
        }
        private void OnLoginCommandExecuted(RadPasswordBox passwordBox)
        {
            fillData(checking_username, passwordBox.Password);
            if (accessing == true && checking_username != null && passwordBox.ToString() != null)
            {
                _regionManager.RequestNavigate("ContentRegion", "Imag_loader");
                errorvisibility = Visibility.Collapsed;
                Resizer_width = 850;
                Resizer_Height = 550;
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
            Resizer_width = 850;
            Resizer_Height = 530;
        }
        private int _Resizer_widthame;
        public int Resizer_width
        {
            get { return _Resizer_widthame; }
            set { SetProperty(ref _Resizer_widthame, value); }
        }
        private int _Resizer_Height;
        public int Resizer_Height
        {
            get { return _Resizer_Height; }
            set { SetProperty(ref _Resizer_Height, value); }
        }
        private string _checking_username = "sdf";

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
            byte[] key = Encoding.UTF8.GetBytes("$Jk!pTq#20hdLA$5");
            //String base64String = "SGVsbG8gV29ybGQ=";
            byte[] iv = Encoding.UTF8.GetBytes("SGVsbG8gV29ybGQ=");   // 16-byte initialization vector

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

                //==================================== NOTE =============================
                //if we use UTF8 it convert to the 24byte and we cant decrypt becuase our input is not valid. actually we should use frombasestring to convert to byte array 
                byte[] data = Convert.FromBase64String(customer.Username);
                
                string decryptedBytes = decrypting.Decryptin(data, key, iv);

                //========================================================================
                byte[] data1 = Convert.FromBase64String(customer.Password);
                string decryptedBytes1 = decrypting.Decryptin(data1, key, iv);


                if (decryptedBytes1 == cheking_password && decryptedBytes == cheking_username)
                {
                    accessing = true;
                }
                user_list.Add(customer);
            }
        }
        private string _cheking_pasword = "df";
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
        private WindowState _windowState = WindowState.Normal;
        public WindowState WindowState
        {
            get { return _windowState; }
            set { SetProperty(ref _windowState, value); }
        }

        public Prism.Commands.DelegateCommand RestoreDownCommand { get; private set; }
        private void RestoreDown()
        {
            WindowState = WindowState == WindowState.Normal ? WindowState.Minimized : WindowState.Normal;
        }
    }
}

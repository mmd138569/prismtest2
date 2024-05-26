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
        private DelegateCommand _minimizeCommand;
        public DelegateCommand MinimizeCommand =>

            _minimizeCommand ?? (_minimizeCommand = new DelegateCommand(ExecuteMinimizeCommand));

        void ExecuteMinimizeCommand()
        {
            Application.Current.MainWindow.WindowState = WindowState.Minimized;
        }
        private DelegateCommand _exitCommand;
        public DelegateCommand ExitCommand =>
            _exitCommand ?? (_exitCommand = new DelegateCommand(ExecuteExitCommand));

        private IRegionManager _regionManager;
        public DelegateCommand Signup { get; set; }
        public DelegateCommand Login { get; set; }
        public MainWindowViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            Signup = new DelegateCommand(OnClick);
            Login = new DelegateCommand(LoginClick);
            createuser = new CreateUser();
            Resizer_Height = 510;
            Resizer_width = 310;
        }
        void ExecuteExitCommand()
        {
            Application.Current.Shutdown();
        }
        private void LoginClick()
        {
            fillData(checking_username, cheking_pasword);
            if (accessing == true && checking_username != null && cheking_pasword != null)
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
                string decryptedBytes = Decrypt(data, key, iv);

                //========================================================================
                byte[] data1 = Convert.FromBase64String(customer.Password);
                string decryptedBytes1 = Decrypt(data1, key, iv);


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
        static string Decrypt(byte[] cipheredtext, byte[] key, byte[] iv)
        {
            string simpletext = String.Empty;
            using (Aes aes = Aes.Create())
            {
                ICryptoTransform decryptor = aes.CreateDecryptor(key, iv);
                using (MemoryStream memoryStream = new MemoryStream(cipheredtext))
                {
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader streamReader = new StreamReader(cryptoStream))
                        {
                            simpletext = streamReader.ReadToEnd();
                        }
                    }
                }
            }
            return simpletext;
        }
    }
}

using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using prismtest2.Models.Entity;
using prismtest2.Models.Services;
using System.Security.Cryptography;
using System.Text;
using System.Collections;

namespace prismtest2.ViewModels
{

    public class PrismUserControl1ViewModel : BindableBase
    {
      //  string secret = "$Jk!pTq#20hdLA$5"; //encryption secret
      public  byte[] key = Encoding.UTF8.GetBytes("$Jk!pTq#20hdLA$5");
     public   byte[] iv = Encoding.UTF8.GetBytes("SGVsbG8gV29ybGQ=");   // 16-byte initialization vector

        public AddUser add_User;
        private IRegionManager _regionManager;
        public DelegateCommand Login { get; set; }
        public PrismUserControl1ViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            Login = new DelegateCommand(LoginClick);
            //dont forger the create the new instance form your object if you dont you ll got the nullExeption Error
            add_User=new AddUser();
            Resizer_width = 810;
            Resizer_Height = 530;
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

            string plainText = username_txtbox; //Text to encode
            byte[] user_plain = Encoding.UTF8.GetBytes(plainText);
            byte[] cipherUsername = Encrypt(user_plain, key, iv);
            string cipherUsernamestr = Convert.ToBase64String(cipherUsername);

            string Emailtxt = Email_txtbox; //Text to encode
            byte[] Emailplain = Encoding.UTF8.GetBytes(Emailtxt);
            byte[] cipherEmail = Encrypt(Emailplain, key, iv);
            string cipherEmailstr = Convert.ToBase64String(cipherEmail);

            string paswordtxt = password_txtbox; //Text to encode
            byte[] passwordplain = Encoding.UTF8.GetBytes(paswordtxt);
            byte[] ciperpassword = Encrypt(passwordplain, key, iv);
            string ciperpasswordstr = Convert.ToBase64String(ciperpassword);

            Users users = new Users()
            {
                Username = cipherUsernamestr,
                Email = cipherEmailstr,
                Password = ciperpasswordstr,
            };
            add_User.adduser(users);
            _regionManager.RequestNavigate("ContentRegion", "Imag_loader");
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
        static byte[] Encrypt(byte[] plainBytes, byte[] key, byte[] iv)
        {
            byte[] encryptedBytes = null;
            // Set up the encryption objects
            using (Aes aes = Aes.Create())
            {
                aes.Key = key;
                aes.IV = iv;
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;

                // Encrypt the input plaintext using the AES algorithm
                using (ICryptoTransform encryptor = aes.CreateEncryptor())
                {
                    encryptedBytes = encryptor.TransformFinalBlock(plainBytes, 0, plainBytes.Length);
                }
            }

            return encryptedBytes;
        }
    }
}

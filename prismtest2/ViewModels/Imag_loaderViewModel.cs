using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using WINFORM = System.Windows.Forms;

namespace prismtest2.ViewModels
{
    public class Imag_loaderViewModel : BindableBase
    {
        public DelegateCommand folder_selection { get; set; }
        private IRegionManager _regionManager;

        public Imag_loaderViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            folder_selection = new DelegateCommand(OnClick);

        }
        public void OnClick()
        {
            WINFORM.FolderBrowserDialog dialog = new WINFORM.FolderBrowserDialog();

            dialog.InitialDirectory = "C:\\Users\\AFRACO\\Source\\Repos\\prismtest2\\prismtest2";
            dialog.ShowDialog();
        }
    }
}

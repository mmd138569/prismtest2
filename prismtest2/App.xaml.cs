using Prism.Ioc;
using prismtest2.Views;
using System.Windows;

namespace prismtest2
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<PrismUserControl1>();
            containerRegistry.RegisterForNavigation<Imag_loader>();
        }
    }
}

using System.Windows;
using GM.UI.Views;
using ZonaTools.XPlorerBar;

namespace GM.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            //ContentControl.Content = new DepartementView();

        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            switch (((XPlorerItem)sender).ItemText)
            {
                    
                case "Départements": ContentControl.Content = new DepartementView();
                    break;
                case "Catégories": ContentControl.Content = new CategorieView();
                    break;
                case "Services": ContentControl.Content = new ServiceView();
                    break;
            }
        }
    }
}

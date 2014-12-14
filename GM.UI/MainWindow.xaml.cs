using BLL;
using GM.Entity.Models;
using Microsoft.Practices.Unity;

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
            var container = new UnityContainer();
            var rep = container.Resolve<Repository<Departement>>();
            DataGrid.ItemsSource = rep.SelectAll();
        }
    }
}

using System.Windows;
using System.Windows.Controls;
using BLL;
using Microsoft.Practices.Unity;

namespace GM.UI.Views
{
    /// <summary>
    /// Interaction logic for MaterielInView.xaml
    /// </summary>
    public partial class MaterielInView
    {
        private static  StockService _StockService;
        public MaterielInView()
        {
            InitializeComponent();
            var container = new UnityContainer();
            _StockService = container.Resolve<StockService>();
            DataGridStock.ItemsSource = _StockService.SelectAll();

        }

        private void AddBonBtn_OnClick(object sender, RoutedEventArgs e)
        {
        //    var frm = new BonEntreeLigneFrm();
        //    frm.Show();
        }

        private void CbCategorie_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void CbSousCategorie_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }
    }
}

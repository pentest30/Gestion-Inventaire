using System.Collections.ObjectModel;
using System.Windows.Controls;
using BLL;
using GM.Entity.Models;
using Microsoft.Practices.Unity;

namespace GM.UI.Views
{
    /// <summary>
    /// Interaction logic for MaterielInView.xaml
    /// </summary>
    public partial class MaterielInView
    {
        private static  StockService _stockService;
        public MaterielInView()
        {
            InitializeComponent();
            var container = new UnityContainer();
            _stockService = container.Resolve<StockService>();
            LoadData();

        }

        private void LoadData()
        {
            DataGridStock.ItemsSource = new ObservableCollection<PieceMagasin>(_stockService.GetAllLazyLoad(x=>x.BonEntree, x=>x.Piece,w=>w.Magasin));
        }

        private void CbCategorie_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void CbSousCategorie_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }
    }
}

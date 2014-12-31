using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using BLL;
using GM.Entity.Models;
using GM.UI.ModelView;
using Microsoft.Practices.Unity;

namespace GM.UI.Views
{
    /// <summary>
    /// Interaction logic for StockView.xaml
    /// </summary>
    public partial class StockView
    {
        public static StockService StockService;
        public StockView()
        {
            InitializeComponent();
            var container = new UnityContainer();
            StockService = container.Resolve<StockService>();
            var magasinRepository = container.Resolve<Repository<Magasin>>();
            CbMagasin.ItemsSource = magasinRepository.SelectAll();
        }

        private void CbMagasin_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CbMagasin.SelectedIndex ==-1)return;
            var magasin = CbMagasin.SelectedItem as Magasin;
            if (magasin== null)return;
            GetData(magasin);
        }

        private void GetData(Magasin magasin)
        {
            var result =
                StockService.GetAllLazyLoad(x => x.Article, x => x.Piece, x => x.Magasin)
                    .Where(x => x.MagasinId == magasin.Id )
                    .ToList();
            var itemsView = new ObservableCollection<StockModelView>();

            if (!result.Any()) return;
            foreach (var pieceMagasin in result) itemsView.Add(AutoMapper.Mapper.Map<StockModelView>(pieceMagasin));
            DataGridStock.ItemsSource = itemsView;
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            
        }
    }
}

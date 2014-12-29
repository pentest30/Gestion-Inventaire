using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using BLL;
using GM.Entity.Models;
using GM.UI.ModelView;
using Microsoft.Practices.Unity;

namespace GM.UI.Views
{
    /// <summary>
    /// Interaction logic for PieceSrviceView.xaml
    /// </summary>
    public partial class PieceSrviceView
    {
        private static  Repository<PieceEmployee> _repository;  
        public PieceSrviceView()
        {
            InitializeComponent();
            var container = new UnityContainer();
            _repository = container.Resolve<Repository<PieceEmployee>>();
            DataGridStock.ItemsSource = LoadData();
        }

        private ICollection<HorsStockView> LoadData()
        {
            var result =new Collection<HorsStockView>();
            var items = _repository.GetAllLazyLoad(x => x.Piece, x => x.Piece.Article, x => x.Service,  x => x.SousService,
                x => x.BonSortie, x => x.Departement).Where(x=>x.Utilisation);
            foreach (var pieceEmployee in items)
            {
                result.Add( AutoMapper.Mapper.Map<HorsStockView>(pieceEmployee));
            }
            return result;
        }

        private void UpdateButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (DataGridStock.SelectedIndex < 0)
            {
                MessageBox.Show("Selectionner un champ", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            var frm = new ChangeServiceFrm(((HorsStockView) DataGridStock.SelectedItem).Id);
            frm.UpdateDg += UpdateDatagrid;
            frm.ShowDialog();
        }

        private void UpdateDatagrid(int id)
        {
            LoadData();
        }
    }
}

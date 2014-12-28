using System.Collections;
using System.Linq;
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

        private IEnumerable LoadData()
        {
            var items = _repository.GetAllLazyLoad(x => x.Piece, x => x.Piece.Article, x => x.Service,  x => x.SousService,
                x => x.BonSortie, x => x.Departement).Where(x=>x.Utilisation);
            if (!items.Any()) yield break;
            foreach (var pieceEmployee in items)
            {
                yield return AutoMapper.Mapper.Map<HorsStockView>(pieceEmployee);
            }
        }
    }
}

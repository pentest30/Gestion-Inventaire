using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;
using BLL;
using GM.Entity.Models;
using GM.UI.ModelView;
using Microsoft.Practices.Unity;

namespace GM.UI.Views
{
    /// <summary>
    /// Interaction logic for MaterielInView.xaml
    /// </summary>
    public partial class MaterielInView
    {
        private static  StockService _stockService;
        private readonly Repository<SousCategorie> _sousCategorieRepository;
        private readonly Repository<Type> _typeRepository;
        private readonly ArticleService _articleService;
        public MaterielInView()
        {
            InitializeComponent();
            var container = new UnityContainer();
            _articleService = container.Resolve<ArticleService>();
            _stockService= container.Resolve<StockService>();
            var marqueRepository = container.Resolve<Repository<Marque>>();
            var categorieRepository = container.Resolve<Repository<Categorie>>();
            _sousCategorieRepository = container.Resolve<Repository<SousCategorie>>();
            _typeRepository = container.Resolve<Repository<Type>>();
            var magasinRepository = container.Resolve<Repository<Magasin>>();
            CbCategorie.ItemsSource = categorieRepository.SelectAll();
            CbMarque.ItemsSource = marqueRepository.SelectAll();
            CbMagasin.ItemsSource = magasinRepository.SelectAll();
           // LoadData();

        }

        private void CbCategorie_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CbCategorie.SelectedIndex == -1) return;
            CbSousCategorie.SelectedIndex = -1;
            var categorie = CbCategorie.SelectedItem as Categorie;
            if (categorie != null)
                CbSousCategorie.ItemsSource = _sousCategorieRepository.Find(x => x.CategorieId == categorie.Id);
        }

        private void CbSousCategorie_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CbSousCategorie.SelectedIndex ==-1)return;
            CbType.SelectedIndex = -1;
            var item = CbSousCategorie.SelectedItem as SousCategorie;
            if (item != null) CbType.ItemsSource = _typeRepository.Find(x => x.SousCategorieId == item.Id);
           
        }

        private void CbType_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CbArticle.SelectedIndex = -1;
        }

        private void CbMaruqe_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CbMarque.SelectedIndex == -1) return;

            var categorie = CbCategorie.SelectedItem as Categorie;
            var sousCategorie = CbSousCategorie.SelectedItem as SousCategorie;
            var marque = CbMarque.SelectedItem as Marque;
            if (sousCategorie != null && categorie != null && marque != null)
                CbArticle.ItemsSource = _articleService.Find(x => x.CategorieId == categorie.Id
                                                                  && x.SousCategorieId == sousCategorie.Id
                                                                  && x.MarqueId == marque.Id);

        }

        private void CbArticle_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CbMagasin.SelectedIndex = -1;
        }

        private void CbMagasin_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CbMagasin.SelectedIndex == -1) return;
            var itemSourceView = new ObservableCollection<StockModelView>();
            var categorie = CbCategorie.SelectedItem as Categorie;
            var sousCategorie = CbSousCategorie.SelectedItem as SousCategorie;
            var marque = CbMarque.SelectedItem as Marque;
            var type = CbType.SelectedItem as Type;
            var article = CbArticle.SelectedItem as Article;
            var magasin = CbMagasin.SelectedItem as Magasin;
            if (sousCategorie == null || categorie == null || marque == null || magasin == null || type == null ||
                article == null) return;
            var result = PieceMagasins(categorie, sousCategorie, type, marque, article, magasin);
            if (ConvertToStockVm(result, itemSourceView)) return;
            DataGridStock.ItemsSource = itemSourceView;
        }

        private static bool ConvertToStockVm(IEnumerable<PieceMagasin> result, ObservableCollection<StockModelView> itemSourceView)
        {
            var pieceMagasins = result as PieceMagasin[] ?? result.ToArray();
            if (!pieceMagasins.Any()) return true;
            foreach (var pieceMagasin in pieceMagasins)
            {
                itemSourceView.Add(AutoMapper.Mapper.Map<StockModelView>(pieceMagasin));
            }
            return false;
        }

        private static IEnumerable<PieceMagasin> PieceMagasins(Categorie categorie, SousCategorie sousCategorie, Type type, Marque marque,
            Article article, Magasin magasin)
        {
            var result = new ObservableCollection<PieceMagasin>(_stockService.GetAllLazyLoad(x => x.BonEntree, x => x.Piece,
                w => w.Magasin))
                .Where(x => x.Article.Categorie.Id == categorie.Id
                            && x.Article.SousCategorie.Id == sousCategorie.Id
                            && x.Article.Type.Id == type.Id
                            && x.Article.Marque.Id == marque.Id
                            && x.Article.Libelle == article.Libelle
                            && x.MagasinId == magasin.Id);
            return result;
        }
    }
}

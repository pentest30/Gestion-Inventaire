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
        private readonly ArticleService _articleService;
        readonly Repository<Service> _serviceRepository;
        public MaterielInView()
        {
            InitializeComponent();
            var container = new UnityContainer();
            _articleService = container.Resolve<ArticleService>();
            _stockService= container.Resolve<StockService>();
            var marqueRepository = container.Resolve<Repository<Marque>>();
            var categorieRepository = container.Resolve<Repository<Categorie>>();
            _sousCategorieRepository = container.Resolve<Repository<SousCategorie>>();
            var beRepository =container.Resolve<Repository<BonSortie>>();
            _serviceRepository=container.Resolve<Repository<Service>>();
            var magasinRepository = container.Resolve<Repository<Magasin>>();
            var departementRepository = container.Resolve<Repository<Departement>>();
            CbCategorie.ItemsSource = categorieRepository.SelectAll();
            CbMarque.ItemsSource = marqueRepository.SelectAll();
            CbMagasin.ItemsSource = magasinRepository.SelectAll();
            CbBonSortie.ItemsSource = beRepository.SelectAll();
            CbDepartement.ItemsSource = departementRepository.SelectAll();
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
            //var type = CbType.SelectedItem as TypeArticle;
            var article = CbArticle.SelectedItem as Article;
            var magasin = CbMagasin.SelectedItem as Magasin;
            if (sousCategorie == null || categorie == null || marque == null || magasin == null ||  article == null) return;
            var result =_stockService. PieceMagasins(categorie, sousCategorie, marque, article, magasin);
            if (ConvertToStockVm(result, itemSourceView)) return;
            DataGridStock.ItemsSource = itemSourceView;
        }
        private static bool ConvertToStockVm(IEnumerable<PieceMagasin> result, ObservableCollection<StockModelView> itemSourceView)
        {
            var pieceMagasins = result as PieceMagasin[] ?? result.ToArray();
            if (!pieceMagasins.Any()) return true;
            foreach (var pieceMagasin in pieceMagasins)itemSourceView.Add(AutoMapper.Mapper.Map<StockModelView>(pieceMagasin));
            return false;
        }

        private void CbDepartement_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CbDepartement.SelectedIndex == -1) return;
            var item = CbDepartement.SelectedItem as Departement;
            if (item != null) CbService.ItemsSource = _serviceRepository.Find(x => x.DepartementId == item.Id);
        }

        private void CbService_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }
    }
}

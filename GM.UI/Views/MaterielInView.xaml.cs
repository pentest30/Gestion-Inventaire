using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using BLL;
using GM.Entity.Models;
using GM.UI.ModelView;
using Microsoft.Practices.Unity;
using WPF.Core.Helpers;

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
        readonly Repository<PieceEmployee> _pieceEmployeeRepository;
        Categorie _categorie ;
        SousCategorie _sousCategorie;
        Marque _marque ;
        //var type = CbType.SelectedItem as TypeArticle;
        Article _article ;
        Magasin _magasin ;
        public MaterielInView()
        {
            InitializeComponent();
            _categorie = new Categorie();
            _sousCategorie = new SousCategorie();
            _marque = new Marque();
            _article = new Article();
            _magasin = new Magasin();
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
            _pieceEmployeeRepository = container.Resolve<Repository<PieceEmployee>>();
            CbCategorie.ItemsSource = categorieRepository.SelectAll();
            CbMarque.ItemsSource = marqueRepository.SelectAll();
            CbMagasin.ItemsSource = magasinRepository.SelectAll();
            CbBonSortie.ItemsSource = beRepository.SelectAll();
            CbDepartement.ItemsSource = departementRepository.SelectAll();

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
             _categorie = CbCategorie.SelectedItem as Categorie;
             _sousCategorie = CbSousCategorie.SelectedItem as SousCategorie;
             _marque = CbMarque.SelectedItem as Marque;
            //var type = CbType.SelectedItem as TypeArticle;
             _article = CbArticle.SelectedItem as Article;
             _magasin = CbMagasin.SelectedItem as Magasin;
            if (_sousCategorie == null || _categorie == null || _marque == null || _magasin == null ||  _article == null) return;
            var result =_stockService. PieceMagasins(_categorie, _sousCategorie, _marque, _article, _magasin);
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

        private void SaveBtn_OnClick(object sender, RoutedEventArgs e)
        {
            var departemet = CbDepartement.SelectedItem as Departement;
            var service = CbService.SelectedItem as Service;
            var sousService = CbSousService.SelectedItem as SousService;
            var bSortie = CbBonSortie.SelectedItem as BonSortie;
            var items = DataGridStock.SelectedItems;
            if (departemet == null || service == null || bSortie == null || items.Count == 0)
            {
                MessageBox.Show("");
                return;
            }
         
            ProgressBar.Minimum = 0;
            ProgressBar.Maximum = items.Count;
            var pBar = new PBar(ProgressBar);
            ProgressBar.Visibility = Visibility.Visible;
            DoWork(items, bSortie, departemet, service, sousService, pBar);
            var tmp = _stockService.PieceMagasins(_categorie, _sousCategorie, _marque, _article, _magasin);
            var itemSourceView = new ObservableCollection<StockModelView>();
            ConvertToStockVm(tmp, itemSourceView);
            DataGridStock.ItemsSource = itemSourceView;
            DataGridDist.ItemsSource = new ObservableCollection<PieceEmployee>(_pieceEmployeeRepository.Find(w=>w.BonSortieId==bSortie.Id &&w.ServiceId ==service.Id));
            ProgressBar.Visibility = Visibility.Collapsed;

        }

        private void DoWork(IList items, BonSortie bSortie, Departement departemet, Service service, SousService sousService,
           PBar pBar)
        {
            foreach (var item in items.OfType<StockModelView>())
            {
                var stock = _stockService.SelectById(item.Id);

                var p = new PieceEmployee
                {
                    PieceId = item.PieceId,
                    Date = DateTime.Now,
                    BonSortieId = bSortie.Id,
                    DepartementId = departemet.Id,
                    ServiceId = service.Id,
                };
                if (sousService != null) p.SousServiceId = sousService.Id;
                _pieceEmployeeRepository.Insert(p);
                _pieceEmployeeRepository.Save();
                stock.Disponibilite = false;
                _stockService.Update(stock);
                _stockService.Save();

                pBar.IncPB();
            }
        }
    }
}

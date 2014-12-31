﻿using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using BLL;
using GM.Entity.Models;
using Microsoft.Practices.Unity;
using AutoMapper;
using WPF.Core.Helpers;

namespace GM.UI.Views
{
    /// <summary>
    /// Interaction logic for PieceView.xaml
    /// </summary>
    public partial class PieceView
    {
        private readonly ArticleService _articleService;
        private readonly IRepository<SousCategorie> _sousCategorieRepository;
        public static PieceService PieceService;
        public static StockService StockService;
        private readonly IRepository<BonEntreeLigne> _beLigneRepository;
        private static  IRepository<PieceEmployee> _pServiRepository;
        private static IRepository<Reformet> _reformeRepository;
        public static  IRepository<HistoriqueInventaire> HistoriqueRepository; 
        public PieceView()
        {
            InitializeComponent();
            var container = new UnityContainer();
            var magasinRepository = container.Resolve<Repository<Magasin>>();
            _articleService = container.Resolve<ArticleService>();
            var marqueRepository = container.Resolve<Repository<Marque>>();
            PieceService = container.Resolve<PieceService>();
            StockService = container.Resolve<StockService>();
            _pServiRepository = container.Resolve<Repository<PieceEmployee>>();
            var beRepository = container.Resolve<Repository<BonEntree>>();
            _beLigneRepository = container.Resolve<Repository<BonEntreeLigne>>();
           
            var categorieRepository = container.Resolve<Repository<Categorie>>();
           
            _sousCategorieRepository = container.Resolve<Repository<SousCategorie>>();
            _reformeRepository = container.Resolve<Repository<Reformet>>();
            HistoriqueRepository = container.Resolve<Repository<HistoriqueInventaire>>();
            Init(categorieRepository, magasinRepository, beRepository, marqueRepository);
        }

        private void Init(Repository<Categorie> categorieRepository, Repository<Magasin> magasinRepository, Repository<BonEntree> beRepository,
            Repository<Marque> marqueRepository)
        {
            CbCategorie.ItemsSource = categorieRepository.SelectAll();
            CbMagasin.ItemsSource = magasinRepository.SelectAll();
            CbBEntree.ItemsSource = beRepository.SelectAll();
            CbMarque.ItemsSource = marqueRepository.SelectAll();
            LoadData();
        }

        private void LoadData()
        {
            DataGrid.ItemsSource =
                new ObservableCollection<Piece>(PieceService.GetAllLazyLoad(x => x.Magasin, x => x.Article,
                    x => x.BonEntree));
        }

        private void AddButton_OnClick(object sender, RoutedEventArgs e)
        {
            AddButton.Visibility = Visibility.Hidden;
            var list = DataGrid.ItemsSource.OfType<Piece>().ToList();
            list.Add(PieceService.Create());
            Grid.DataContext = list.Last();
        }

        private void UpdateButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (DataGrid.SelectedIndex < 0)
            {
                MessageBox.Show("Selectionner un champ", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            AddButton.Visibility = Visibility.Hidden;
            UpdateButton.Visibility = Visibility.Hidden;
            DeleteButton.Visibility = Visibility.Hidden;
        }

        private void SaveButton_OnClick(object sender, RoutedEventArgs e)
        {
            var item = (Piece) Grid.DataContext;

            if (item.Id <= 0)
            {
                if (string.IsNullOrEmpty(item.NInventaire))
                {
                    var article = _articleService.SelectById(item.ArticleId);
                    item.NInventaire = GenerateInventoryName(article.Libelle);
                    item.EtatStock = EtatStock.Normal.ToString();

                }
                // ((ObservableCollection<Article>)DataGrid.ItemsSource).Add(item);
            }
            else
            {
                PieceService.Update(item);
            }
            try
            { 
                PieceService.Insert(item);
                PieceService.Save();
                var historqique = new HistoriqueInventaire
                {
                    CodeLocation = ((Magasin)CbMagasin.SelectedItem).Code,
                    Inventaire = item.NInventaire,
                    LocationId = item.MagasinId,
                    Date = DateTime.Now, 
                    Etat = EtatStock.Normal.ToString()
                };
                HistoriqueRepository.Insert(historqique);
                HistoriqueRepository.Save();
               
                var stcok = Mapper.Map<PieceMagasin>(item);
                stcok.Disponibilite = true;
                stcok.EtatStock = EtatStock.Normal.ToString();
                StockService.Insert(stcok);
                StockService.Save();
                LoadData();
                var binding = new Binding { ElementName = "DataGrid", Path = new PropertyPath("SelectedItem") };
                Grid.SetBinding(DataContextProperty, binding);
              
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Erreurs pendant l'enregistrement", MessageBoxButton.OK,
                    MessageBoxImage.Error);
                //((ObservableCollection<Article>)DataGrid.ItemsSource).Remove(item);
            }

            AddButton.Visibility = Visibility.Visible;
          
            UpdateButton.Visibility = Visibility.Visible;
            DeleteButton.Visibility = Visibility.Visible;
        }

        private void BackButton_OnClick(object sender, RoutedEventArgs e)
        {
            AddButton.Visibility = Visibility.Visible;
            UpdateButton.Visibility = Visibility.Visible;
            DeleteButton.Visibility = Visibility.Visible;
            var binding = new Binding {ElementName = "DataGrid", Path = new PropertyPath("SelectedItem")};
            Grid.SetBinding(DataContextProperty, binding);
        }

        private void DeleteButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (DataGrid.SelectedIndex < 0)
            {
                MessageBox.Show("Selectionner un champ", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            var result = MessageBox.Show("Est vous sure!", "Warning", MessageBoxButton.YesNo,
                MessageBoxImage.Warning);
            if (!result.ToString().Equals("Yes")) return;
            var items = DataGrid.SelectedItems;
            if (items == null) return;
            var frm = new ProgressBarView{ Title = "Supprission en cours ..."};
            frm.Show();
            DeleteDetailsPieces(items, frm.PrBar);
            LoadData();
            frm.Close();
        }

        private static void DeleteDetailsPieces(IEnumerable items ,ProgressBar bar)
        {
            if (items ==null)return;
            bar.Minimum = 0;
            var enumerable = items as object[] ?? items.Cast<object>().ToArray();
            bar.Maximum = enumerable.OfType<Piece>().Count();
            var pBarHandler = new PBar(bar);
            foreach (var item in enumerable.OfType<Piece>())
            {
                if (item == null) continue;
                var piece = item;
                var stock = StockService.Find(x => x.PieceId == piece.Id).FirstOrDefault();
                var pieceUses = _pServiRepository.Find(x=>x.PieceId == piece.Id).FirstOrDefault();
               
                if (stock != null)
                {
                    StockService.Delete(stock.Id);
                    StockService.Save();
                }
                if (pieceUses!= null)
                {
                    _pServiRepository.Delete(pieceUses.Id);
                    _pServiRepository.Save();
                }
              
                PieceService.Delete(item.Id);
                PieceService.Save();
                pBarHandler.IncPB();
            }
        }


        private void CbCategorie_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CbCategorie.SelectedIndex == -1) return;
            var categorie = CbCategorie.SelectedItem as Categorie;
            if (categorie != null)
                CbSousCategorie.ItemsSource = _sousCategorieRepository.Find(x => x.CategorieId == categorie.Id);

        }

        public static string GenerateInventoryName(string article)
        {
            return string.Format("{0}_{1}", DateTime.Now.ToString("yyyyMMddHHmmssfff"), article);
        }

        private void CbSousCategorie_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CbSousCategorie.SelectedIndex == -1) return;

            var categorie = CbCategorie.SelectedItem as Categorie;
            var sousCategorie = CbSousCategorie.SelectedItem as SousCategorie;
            if (sousCategorie != null && categorie != null)
                CbArticle.ItemsSource = _articleService.Find(x => x.CategorieId == categorie.Id
                                                               && x.SousCategorieId == sousCategorie.Id);
        }

        private void CbMarque_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CbMarque.SelectedIndex == -1) return;

            var categorie = CbCategorie.SelectedItem as Categorie;
            var sousCategorie = CbSousCategorie.SelectedItem as SousCategorie;
            var marque = CbMagasin.SelectedItem as Marque;
            if (sousCategorie != null && categorie != null && marque != null)
                CbArticle.ItemsSource = _articleService.Find(x => x.CategorieId == categorie.Id
                                                                  && x.SousCategorieId == sousCategorie.Id 
                                                                  && x.MarqueId == marque.Id);
        }

        private void AddMultButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (DataGrid.SelectedIndex < 0)
            {
                MessageBox.Show("Selectionner un champ", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            var item = DataGrid.SelectedItem as Piece;
            if (item == null) return;
            var ligne =  _beLigneRepository.Find(x => x.BonEntreeId == item.BonEntreeId && x.ArticleId == item.ArticleId).FirstOrDefault();
            var countStock = StockService.Find(x => x.ArticleId == item.ArticleId && x.BonEntreeId == item.BonEntreeId).Count();
            if (ligne == null) return;
            var qnt = ligne.Qnt - (countStock)>=0 ?ligne.Qnt - (countStock):0;
            var frm = new MulitStockFrm(Convert.ToInt32(qnt), item);
            try
            {
                frm.Update += UpdateDataGrid;
                frm.ShowDialog();

            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void UpdateDataGrid(long id)
        {
            LoadData();
        }


        private void TxtSearch_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                DataGrid.ItemsSource = new ObservableCollection<Piece>(PieceService.Find(
                    x => x.Article.Libelle.Contains(TxtSearch.Text)
                        ||x.NInventaire.StartsWith(TxtSearch.Text)
                    || x.Article.SousCategorie.Libelle.Contains(TxtSearch.Text) 
                  || x.Article.Marque.Libelle.Contains(TxtSearch.Text)));
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void ReformeBtn_OnClick(object sender, RoutedEventArgs e)
        {
            if (DataGrid.SelectedIndex < 0)
            {
                MessageBox.Show("Selectionner un champ svp", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            var result = MessageBox.Show("Est vous sure!", "Warning", MessageBoxButton.YesNo,
              MessageBoxImage.Warning);
            if (!result.ToString().Equals("Yes")) return;
            var piece = DataGrid.SelectedItem as Piece;
            var reforme = Mapper.Map<Reformet>(piece);
            reforme.Date = DateTime.Now.Date;
            _reformeRepository.Insert(reforme);
            _reformeRepository.Save();
            var etat = new EtatMateriel();
            if (piece != null)
            {
                UpdateUtilisationPiece(piece);
                piece.EtatPiece = etat.EtatMateriels[2].Etat;
                PieceService.Update(piece);
                PieceService.Save();
                UpdateStockPiece(piece);
                MessageBox.Show("Reforme terminé correctement");
            }
            
            
        }

        private static void UpdateStockPiece(Piece piece)
        {
            var stock = StockService.Find(x => x.PieceId == piece.Id).FirstOrDefault();
            if (stock == null) return;
            if (!stock.Disponibilite) return;
            stock.Disponibilite = false;
            StockService.Update(stock);
            StockService.Save();
        }

        private static void UpdateUtilisationPiece(Piece piece)
        {
            var ut = _pServiRepository.Find(x => x.PieceId == piece.Id).FirstOrDefault();
            if (ut == null) return;
            ut.Utilisation = false;
            _pServiRepository.Update(ut);
            _pServiRepository.Save();
        }
    }
}

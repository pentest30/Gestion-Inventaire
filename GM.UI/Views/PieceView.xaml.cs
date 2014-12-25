using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
using BLL;
using GM.Entity.Models;
using Microsoft.Practices.Unity;
using AutoMapper;
using Microsoft.Windows.Controls;

namespace GM.UI.Views
{
    /// <summary>
    /// Interaction logic for PieceView.xaml
    /// </summary>
    public partial class PieceView
    {
        private readonly ArticleService _articleService;
        private readonly Repository<SousCategorie> _sousCategorieRepository;
        private readonly Repository<Entity.Models.Type> _typeRepository;
        public static PieceService PieceService;
        public static StockService StockService;
        private readonly Repository<BonEntreeLigne> _beLigneRepository;


        public PieceView()
        {
            InitializeComponent();
            var container = new UnityContainer();
            _articleService = container.Resolve<ArticleService>();
            PieceService = container.Resolve<PieceService>();
            StockService = container.Resolve<StockService>();
            var beRepository = container.Resolve<Repository<BonEntree>>();
            _beLigneRepository = container.Resolve<Repository<BonEntreeLigne>>();
            var magasinRepository = container.Resolve<Repository<Magasin>>();
            var categorieRepository = container.Resolve<Repository<Categorie>>();
            var marqueRepository = container.Resolve<Repository<Marque>>();
            _sousCategorieRepository = container.Resolve<Repository<SousCategorie>>();
            _typeRepository = container.Resolve<Repository<Entity.Models.Type>>();
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
                    item.EtatStock = EtatStock.Stock.ToString();

                }
                PieceService.Insert(item);
                // ((ObservableCollection<Article>)DataGrid.ItemsSource).Add(item);
            }
            else
            {
                PieceService.Update(item);
            }
            try
            {
                PieceService.Save();
                var stcok = Mapper.Map<PieceMagasin>(item);
                StockService.Insert(stcok);
                StockService.Save();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Erreurs pendant l'enregistrement", MessageBoxButton.OK,
                    MessageBoxImage.Error);
                //((ObservableCollection<Article>)DataGrid.ItemsSource).Remove(item);
            }

            AddButton.Visibility = Visibility.Visible;
            LoadData();
            var binding = new Binding {ElementName = "DataGrid", Path = new PropertyPath("SelectedItem")};
            Grid.SetBinding(DataContextProperty, binding);
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

            foreach (var item in items.OfType<Piece>())
            {
                if (item==null)continue;
                Piece piece = item;
                var stock = StockService.Find(x => x.PieceId == piece.Id).FirstOrDefault();
                if (stock != null)
                {
                    StockService.Delete(stock.Id);
                    StockService.Save();
                }
                PieceService.Delete(item.Id);
                PieceService.Save();
            }
            LoadData();
        }

        private void CbType_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CbType.SelectedIndex == -1) return;
            var type = CbType.SelectedItem as Entity.Models.Type;
            var categorie = CbCategorie.SelectedItem as Categorie;
            var sousCategorie = CbSousCategorie.SelectedItem as SousCategorie;
            if (type != null && categorie != null && sousCategorie != null)
                CbArticle.ItemsSource = _articleService.Find(x => x.TypeId == type.Id).ToList();
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
            //var marque = CbMagasin.SelectedItem as Marque;
            if (sousCategorie != null && categorie != null)
                CbType.ItemsSource = _typeRepository.Find(x => x.CategorieId == categorie.Id
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
            var qnt = ligne.Qnt - (countStock+1)>=0 ?ligne.Qnt - (countStock+1):0;
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


        private void My_first_event_handler(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.S:DeleteButton.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
                    break;
                case Key.M: UpdateButton.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
                    break;
                case Key.A: AddButton.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
                    break;
               
            }
          
        }

       
    }
}

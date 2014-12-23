using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using BLL;
using GM.Entity.Models;
using Microsoft.Practices.Unity;

namespace GM.UI.Views
{
    /// <summary>
    /// Interaction logic for PieceView.xaml
    /// </summary>
    public partial class PieceView
    {
        private readonly ArticleService _articleService;
        private readonly Repository<SousCategorie> _sousCategorieRepository;
        private readonly PieceService _pieceService;


        public PieceView()
        {
            InitializeComponent();
            var container = new UnityContainer();
            _articleService = container.Resolve<ArticleService>();
            _pieceService = container.Resolve<PieceService>();
            //_articleRepository = container.Resolve<Repository<Article>>();
            //var marqueRepository = container.Resolve<Repository<Marque>>();
            var categorieRepository = container.Resolve<Repository<Categorie>>();
            _sousCategorieRepository = container.Resolve<Repository<SousCategorie>>();
            var typeRepository = container.Resolve<Repository<Entity.Models.Type>>();
            CbCategorie.ItemsSource = categorieRepository.SelectAll();
            CbType.ItemsSource = typeRepository.SelectAll();
            DataGrid.ItemsSource = new ObservableCollection<Piece>(_pieceService.GetAllLazyLoad(x=>x.Article ,x=>x.Magasin));
        }

        private void AddButton_OnClick(object sender, RoutedEventArgs e)
        {
            AddButton.Visibility = Visibility.Hidden;
            var list = DataGrid.ItemsSource.OfType<Piece>().ToList();
            list.Add(_pieceService.Create());
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
            var item = (Piece)Grid.DataContext;
          
            if (item.Id <= 0)
            {
                if (string.IsNullOrEmpty(item.NInventaire))
                {
                    var article = _articleService.SelectById(item.ArticleId);
                    item.NInventaire = GenerateInventoryName(article.Libelle);

                }
                _pieceService.Insert(item);
                // ((ObservableCollection<Article>)DataGrid.ItemsSource).Add(item);
            }
            else
            {
                _pieceService.Update(item);
            }
            try
            {
                _pieceService.Save();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Erreurs pendant l'enregistrement", MessageBoxButton.OK,
                    MessageBoxImage.Error);
                //((ObservableCollection<Article>)DataGrid.ItemsSource).Remove(item);
            }

            AddButton.Visibility = Visibility.Visible;
            DataGrid.ItemsSource = new ObservableCollection<Piece>(_pieceService.GetAllLazyLoad(x => x.Article, x => x.Magasin));
            var binding = new Binding { ElementName = "DataGrid", Path = new PropertyPath("SelectedItem") };
            Grid.SetBinding(DataContextProperty, binding);
            UpdateButton.Visibility = Visibility.Visible;
            DeleteButton.Visibility = Visibility.Visible;
        }

        private void BackButton_OnClick(object sender, RoutedEventArgs e)
        {
            AddButton.Visibility = Visibility.Visible;
            UpdateButton.Visibility = Visibility.Visible;
            DeleteButton.Visibility = Visibility.Visible;
            var binding = new Binding { ElementName = "DataGrid", Path = new PropertyPath("SelectedItem") };
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
            var item = DataGrid.SelectedItem as Piece;
            if (item == null) return;
            _pieceService.Delete(item.Id);
            ((ObservableCollection<Piece>) DataGrid.ItemsSource).Remove(item);
            _pieceService.Save();
        }

        private void CbType_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CbType.SelectedIndex == -1) return;
            var type = CbType.SelectedItem as Entity.Models.Type;
            var categorie = CbCategorie.SelectedItem as Categorie;
            var sousCategorie = CbSousCategorie.SelectedItem as SousCategorie;
            if (type != null && categorie!= null && sousCategorie!=null)
                CbArticle.ItemsSource = _articleService.Find(x => x.TypeId == type.Id 
                    && x.SousCategorieId==sousCategorie.Id 
                    && x.CategorieId== categorie.Id);
        }

        private void CbCategorie_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CbCategorie.SelectedIndex == -1) return;
            var categorie = CbCategorie.SelectedItem as Categorie;
            if (categorie != null)
                CbSousCategorie.ItemsSource = _sousCategorieRepository.Find(x => x.CategorieId == categorie.Id);

        }
        private string GenerateInventoryName(string article)
        {
            return string.Format("{0}_{1}", DateTime.Now.ToString("yyyyMMddHHmmssfff"), article);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using BLL;
using GM.Entity.Models;
using Microsoft.Practices.Unity;
using OpenFileDialog = Microsoft.Win32.OpenFileDialog;

namespace GM.UI.Views
{
    /// <summary>
    /// Interaction logic for ArticleView.xaml
    /// </summary>
    public partial class ArticleView
    {
        
        //private readonly Repository<Article> _articleRepository;
        private readonly ArticleService _articleService;
        private readonly Repository<SousCategorie> _sousCategorieRepository;
        private readonly Repository<Entity.Models.Type> _typeRepository;
        public ArticleView()
        {
            InitializeComponent();
            var container = new UnityContainer();
            _articleService = container.Resolve<ArticleService>();
            //_articleRepository = container.Resolve<Repository<Article>>();
            var marqueRepository = container.Resolve<Repository<Marque>>();
            var categorieRepository = container.Resolve<Repository<Categorie>>();
            _sousCategorieRepository = container.Resolve<Repository<SousCategorie>>();
            _typeRepository = container.Resolve<Repository<Entity.Models.Type>>();
            DataGrid.ItemsSource =(_articleService.ListWithCount().Any())? _articleService.ListWithCount(): new List<Article>();
            CbCategorie.ItemsSource = categorieRepository.SelectAll();
            CbMaruqe.ItemsSource = marqueRepository.SelectAll();
        }

      
        private void BtnImageLoader_OnClick(object sender, RoutedEventArgs e)
        {
            var dlg = new OpenFileDialog();
            dlg.Filter = "Image files (*.jpg)|*.jpg|All Files (*.*)|*.*";
            dlg.RestoreDirectory = true;
            if (dlg.ShowDialog() != true) return;
            var selectedFileName = dlg.FileName;
            var bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(selectedFileName);
            bitmap.EndInit();
            Image.Source = bitmap;
        }

        private void AddButton_OnClick(object sender, RoutedEventArgs e)
        {
            AddButton.Visibility = Visibility.Hidden;
            var list = DataGrid.ItemsSource.OfType<Article>().ToList();
            list.Add(_articleService.Create());
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
            var item = (Article)Grid.DataContext;
           if (Image.Source!= null) item.Image = BitmapSourceToByteArray(Image.Source as BitmapSource);
            if (item.Id <= 0)
            {
               _articleService.Insert(item);
               // ((ObservableCollection<Article>)DataGrid.ItemsSource).Add(item);
            }
            else
            {
                _articleService.Update(item);
            }
            try
            {
                _articleService.Save();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Erreurs pendant l'enregistrement", MessageBoxButton.OK,
                    MessageBoxImage.Error);
                //((ObservableCollection<Article>)DataGrid.ItemsSource).Remove(item);
            }

            AddButton.Visibility = Visibility.Visible;
            DataGrid.ItemsSource = new ObservableCollection<Article>(_articleService.ListWithCount());
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
            var deleted = DataGrid.SelectedItem as Article;
            if (deleted == null) return;
            _articleService.Delete(deleted.Id);
            ((ObservableCollection<Article>)DataGrid.ItemsSource).Remove(deleted);
            _articleService.Save();
        }

        private void CbCategorie_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CbCategorie.SelectedIndex ==-1) return;
            var categorie = CbCategorie.SelectedItem as Categorie;
            if(categorie!= null)
                CbSousCategorie.ItemsSource = _sousCategorieRepository.Find(x => x.CategorieId == categorie.Id);
        }
        private static byte[] BitmapSourceToByteArray(BitmapSource image)
        {
            using (var stream = new MemoryStream())
            {
                var encoder = new PngBitmapEncoder(); // or some other encoder
                encoder.Frames.Add(BitmapFrame.Create(image));
                encoder.Save(stream);
                return stream.ToArray();
            }
        }

        private void CbSousCategorie_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var item = CbSousCategorie.SelectedItem as SousCategorie;
                if (item != null) CbType.ItemsSource = _typeRepository.Find(x => x.SousCategorieId == item.Id);
            }
            catch (Exception)
            {
                
            }
        }
    }
}

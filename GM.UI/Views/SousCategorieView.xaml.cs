using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using BLL;
using GM.Entity.Models;
using Microsoft.Practices.Unity;

namespace GM.UI.Views
{
    /// <summary>
    /// Interaction logic for SousCategorieView.xaml
    /// </summary>
    public partial class SousCategorieView
    {
        private readonly EntityFactory<SousCategorie> _factory;
        private readonly Repository<SousCategorie> _sousCategorieRepository;
        public SousCategorieView()
        {
            InitializeComponent();
            var container = new UnityContainer();
            _sousCategorieRepository = container.Resolve<Repository<SousCategorie>>();
            var departmentRepository = container.Resolve<Repository<Categorie>>();
            _factory = container.Resolve<EntityFactory<SousCategorie>>();
            CbDepartement.ItemsSource = departmentRepository.SelectAll();
            PopulateDg();
        }

        private void PopulateDg()
        {
            DataGrid.ItemsSource =
                new ObservableCollection<SousCategorie>(_sousCategorieRepository.GetAllLazyLoad(x => x.Categorie));
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
            var item = (SousCategorie)Grid.DataContext;
            if (item.Id <= 0)
            {
                _sousCategorieRepository.Insert(item);
                ((ObservableCollection<SousCategorie>)DataGrid.ItemsSource).Add(item);

            }
            else
            {
                _sousCategorieRepository.Update(item);
            }
            try
            {
                _sousCategorieRepository.Save();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Erreurs pendant l'enregistrement", MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }

            AddButton.Visibility = Visibility.Visible;
            PopulateDg();
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
            var deleted = DataGrid.SelectedItem as SousCategorie;
            if (deleted == null) return;
            _sousCategorieRepository.Delete(deleted.Id);
            ((ObservableCollection<SousCategorie>)DataGrid.ItemsSource).Remove(deleted);
            _sousCategorieRepository.Save();
        }

        private void AddButton_OnClick(object sender, RoutedEventArgs e)
        {
            AddButton.Visibility = Visibility.Hidden;
            var list = DataGrid.ItemsSource.OfType<SousCategorie>().ToList();
            list.Add(_factory.Create());
            Grid.DataContext = list.Last();  
        }
    }
}

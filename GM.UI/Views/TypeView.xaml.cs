using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using BLL;
using GM.Entity.Models;
using Microsoft.Practices.Unity;
using Type = GM.Entity.Models.Type;

namespace GM.UI.Views
{
    /// <summary>
    /// Interaction logic for TypeView.xaml
    /// </summary>
    public partial class TypeView
    {
        private readonly EntityFactory<Type> _factory;
        private readonly Repository<SousCategorie> _sousCategorieRepository;
        private readonly Repository<Type> _typeRepository;
        public TypeView()
        {
            InitializeComponent();
            var container = new UnityContainer();
            _sousCategorieRepository = container.Resolve<Repository<SousCategorie>>();
            var departmentRepository = container.Resolve<Repository<Categorie>>();
            _typeRepository = container.Resolve<Repository<Type>>();
            _factory = container.Resolve<EntityFactory<Type>>();
            CbDepartement.ItemsSource = departmentRepository.SelectAll();
            DataGrid.ItemsSource = new ObservableCollection<Type>(_typeRepository.GetAllLazyLoad(x => x.Categorie, x => x.SousCategorie));
        }

        private void AddButton_OnClick(object sender, RoutedEventArgs e)
        {
            AddButton.Visibility = Visibility.Hidden;
            var list = DataGrid.ItemsSource.OfType<Type>().ToList();
            list.Add(_factory.Create());
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
            var item = (Type)Grid.DataContext;
            if (item.Id <= 0)
            {
                _typeRepository.Insert(item);
                ((ObservableCollection<Type>)DataGrid.ItemsSource).Add(item);

            }
            else
            {
                _typeRepository.Update(item);
            }
            try
            {
                _typeRepository.Save();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Erreurs pendant l'enregistrement", MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }

            AddButton.Visibility = Visibility.Visible;
            DataGrid.ItemsSource = new ObservableCollection<Type>(_typeRepository.GetAllLazyLoad(x => x.Categorie, x => x.SousCategorie));
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
            var item = DataGrid.SelectedItem as Type;
            if (item == null) return;
            _sousCategorieRepository.Delete(item.Id);
            ((ObservableCollection<Type>)DataGrid.ItemsSource).Remove(item);
            _typeRepository.Save();
        }

        private void CbDepartement_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CbDepartement.SelectedIndex == -1) return;
            var categorie = CbDepartement.SelectedItem as Categorie;
            if (categorie != null)
                CbSousCategorie.ItemsSource = _sousCategorieRepository.Find(x => x.CategorieId == categorie.Id);

        }
    }
}

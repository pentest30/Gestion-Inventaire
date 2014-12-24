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
    /// Interaction logic for CategorieView.xaml
    /// </summary>
    public partial class CategorieView
    {
        private readonly EntityFactory<Categorie> _factory;
        private readonly Repository<Categorie> _repository; 
        public CategorieView()
        {
            InitializeComponent();
            var container = new UnityContainer();
            _repository = container.Resolve<Repository<Categorie>>();
            _factory = container.Resolve<EntityFactory<Categorie>>();
            DataGrid.ItemsSource = new ObservableCollection<Categorie>(_repository.SelectAll());
        }

        private void AddButton_OnClick(object sender, RoutedEventArgs e)
        {
            AddButton.Visibility = Visibility.Hidden;
            var list = DataGrid.ItemsSource.OfType<Categorie>().ToList();
            list.Add(_factory.Create());
            Grid.DataContext = list.Last();  
        }

        private void SaveButton_OnClick(object sender, RoutedEventArgs e)
        {
            var item = (Categorie)Grid.DataContext;
            if (item.Id <= 0)
            {
                _repository.Insert(item);
                ((ObservableCollection<Categorie>) DataGrid.ItemsSource).Add(item);

            }
            else
            {
                _repository.Update(item);
            }
            try
            {
                _repository.Save();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Erreurs pendant l'enregistrement", MessageBoxButton.OK,
                    MessageBoxImage.Error);
                ((ObservableCollection<Categorie>)DataGrid.ItemsSource).Remove(item);
            }

            AddButton.Visibility = Visibility.Visible;
            DataGrid.ItemsSource = new ObservableCollection<Categorie>(_repository.SelectAll());
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
            var deleted = DataGrid.SelectedItem as Categorie;
            if (deleted == null) return;
            _repository.Delete(deleted.Id);
            ((ObservableCollection<Categorie>)DataGrid.ItemsSource).Remove(deleted);
            _repository.Save();
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
    }
}

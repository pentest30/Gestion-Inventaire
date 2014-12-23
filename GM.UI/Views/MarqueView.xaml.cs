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
    /// Interaction logic for MarqueView.xaml
    /// </summary>
    public partial class MarqueView
    {
        private readonly EntityFactory<Marque> _factory;
        private readonly Repository<Marque> _repository; 
        public MarqueView()
        {
            InitializeComponent();
            var container = new UnityContainer();
            _repository = container.Resolve<Repository<Marque>>();
            _factory = container.Resolve<EntityFactory<Marque>>();
            DataGrid.ItemsSource = new ObservableCollection<Marque>(_repository.SelectAll());
        }

        private void AddButton_OnClick(object sender, RoutedEventArgs e)
        {
            AddButton.Visibility = Visibility.Hidden;
            var list = DataGrid.ItemsSource.OfType<Marque>().ToList();
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
            var marque = (Marque)Grid.DataContext;
            if (marque.Id <= 0)
            {
                _repository.Insert(marque);
                ((ObservableCollection<Marque>)DataGrid.ItemsSource).Add(marque);

            }
            else
            {
                _repository.Update(marque);
            }
            try
            {
                _repository.Save();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Erreurs pendant l'enregistrement", MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }

            AddButton.Visibility = Visibility.Visible;
            DataGrid.ItemsSource = new ObservableCollection<Marque>(_repository.SelectAll());
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
            var deleted = DataGrid.SelectedItem as Marque;
            if (deleted == null) return;
            _repository.Delete(deleted.Id);
            ((ObservableCollection<Marque>)DataGrid.ItemsSource).Remove(deleted);
            _repository.Save();
        }
    }
}

using System;
using System.Collections.Generic;
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
    /// Interaction logic for MagasinView.xaml
    /// </summary>
    public partial class MagasinView
    {
        private readonly EntityFactory<Magasin> _factory;
        private readonly Repository<Magasin> _magasinRepository;
        public MagasinView()
        {
            InitializeComponent();
            var container = new UnityContainer();
            _magasinRepository = container.Resolve<Repository<Magasin>>();
            var departmentRepository = container.Resolve<Repository<Departement>>();
            _factory = container.Resolve<EntityFactory<Magasin>>();
            CbDepartement.ItemsSource = departmentRepository.SelectAll();
            DataGrid.ItemsSource = new ObservableCollection<Magasin>(_magasinRepository.GetAllLazyLoad(x=>x.Departement));
        }

        private void AddButton_OnClick(object sender, RoutedEventArgs e)
        {
            AddButton.Visibility = Visibility.Hidden;
            var list = DataGrid.ItemsSource.OfType<Magasin>().ToList();
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
            var item = (Magasin)Grid.DataContext;
            if (item.Id <= 0)
            {
                _magasinRepository.Insert(item);
                ((ObservableCollection<Magasin>)DataGrid.ItemsSource).Add(item);

            }
            else
            {
                _magasinRepository.Update(item);
            }
            try
            {
                _magasinRepository.Save();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Erreurs pendant l'enregistrement", MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }

            AddButton.Visibility = Visibility.Visible;
            DataGrid.ItemsSource = new ObservableCollection<Magasin>(_magasinRepository.SelectAll());
            var binding = new Binding { ElementName = "DataGrid", Path = new PropertyPath("SelectedItem") };
            Grid.SetBinding(DataContextProperty, binding);
            UpdateButton.Visibility = Visibility.Visible;
            DeleteButton.Visibility = Visibility.Visible;
        }

        private void BackButton_OnClick(object sender, RoutedEventArgs e)
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
            var deleted = DataGrid.SelectedItem as Magasin;
            if (deleted == null) return;
            _magasinRepository.Delete(deleted.Id);
            ((List<Magasin>)DataGrid.ItemsSource).Remove(deleted);
            _magasinRepository.Save();
        }
    }
}

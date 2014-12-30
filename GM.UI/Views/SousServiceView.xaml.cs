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
    /// Interaction logic for SousServiceView.xaml
    /// </summary>
    public partial class SousServiceView
    {
        private readonly EntityFactory<SousService> _factory;
        private readonly Repository<SousService> _sousServiceRepository;
        private readonly Repository<Service> _serviceRepository;

        public SousServiceView()
        {
            InitializeComponent();
            var container = new UnityContainer();
            _factory = container.Resolve<EntityFactory<SousService>>();
            _sousServiceRepository = container.Resolve<Repository<SousService>>();
            var departementRepository = container.Resolve<Repository<Departement>>();
            _serviceRepository = container.Resolve<Repository<Service>>();
            CbDepartement.ItemsSource = departementRepository.SelectAll();
            LoadData();
        }

        private void CbDepartement_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CbDepartement.SelectedIndex ==-1)return;
            var item = CbDepartement.SelectedItem as Departement;
            if (item != null) CbSerivece.ItemsSource = _serviceRepository.Find(x => x.DepartementId == item.Id);
        }

        private void AddButton_OnClick(object sender, RoutedEventArgs e)
        {
            AddButton.Visibility = Visibility.Hidden;
            var list = DataGrid.ItemsSource.OfType<SousService>().ToList();
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
            var item = DataGrid.SelectedItem as SousService;
            if (item == null) return;
            _sousServiceRepository.Delete(item.Id);
            ((ObservableCollection<SousService>)DataGrid.ItemsSource).Remove(item);
            _sousServiceRepository.Save();
        }

        private void SaveButton_OnClick(object sender, RoutedEventArgs e)
        {
            var item = (SousService)Grid.DataContext;
            if (item.Id <= 0)
            {
                item.Code = Guid.NewGuid();
                _sousServiceRepository.Insert(item);
                ((ObservableCollection<SousService>)DataGrid.ItemsSource).Add(item);

            }
            else
            {
                _sousServiceRepository.Update(item);
            }
            try
            {
                _sousServiceRepository.Save();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Erreurs pendant l'enregistrement", MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }

            AddButton.Visibility = Visibility.Visible;
            LoadData();
            var binding = new Binding { ElementName = "DataGrid", Path = new PropertyPath("SelectedItem") };
            Grid.SetBinding(DataContextProperty, binding);
            UpdateButton.Visibility = Visibility.Visible;
            DeleteButton.Visibility = Visibility.Visible;
        }

        private void LoadData()
        {
            DataGrid.ItemsSource =
                new ObservableCollection<SousService>(_sousServiceRepository.GetAllLazyLoad(x => x.Departement, x => x.Service));
        }
    }
}

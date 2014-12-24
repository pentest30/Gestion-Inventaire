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
    /// Interaction logic for CommandeView.xaml
    /// </summary>
    public partial class CommandeView
    {
        private readonly EntityFactory<CommandeInterne> _factory;
        private readonly Repository<CommandeInterne> _commandeRepository;
        private readonly Repository<CommandeLigne> _commandeLigneRepository;
        private readonly Repository<Service> _serviceRepository;
        public CommandeView()
        {
            InitializeComponent();
            var container = new UnityContainer();
            _factory = container.Resolve<EntityFactory<CommandeInterne>>();
            _commandeRepository = container.Resolve<Repository<CommandeInterne>>();
            _commandeLigneRepository = container.Resolve<Repository<CommandeLigne>>();
            var departementRepository = container.Resolve<Repository<Departement>>();
            _serviceRepository = container.Resolve<Repository<Service>>();
            CbDepartement.ItemsSource = departementRepository.SelectAll();
            LoadData();
            
        }

        private void LoadData()
        {
            DataGrid.ItemsSource =
                new ObservableCollection<CommandeInterne>(_commandeRepository.GetAllLazyLoad(c => c.Departement, c => c.Service));
        }

        private void DataGrid_OnSelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            if (DataGrid.SelectedIndex == -1) return;
            var item = DataGrid.SelectedItem as CommandeInterne;
            LoadLigneData(item);
        }

        private void AddButton_OnClick(object sender, RoutedEventArgs e)
        {
            AddButton.Visibility = Visibility.Hidden;
            DeleteButton.Visibility = Visibility.Hidden;
            var list = DataGrid.ItemsSource.OfType<CommandeInterne>().ToList();
            list.Add(_factory.Create());
            Grid.DataContext = list.Last();  
        }

        private void SaveButton_OnClick(object sender, RoutedEventArgs e)
        {
            var item = (CommandeInterne)Grid.DataContext;

            if (item.Id <= 0)
            {
                _commandeRepository.Insert(item);
                //((ObservableCollection<BonEntree>)DataGrid.ItemsSource).Add(item);
            }
            else
            {
                _commandeRepository.Update(item);
            }
            try
            {
                _commandeRepository.Save();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Erreurs pendant l'enregistrement", MessageBoxButton.OK,
                    MessageBoxImage.Error);
                //((ObservableCollection<BonEntree>)DataGrid.ItemsSource).Remove(item);
            }

            AddButton.Visibility = Visibility.Visible;
            LoadData();
            var binding = new Binding { ElementName = "DataGrid", Path = new PropertyPath("SelectedItem") };
            Grid.SetBinding(DataContextProperty, binding);
            UpdateButton.Visibility = Visibility.Visible;
            DeleteButton.Visibility = Visibility.Visible;
        }

        private void UpdateButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (!CheckSelectedItem()) return;
            AddButton.Visibility = Visibility.Hidden;
            UpdateButton.Visibility = Visibility.Hidden;
            DeleteButton.Visibility = Visibility.Hidden;
        }
        private bool CheckSelectedItem()
        {
            if (DataGrid.SelectedIndex < 0)
            {
                MessageBox.Show("Selectionner un champ", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return true;
            }
            return false;
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
            if (DataGridLignes.SelectedIndex < 0)
            {
                MessageBox.Show("Selectionner un champ", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            var result = MessageBox.Show("Est vous sure!", "Warning", MessageBoxButton.YesNo,
               MessageBoxImage.Warning);
            if (!result.ToString().Equals("Yes")) return;
            var ligne = DataGridLignes.SelectedItem as BonEntreeLigne;
            if (ligne == null) return;
            _commandeRepository.Delete(ligne.Id);
            _commandeRepository.Save();
            LoadData();
        }

        private void LBonAddBtn_OnClick(object sender, RoutedEventArgs e)
        {
            if (CheckSelectedItem()) return;
            var be = DataGrid.SelectedItem as CommandeInterne;
            if (be == null) return;
            var frm = new CommndeInterneLigneFrm(be.Id, new CommandeLigne());
            frm.UpdateDataDg += UpdateDG;
            frm.ShowDialog();
        }

        private void UpdateDG(long id)
        {
            if (DataGrid.SelectedIndex ==-1)return;
            var item = DataGrid.SelectedItem as CommandeInterne;
            if(item!=null)LoadLigneData(item);
        }

        private void LoadLigneData(CommandeInterne item)
        {
            DataGridLignes.ItemsSource =
                new ObservableCollection<CommandeLigne>(_commandeLigneRepository.Find(c => c.CommandeInterneId == item.Id));
        }

        private void DeleteBeLignesButton_OnClick(object sender, RoutedEventArgs e)
        {
            
        }

        private void BtnModifierBeLigne_OnClick(object sender, RoutedEventArgs e)
        {
            if (DataGridLignes.SelectedIndex < 0)
            {
                MessageBox.Show("Selectionner un champ", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            var ligne = (DataGridLignes.SelectedItem) as CommandeLigne;
            var frm = new CommndeInterneLigneFrm(0,ligne);
            frm.UpdateDataDg += UpdateDG;
            frm.ShowDialog();
        }

        private void CbDepartement_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CbDepartement.SelectedIndex == -1) return;
            var item = CbDepartement.SelectedItem as Departement;
            if (item != null) CbService.ItemsSource = _serviceRepository.Find(x => x.DepartementId == item.Id);
        }
    }
}

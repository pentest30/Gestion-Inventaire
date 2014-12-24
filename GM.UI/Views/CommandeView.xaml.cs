using System.Windows;
using System.Windows.Controls;
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
        }

        private void DataGrid_OnSelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            
        }

        private void AddButton_OnClick(object sender, RoutedEventArgs e)
        {
            

        }

        private void SaveButton_OnClick(object sender, RoutedEventArgs e)
        {
            
        }

        private void UpdateButton_OnClick(object sender, RoutedEventArgs e)
        {
            
        }

        private void BackButton_OnClick(object sender, RoutedEventArgs e)
        {
            
        }

        private void DeleteButton_OnClick(object sender, RoutedEventArgs e)
        {
            
        }

        private void LBonAddBtn_OnClick(object sender, RoutedEventArgs e)
        {
            
        }

        private void DeleteBeLignesButton_OnClick(object sender, RoutedEventArgs e)
        {
            
        }

        private void BtnModifierBeLigne_OnClick(object sender, RoutedEventArgs e)
        {
            
        }

        private void CbDepartement_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CbDepartement.SelectedIndex == -1) return;
            var item = CbDepartement.SelectedItem as Departement;
            if (item != null) CbService.ItemsSource = _serviceRepository.Find(x => x.DepartementId == item.Id);
        }
    }
}

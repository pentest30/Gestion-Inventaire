using System.Windows;
using System.Windows.Controls;
using BLL;
using GM.Entity.Models;
using GM.UI.Views;
using Microsoft.Practices.Unity;

namespace GM.UI
{
    /// <summary>
    /// Interaction logic for ChangeServiceFrm.xaml
    /// </summary>
    public partial class ChangeServiceFrm
    {
        public CommndeInterneLigneFrm.UpdateDg UpdateDg;
        readonly IRepository<Service> _serviceRepository;
        private readonly PieceEmployee _pieceEmployee;
         static  IRepository<PieceEmployee> _repository; 
        private static  IRepository<SousService> _sousSrviceRepository; 
        public ChangeServiceFrm(long pieceEmployee)
        {
            InitializeComponent();
           
            var container = new UnityContainer();
            var departememntRepository = container.Resolve<Repository<Departement>>();
            _serviceRepository = container.Resolve<Repository<Service>>();
            _sousSrviceRepository = container.Resolve<Repository<SousService>>();
            _repository = container.Resolve<Repository<PieceEmployee>>(); 
            _pieceEmployee = _repository.SelectById(pieceEmployee);
            CbDepartement.ItemsSource = departememntRepository.SelectAll();
        }

        private void CbDepartement_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CbDepartement.SelectedIndex == -1) return;
            var item = CbDepartement.SelectedItem as Departement;
            if (item != null) CbService.ItemsSource = _serviceRepository.Find(x => x.DepartementId == item.Id);
            
        }

        private void AddUpdateBtn_OnClick(object sender, RoutedEventArgs e)
        {
            var dep = CbDepartement.SelectedItem as Departement;
            var service = CbService.SelectedItem as Service;
            if (dep == null || service == null)return;
            _pieceEmployee.ServiceId = service.Id;
            _pieceEmployee.DepartementId = dep.Id;
            var souService = CbSousService.SelectedItem as SousService;
            if (souService != null)
            {
                _pieceEmployee.SousServiceId =souService.Id;
            }
            _repository.Update(_pieceEmployee);
            _repository.Save();
            if (UpdateDg != null) UpdateDg(0);

        }

        private void CbService_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CbService.SelectedIndex == -1) return;
            var item = CbService.SelectedItem as Service;
            if (item != null) CbSousService.ItemsSource = _sousSrviceRepository.Find(x => x.ServiceId == item.Id);

        }
    }
}

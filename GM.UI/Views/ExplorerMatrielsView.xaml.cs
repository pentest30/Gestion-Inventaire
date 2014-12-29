using BLL;
using GM.Entity.Models;
using Microsoft.Practices.Unity;

namespace GM.UI.Views
{
    /// <summary>
    /// Interaction logic for ExplorerMatrielsView.xaml
    /// </summary>
    public partial class ExplorerMatrielsView
    {
        private  readonly IRepository<Magasin>_magasinRepository;
        private readonly IRepository<Departement> _departementRepository;
        private static IRepository<Service> _sercviceRepository;
        private readonly IRepository<SousService> _sousServiceRepository; 
        public ExplorerMatrielsView()
        {
            InitializeComponent();
            var container = new UnityContainer();
            _magasinRepository = container.Resolve<Repository<Magasin>>();
            _sercviceRepository = container.Resolve<Repository<Service>>();
            _sousServiceRepository = container.Resolve<Repository<SousService>>();
            _departementRepository = container.Resolve<Repository<Departement>>();
        }
    }
}

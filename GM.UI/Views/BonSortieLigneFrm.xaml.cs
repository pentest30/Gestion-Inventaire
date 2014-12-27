using System;
using System.Windows;
using System.Windows.Controls;
using BLL;
using GM.Entity.Models;
using Microsoft.Practices.Unity;

namespace GM.UI.Views
{
    /// <summary>
    /// Interaction logic for BonSortieLigneFrm.xaml
    /// </summary>
    public partial class BonSortieLigneFrm
    {
        private readonly Repository<BonSortieLigne> _bsLigneRepository;
        readonly Repository<Service> _serviceRepository ;
        public BonSortieLigneFrm(long id , BonSortieLigne ligneSortie)
        {
            InitializeComponent();
            var container = new UnityContainer();
            _bsLigneRepository = container.Resolve<Repository<BonSortieLigne>>();
            var articleLigneRepository = container.Resolve<Repository<Article>>();
            var departememntRepository = container.Resolve<Repository<Departement>>();
            _serviceRepository = container.Resolve<Repository<Service>>();
            CbArticle.ItemsSource = articleLigneRepository.SelectAll();
            CbDepartement.ItemsSource = departememntRepository.SelectAll();
            var factory = container.Resolve<EntityFactory<BonSortieLigne>>();
            if (ligneSortie.Id == 0)
            {
                var ligne = factory.Create();
                ligne.BonSortieId = id;
                Grid.DataContext = ligne;
            }
            else
            {
                Grid.DataContext = ligneSortie;
            }
        }

        public BonEntreeLigneFrm.UpdateDg UpdateDataDg { get; set; }



        private void CbDepartement_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CbDepartement.SelectedIndex == -1) return;
            var item = CbDepartement.SelectedItem as Departement;
            if (item != null) CbService.ItemsSource = _serviceRepository.Find(x => x.DepartementId == item.Id);

        }

        private void AddUpdateBtn_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var ligne = Grid.DataContext as BonSortieLigne;
                if (ligne == null)
                {
                    MessageBox.Show("Donneés nulls", "Attention", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (ligne.Id == 0) _bsLigneRepository.Insert(ligne);
                else _bsLigneRepository.Update(ligne);
                _bsLigneRepository.Save();
                if (UpdateDataDg != null) UpdateDataDg(ligne.BonSortieId);
            }
            catch (Exception)
            {
                MessageBox.Show("Erreur pendant l'enregistrement des données", "Attention", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}

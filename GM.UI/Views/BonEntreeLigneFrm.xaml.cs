using System;
using System.Windows;
using BLL;
using GM.Entity.Models;
using Microsoft.Practices.Unity;
using System.Collections.Generic;

namespace GM.UI.Views
{
    /// <summary>
    /// Interaction logic for BonEntreeFrm.xaml
    /// </summary>
    public partial class BonEntreeLigneFrm
    {
        public delegate void UpdateDg(long id);
        public UpdateDg UpdateDataDg;
        private  Repository<BonEntreeLigne> _beLigneRepository;
        private UnityContainer _container;

        // BonEntreeLigne _ligne ;

        public BonEntreeLigneFrm(long id)
        {
            InitializeComponent();
            _container = new UnityContainer();
            var factory = _container.Resolve<EntityFactory<BonEntreeLigne>>();
            var ligne = factory.Create();
            ligne.BonEntreeId = id;
            Grid.DataContext = ligne;

        }

        public BonEntreeLigneFrm(BonEntreeLigne ligne)
        {
            InitializeComponent();
            Grid.DataContext = ligne;
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var ligne =Grid. DataContext as BonEntreeLigne;
                if (ligne == null)
                {
                    MessageBox.Show("Donneés nulls", "Attention", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (ligne.Id == 0) _beLigneRepository.Insert(ligne);
                else _beLigneRepository.Update(ligne);
                _beLigneRepository.Save();
                if (UpdateDataDg != null) UpdateDataDg(ligne.BonEntreeId);
            }
            catch (Exception)
            {
                MessageBox.Show("Erreur pendant l'enregistrement des données", "Attention", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BonEntreeLigneFrm_OnLoaded(object sender, RoutedEventArgs e)
        {
            _container = new UnityContainer();
            
            _beLigneRepository = _container.Resolve<Repository<BonEntreeLigne>>();
            var articleLigneRepository = _container.Resolve<Repository<Article>>();
            CbArticle.ItemsSource = articleLigneRepository.SelectAll() ;
        }
    }
}

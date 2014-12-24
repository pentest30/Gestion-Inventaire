using System;
using System.Windows;
using BLL;
using GM.Entity.Models;
using Microsoft.Practices.Unity;

namespace GM.UI.Views
{
    /// <summary>
    /// Interaction logic for CommndeInterneLigneFrm.xaml
    /// </summary>
    public partial class CommndeInterneLigneFrm
    {
        public delegate void UpdateDg(int id);
        public BonEntreeLigneFrm.UpdateDg UpdateDataDg;
        private readonly Repository<CommandeLigne> _beLigneRepository;
        public CommndeInterneLigneFrm(int id , CommandeLigne ligneSortie )
        {
            InitializeComponent();
            var container = new UnityContainer();
            _beLigneRepository = container.Resolve<Repository<CommandeLigne>>();
            var factory = container.Resolve<EntityFactory<CommandeLigne>>();
            var articleLigneRepository = container.Resolve<Repository<Article>>();
            if (ligneSortie.Id == 0)
            {
                var ligne = factory.Create();
                ligne.CommandeInterneId = id;
                Grid.DataContext = ligne;
            }
            else
            {
                Grid.DataContext = ligneSortie;
            }
            CbArticle.ItemsSource = articleLigneRepository.SelectAll();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var ligne = Grid.DataContext as CommandeLigne;
                if (ligne == null)
                {
                    MessageBox.Show("Donneés nulls", "Attention", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (ligne.Id == 0) _beLigneRepository.Insert(ligne);
                else _beLigneRepository.Update(ligne);
                _beLigneRepository.Save();
                if (UpdateDataDg != null) UpdateDataDg(ligne.CommandeInterneId);
            }
            catch (Exception)
            {
                MessageBox.Show("Erreur pendant l'enregistrement des données", "Attention", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}

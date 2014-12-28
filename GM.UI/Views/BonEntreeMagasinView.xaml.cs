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
    /// Interaction logic for BonEntreeMagasinView.xaml
    /// </summary>
    public partial class BonEntreeMagasinView
    {
        private readonly EntityFactory<BonEntree> _factory;
        private readonly Repository<BonEntree> _beRepository;
        private readonly Repository<BonEntreeLigne> _beLigneRepository;

        public BonEntreeMagasinView()
        {
            InitializeComponent();
            var container = new UnityContainer();
            _factory = container.Resolve<EntityFactory<BonEntree>>();
            _beRepository = container.Resolve<Repository<BonEntree>>();
            _beLigneRepository = container.Resolve<Repository<BonEntreeLigne>>();
            var magsinRepository = container.Resolve<Repository<Magasin>>();
            DataGrid.ItemsSource =new ObservableCollection<BonEntree>( _beRepository.SelectAll());
            CbMagasin.ItemsSource = magsinRepository.SelectAll();
        }

        private void UpdateDG(long id)
        {
            DataGridLignes.ItemsSource = _beLigneRepository.Find(x => x.BonEntreeId ==id);
        }

        private void AddButton_OnClick(object sender, RoutedEventArgs e)
        {
            AddButton.Visibility = Visibility.Hidden;
            DeleteButton.Visibility = Visibility.Hidden;
            var list = DataGrid.ItemsSource.OfType<BonEntree>().ToList();
            list.Add(_factory.Create());
            var lastOrDefault = _beRepository.SelectAll().OrderBy(x => x.NBon).LastOrDefault();
            if (lastOrDefault != null)
                list.Last().NBon = lastOrDefault.NBon+1;
            else
            {
                list.Last().NBon = 1;
            }
            Grid.DataContext = list.Last();  
        }

        private void UpdateButton_OnClick(object sender, RoutedEventArgs e)
        {
           if (!CheckSelectedItem())return;
            AddButton.Visibility = Visibility.Hidden;
            UpdateButton.Visibility = Visibility.Hidden;
            DeleteButton.Visibility = Visibility.Hidden;
        }

        private void SaveButton_OnClick(object sender, RoutedEventArgs e)
        {
            var item = (BonEntree)Grid.DataContext;
           
            if (item.Id <= 0)
            {
                _beRepository.Insert(item);
                //((ObservableCollection<BonEntree>)DataGrid.ItemsSource).Add(item);
            }
            else
            {
                _beRepository.Update(item);
            }
            try
            {
                _beRepository.Save();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Erreurs pendant l'enregistrement", MessageBoxButton.OK,
                    MessageBoxImage.Error);
                //((ObservableCollection<BonEntree>)DataGrid.ItemsSource).Remove(item);
            }

            AddButton.Visibility = Visibility.Visible;
            DataGrid.ItemsSource = new ObservableCollection<BonEntree>(_beRepository.SelectAll());
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
            if (!CheckSelectedItem()) return;
            var result = MessageBox.Show("Est vous sure!", "Warning", MessageBoxButton.YesNo,
                MessageBoxImage.Warning);
            if (!result.ToString().Equals("Yes")) return;
            var entree = DataGrid.SelectedItem as BonEntree;
            if (entree == null) return;
            _beRepository.Delete(entree.Id);
            ((ObservableCollection<BonEntree>)DataGrid.ItemsSource).Remove(entree);
            _beRepository.Save();
        }

        private void LBonAddBtn_OnClick(object sender, RoutedEventArgs e)
        {
            if (CheckSelectedItem()) return;
            var be = DataGrid.SelectedItem as BonEntree;
           if(be== null) return;
           var frm = new BonEntreeLigneFrm(be.Id);
           frm.UpdateDataDg += UpdateDG;
           frm.ShowDialog();

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

        private void DataGrid_OnSelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            try
            {
                var item = DataGrid.SelectedItem as BonEntree;
                if (item != null) DataGridLignes.ItemsSource = _beLigneRepository.Find(x => x.BonEntreeId == item.Id);
            }
            catch (Exception)
            {
            }
            //else return;
        }

      

        private void BtnModifierBeLigne_OnClick(object sender, RoutedEventArgs e)
        {

            if (DataGridLignes.SelectedIndex < 0)
            {
                MessageBox.Show("Selectionner un champ", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            var ligne = (DataGridLignes.SelectedItem) as BonEntreeLigne;
            var frm = new BonEntreeLigneFrm(ligne);
            frm.UpdateDataDg += UpdateDG;
            frm.ShowDialog();


        }

        private void DeleteBeLignesButton_OnClick(object sender, RoutedEventArgs e)
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
            _beLigneRepository.Delete(ligne.Id);
            _beLigneRepository.Save();
            DataGridLignes.ItemsSource =_beLigneRepository.SelectAll();
        }
    }
}

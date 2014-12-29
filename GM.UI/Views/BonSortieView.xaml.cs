using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using BLL;
using DAL.Migrations;
using GM.Entity.Models;
using GM.UI.ModelView;
using GM.UI.Views.ReportUserControls;
using GM.UI.Views.ReportUserCOntrols;
using Microsoft.Practices.Unity;

namespace GM.UI.Views
{
    /// <summary>
    /// Interaction logic for BonSortieView.xaml
    /// </summary>
    public partial class BonSortieView
    {
        private readonly EntityFactory<BonSortie> _factory;
        private readonly Repository<BonSortie> _bsRepository;
        private readonly Repository<BonSortieLigne> _bsLigneRepository;
        public BonSortieView()
        {
            InitializeComponent();
            var container = new UnityContainer();
            _factory = container.Resolve<EntityFactory<BonSortie>>();
            _bsRepository = container.Resolve<Repository<BonSortie>>();
            _bsLigneRepository = container.Resolve<Repository<BonSortieLigne>>();
            var magasinRepository = container.Resolve<Repository<Magasin>>();
            var commandeRepository = container.Resolve<Repository<CommandeInterne>>();
            DataGrid.ItemsSource = new ObservableCollection<BonSortie>(_bsRepository.SelectAll());
            CbMagasin.ItemsSource = magasinRepository.SelectAll();
            CbCommande.ItemsSource = commandeRepository.SelectAll();
        }

        private void DataGrid_OnSelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            try
            {
                var item = DataGrid.SelectedItem as BonSortie;
                if (item != null) DataGridLignes.ItemsSource = _bsLigneRepository.GetAllLazyLoad(x => x.Departement, x => x.Service, x => x.Article).Where(x => x.BonSortieId == item.Id).ToList();
        
            }
            catch (Exception)
            {
            }
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

        private void AddButton_OnClick(object sender, RoutedEventArgs e)
        {
            AddButton.Visibility = Visibility.Hidden;
            DeleteButton.Visibility = Visibility.Hidden;
            var list = DataGrid.ItemsSource.OfType<BonSortie>().ToList();
            list.Add(_factory.Create());
            var lastOrDefault = _bsRepository.SelectAll().OrderBy(x => x.NBon).LastOrDefault();
            if (lastOrDefault != null)
                list.Last().NBon = lastOrDefault.NBon + 1;
            else
            {
                list.Last().NBon = 1;
            }
            Grid.DataContext = list.Last();  
        }

        private void SaveButton_OnClick(object sender, RoutedEventArgs e)
        {
            var item = (BonSortie)Grid.DataContext;

            if (item.Id <= 0)
            {
                _bsRepository.Insert(item);
                //((ObservableCollection<BonEntree>)DataGrid.ItemsSource).Add(item);
            }
            else
            {
                _bsRepository.Update(item);
            }
            try
            {
                _bsRepository.Save();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Erreurs pendant l'enregistrement", MessageBoxButton.OK,
                    MessageBoxImage.Error);
                //((ObservableCollection<BonEntree>)DataGrid.ItemsSource).Remove(item);
            }

            AddButton.Visibility = Visibility.Visible;
            DataGrid.ItemsSource = new ObservableCollection<BonSortie>(_bsRepository.SelectAll());
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
        private bool CheckSelectedItem()
        {
            if (DataGrid.SelectedIndex < 0)
            {
                MessageBox.Show("Selectionner un champ", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return true;
            }
            return false;
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
            var entree = DataGrid.SelectedItem as BonSortie;
            if (entree == null) return;
            _bsRepository.Delete(entree.Id);
            ((ObservableCollection<BonSortie>)DataGrid.ItemsSource).Remove(entree);
            _bsRepository.Save();
        }

        private void LBonAddBtn_OnClick(object sender, RoutedEventArgs e)
        {
            if (CheckSelectedItem()) return;
            var be = DataGrid.SelectedItem as BonSortie;
            if (be == null) return;
            var frm = new BonSortieLigneFrm(be.Id , new BonSortieLigne());
            frm.UpdateDataDg += UpdateDG;
            frm.ShowDialog();
        }

        private void UpdateDG(long id)
        {
            DataGridLignes.ItemsSource = _bsLigneRepository.GetAllLazyLoad(x => x.BonSortie, x => x.Departement, x => x.Service, x => x.Article).Where(x => x.BonSortieId == id).ToList();
        }

        private void DeleteBeLignesButton_OnClick(object sender, RoutedEventArgs e)
        {
            
        }

        private void BtnModifierBeLigne_OnClick(object sender, RoutedEventArgs e)
        {
            
        }

        private void BtnPrint_OnClick(object sender, RoutedEventArgs e)
        {
            if (DataGrid.SelectedIndex < 0)
            {
                MessageBox.Show("Selectionner un champ", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            var list =DataGridLignes.ItemsSource.OfType<BonSortieLigne>().OrderBy(x => x.Article.Libelle);
            var result = AutoMapper.Mapper.Map<IList<BonSortieReportView>>(list);
            var frm = new ReportFrm();
            var ucReport = new BonSortieReportUc(result);
            frm.ContentControl.Content = ucReport;
            frm.ShowDialog();
        }
    }
}

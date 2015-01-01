using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using BLL;
using GM.Entity.Models;
using GM.UI.ModelView;
using Microsoft.Practices.Unity;
namespace GM.UI.Views
{
    /// <summary>
    /// Interaction logic for ChangementPiceFrm.xaml
    /// </summary>
    public partial class ChangementPiceFrm
    {
        private readonly HorsStockView _piece;
        private readonly StockService _stockService;
        private readonly PieceService _pieceService;
        private readonly ArticleService _articleService;
        private static IRepository<PieceEmployee> _pServiRepository;
        private readonly IRepository<HistoriqueInventaire> _historiqueRepository;
        private readonly Repository<SousCategorie> _sousCategorieRepository;
        private readonly Repository<SousService> _sousServeiceRepository;
        public BonEntreeLigneFrm.UpdateDg UpdateDataDg { get; set; }
      
        public ChangementPiceFrm(HorsStockView piece)
        {
            
            InitializeComponent();
            _piece = piece;
            var container = new UnityContainer();
            var magasinRepository = container.Resolve<Repository<Magasin>>();
             var marqueRepository = container.Resolve<Repository<Marque>>();
            _historiqueRepository = container.Resolve<Repository<HistoriqueInventaire>>();
            _stockService = container.Resolve<StockService>();
            _sousCategorieRepository = container.Resolve<Repository<SousCategorie>>();
            _pieceService = container.Resolve<PieceService>();
            _articleService = container.Resolve<ArticleService>();
            _sousServeiceRepository = container.Resolve<Repository<SousService>>();
            _pServiRepository = container.Resolve<Repository<PieceEmployee>>();
            CbMagasin.ItemsSource = magasinRepository.SelectAll();
            CbMarque.ItemsSource = marqueRepository.SelectAll();
          
            // CbArticle.ItemsSource = articleService.SelectAll();
        }

        private void CbMarque_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CbMarque.SelectedIndex ==-1)return;
            var marque = CbMarque.SelectedItem as Marque;
            var sousCategorie = _sousCategorieRepository.FindSingle(x => x.Libelle == _piece.SousCategorie);
            CbArticle.ItemsSource = _articleService.Find(x => marque != null && x.MarqueId == marque.Id&&x.SousCategorieId==sousCategorie.Id);
        }

        private void AddBtn_OnClick(object sender, RoutedEventArgs e)
        {
            if (DefautRbtn.IsChecked == false && FetormeRbtb.IsChecked == false && RepareRbtn.IsChecked == false)
            {
                MessageBox.Show("désigner la raison de changement stp", "Attention", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (CbArticle.SelectedIndex == -1 && CbMarque.SelectedIndex == -1 && CbPices.SelectedIndex == -1)
            {
                MessageBox.Show("Valeures vides","Attention" ,MessageBoxButton.OK,MessageBoxImage.Error);
                return;
            }
            try
            {
                if (WorksOnSource()) return;
                DoWorksOnDist();
                if (UpdateDataDg != null) UpdateDataDg(0);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Attention", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DoWorksOnDist()
        {
            var pieceMagasin = CbPices.SelectedItem as PieceMagasin;

            if (pieceMagasin == null) return;
            pieceMagasin.Disponibilite = false;
            UpdateStock(pieceMagasin);
            var pieceRechange = new PieceEmployee
            {
                PieceId = pieceMagasin.PieceId,
                ServiceId = _piece.ServiceId,
                DepartementId = _piece.DepartementId,
                SousServiceId = _piece.SousServiceId,
                Date = DateTime.Now.Date,
                Utilisation = true
            };
            var historiqueDist = new HistoriqueInventaire
            {
                CodeLocation = _sousServeiceRepository.FindSingle(x => x.Id == _piece.SousServiceId).Code,
                Inventaire = _pieceService.FindReturnSingle(x => x.Id == pieceMagasin.PieceId).NInventaire,
                LocationId = Convert.ToInt32(pieceRechange.SousServiceId),
                Date = DateTime.Now
            };
            AdHistoryInventaire(historiqueDist);
            AddUpdatePieceService(pieceRechange);
        }

        private static void AddUpdatePieceService(PieceEmployee pieceRechange)
        {
            if (pieceRechange .Id ==0 )
            _pServiRepository.Insert(pieceRechange);
            _pServiRepository.Save();
        }

        private void UpdateStock(PieceMagasin pieceMagasin)
        {
            _stockService.Update(pieceMagasin);
            _stockService.Save();
        }

        private bool WorksOnSource()
        {
            var historqiqueSource = new HistoriqueInventaire
            {
                CodeLocation = ((Magasin) CbMagasin.SelectedItem).Code,
                Inventaire = _piece.Inventaire,
                LocationId = ((Magasin) CbMagasin.SelectedItem).Id,
                Date = DateTime.Now
            };
           // var p = _pieceService.FindReturnSingle(x => x.Id == _piece.PieceId);
            //var etatMateriel = new EtatMateriel();

            var stock = _stockService.FindSingle(x => x.PieceId == _piece.PieceId);
            if (stock == null) return true;
            ChekOptions(stock, historqiqueSource);
            AdHistoryInventaire(historqiqueSource);
            UpdateStock(stock);
            //_pieceService.Update(p);
            //_pieceService.Save();
            _pServiRepository.Delete(_piece.Id);
            _pServiRepository.Save();
            return false;
        }

        private void ChekOptions(PieceMagasin stock, HistoriqueInventaire historqiqueSource)
        {
            if (DefautRbtn.IsChecked == true)
            {
                stock.EtatStock = EtatStock.Defaut.ToString();
                historqiqueSource.Etat = EtatStock.Defaut.ToString();
                //p.EtatPiece = etatMateriel.EtatMateriels[4].Etat;
            }
            else if (FetormeRbtb.IsChecked == true)
            {
                stock.EtatStock = EtatStock.Reforme.ToString();
                historqiqueSource.Etat = EtatStock.Reforme.ToString();
                //p.EtatPiece = etatMateriel.EtatMateriels[1].Etat;
            }
            else if (RepareRbtn.IsChecked == true)
            {
                stock.EtatStock = EtatStock.Reparation.ToString();
                historqiqueSource.Etat = EtatStock.Reparation.ToString();
                //p.EtatPiece = etatMateriel.EtatMateriels[2].Etat;
            }
            else if (PerteRbtb.IsChecked == true)
            {
                stock.EtatStock = EtatStock.Perte.ToString();
                historqiqueSource.Etat = EtatStock.Reparation.ToString();
                //p.EtatPiece = etatMateriel.EtatMateriels[2].Etat;
            }
        }

        private void AdHistoryInventaire(HistoriqueInventaire historqiqueSource)
        {
            _historiqueRepository.Insert(historqiqueSource);
            _historiqueRepository.Save();
        }

        private void CbArticle_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           if (CbArticle.SelectedIndex==-1)return;
            var article = CbArticle.SelectedItem as Article;

            CbPices.ItemsSource =new ObservableCollection<PieceMagasin>( LoadData(article));
        }

        private IEnumerable<PieceMagasin> LoadData(Article article)
        {
            try
            {


                return _stockService.Find(x => article != null
                                        && x.ArticleId == article.Id
                                        && x.Disponibilite &&
                                        x.EtatStock == EtatStock.Normal.ToString()) .ToList();
                   
            }
            catch (Exception exception)
            {

                MessageBox.Show(exception.Message);
                return new List<PieceMagasin>();
            }
        }
    }
}

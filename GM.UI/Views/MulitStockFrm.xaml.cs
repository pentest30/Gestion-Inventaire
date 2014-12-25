using System;
using System.Globalization;
using System.Windows;
using AutoMapper;
using GM.Entity.Models;
using WPF.Core.Helpers;

namespace GM.UI.Views
{
    /// <summary>
    /// Interaction logic for MulitStockFrm.xaml
    /// </summary>
    public partial class MulitStockFrm
    {
        private readonly Piece _piece  ;
        private readonly int _qnt;
        public BonEntreeLigneFrm.UpdateDg Update;
        public MulitStockFrm(int qnt , Piece piece)
        {
            InitializeComponent();
            TextBox.Text = qnt.ToString(CultureInfo.InvariantCulture);
            _piece = piece;
            _qnt = qnt;

        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
           
            ProgressBar.Minimum = 0;
            ProgressBar.Maximum = _qnt;
            var pBar = new PBar(ProgressBar);
            if (CbArticle.Text == ChoixInventaire.AutoGeneration.ToString())
            {
                ProgressBar.Visibility = Visibility.Visible;
                for (var i = 0; i <Convert.ToInt32( TextBox.Text); i++)
                {
                    var nouveauPeice = NouveauPeice();
                    nouveauPeice.NInventaire = PieceView.GenerateInventoryName(nouveauPeice.Article.Libelle);
                    SavePiece(nouveauPeice);
                    pBar.IncPB();
                }
              
            }
            else if (CbArticle.Text == ChoixInventaire.SuiverInventaireExistant.ToString())
            {
                
            }
            else
            {
                for (var i = 0; i < Convert.ToInt32(TextBox.Text); i++)
                {
                    var nouveauPeice = NouveauPeice();
                    SavePiece(nouveauPeice);
                }
            }
            if (Update != null) Update(0);
            ProgressBar.Visibility = Visibility.Collapsed;
        }

        private Piece NouveauPeice()
        {
            var nouveauPeice = Mapper.Map<Piece>(_piece);
            return nouveauPeice;
        }

        private static void SavePiece(Piece nouveauPeice)
        {
            PieceView.PieceService.Insert(nouveauPeice);
            PieceView.PieceService.Save();
            var stcok = Mapper.Map<PieceMagasin>(nouveauPeice);
            PieceView.StockService.Insert(stcok);
            PieceView.StockService.Save();
        }
    }
}

using System;
using System.Globalization;
using System.Linq;
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
        private int _nr;
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
                ProgressBar.Visibility = Visibility.Visible;
                for (var i = 0; i < Convert.ToInt32(TextBox.Text); i++)
                {
                    var nouveauPeice = NouveauPeice();
                    if (!string.IsNullOrEmpty(nouveauPeice.NInventaire) && nouveauPeice.NInventaire.Contains("/"))
                    {
                        try
                        {
                            if (i == 0) _nr = Convert.ToInt32(nouveauPeice.NInventaire.Split('/')[0]) + 1;
                            else _nr ++;
                            nouveauPeice.NInventaire = _nr +"/"+ nouveauPeice.NInventaire.Split('/')[1];
                        }
                        catch (Exception)
                        {
                            continue;
                        }
                    }
                    else if (!string.IsNullOrEmpty(nouveauPeice.NInventaire))
                    {
                        if (i == 0) _nr = Convert.ToInt32(nouveauPeice.NInventaire) + 1;
                        else _nr++;
                        nouveauPeice.NInventaire = _nr.ToString(CultureInfo.InvariantCulture);
                    }
                 
                    SavePiece(nouveauPeice);
                    pBar.IncPB();
                }

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
            stcok.Disponibilite = true;
            PieceView.StockService.Insert(stcok);
            PieceView.StockService.Save();

        }
    }
}

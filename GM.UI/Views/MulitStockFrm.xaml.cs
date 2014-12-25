using System;
using System.Globalization;
using System.Windows;
using GM.Entity.Models;

namespace GM.UI.Views
{
    /// <summary>
    /// Interaction logic for MulitStockFrm.xaml
    /// </summary>
    public partial class MulitStockFrm
    {
        private readonly Piece _piece  ;
       
        public MulitStockFrm(int qnt , Piece piece)
        {
            InitializeComponent();
            
            TextBox.Text = qnt.ToString(CultureInfo.InvariantCulture);
            _piece = piece;

        }

       

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            AutoMapper.Mapper.CreateMap<Piece, Piece>()
                //.ForMember(x => x.Article, o => o.MapFrom(s => s.Article))
                //.ForMember(x => x.Magasin, o => o.MapFrom(s => s.Magasin))
                .ForMember(x => x.Id, o => o.Ignore())
                .ForMember(x => x.NInventaire, o => o.Ignore());
            if (CbArticle.Text == ChoixInventaire.AutoGeneration.ToString())
            {
                for (var i = 0; i <Convert.ToInt32( TextBox.Text); i++)
                {
                    var nouveauPeice = AutoMapper.Mapper.Map<Piece>(_piece);
                    nouveauPeice.NInventaire = PieceView.GenerateInventoryName(nouveauPeice.Article.Libelle);
                    PieceView._pieceService.Insert(nouveauPeice);
                    PieceView._pieceService.Save();
                }

            }
            else if (CbArticle.Text == ChoixInventaire.SuiverInventaireExistant.ToString())
            {
                
            }
            else
            {
                
            }
        }
    }
}

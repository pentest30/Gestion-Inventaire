using System.Windows;
using BLL;
using GM.Entity.Models;
using Microsoft.Practices.Unity;

namespace GM.UI.Views
{
    /// <summary>
    /// Interaction logic for ChangeEtatInvfrm.xaml
    /// </summary>
    public partial class ChangeEtatInvfrm
    {
        private static  StockService _stockService;
        private readonly PieceMagasin _piece;
        public BonEntreeLigneFrm.UpdateDg UpdateDataDg { get; set; }
        public ChangeEtatInvfrm(long id)
        {
            InitializeComponent();
            var container = new UnityContainer();
            _stockService = container.Resolve<StockService>();
            _piece = _stockService.SelectById(id);
        }

        private void SaveBtn_OnClick(object sender, RoutedEventArgs e)
        {
            if (ComboBox.SelectedIndex ==-1 || _piece==null)return;
            if (ComboBox.SelectedIndex == 0)
            {
                _piece.EtatStock = EtatStock.Normal.ToString();
                _piece.Disponibilite = true;
            }
            else
            {
                _piece.EtatStock = EtatStock.Reforme.ToString();
                _piece.Disponibilite = false;
            }
            _stockService.Update(_piece);
            _stockService.Save();
            if (UpdateDataDg != null) UpdateDataDg(0);
        }
    }
}

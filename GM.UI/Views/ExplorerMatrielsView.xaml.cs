using System.Linq;
using System.Windows;
using GM.UI.ModelView;
using GM.UI.Views.ReportUserControls;

namespace GM.UI.Views
{
    /// <summary>
    /// Interaction logic for ExplorerMatrielsView.xaml
    /// </summary>
    public partial class ExplorerMatrielsView
    {
      
        public TreeViewModel ViewModel { get; set; }

        public ExplorerMatrielsView()
        {
            InitializeComponent();
            ViewModel = new TreeViewModel();
            DataContext = ViewModel;

        }


        private void UpdateButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (DataGridStock.SelectedIndex < 0)
            {
                MessageBox.Show("Selectionner un champ", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            var frm = new ChangeServiceFrm(((HorsStockView)DataGridStock.SelectedItem).Id);
            frm.UpdateDataDg += ViewModel.UpdateDatagrid;
            frm.ShowDialog();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var list = DataGridStock.Items.OfType<HorsStockView>();
            var frm = new ReportFrm();
            var ucReport = new ReportParServiceUc(list);
            frm.ContentControl.Content = ucReport;
            frm.ShowDialog();
        }
    }
}

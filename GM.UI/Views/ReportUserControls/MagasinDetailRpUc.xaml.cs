using System;
using System.Collections.Generic;
using GM.UI.ModelView;

namespace GM.UI.Views.ReportUserControls
{
    /// <summary>
    /// Interaction logic for MagasinDetailRpUc.xaml
    /// </summary>
    public partial class MagasinDetailRpUc
    {
        public MagasinDetailRpUc(IEnumerable<StockModelView> articles)
        {
            InitializeComponent();

            ReportViewer.LocalReport.ReportPath = Environment.CurrentDirectory + @"\Reporting\rpMagasinDetaiis.rdlc";
            ReportViewer.LocalReport.DataSources.Clear();
            ReportViewer.LocalReport.DataSources.Add(new Microsoft.Reporting.WinForms.ReportDataSource("DataSet1",
                articles));
            ReportViewer.RefreshReport();
            ReportViewer.Show();
        }
    }
}

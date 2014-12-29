using System.Collections.Generic;
using GM.UI.ModelView;

namespace GM.UI.Views.ReportUserControls
{
    /// <summary>
    /// Interaction logic for ArticleReportUc.xaml
    /// </summary>
    public partial class ArticleReportUc
    {
        public ArticleReportUc(IEnumerable<ArticleViewModel> articles)
        {
            InitializeComponent();
            ReportViewer.LocalReport.ReportPath =
               @"C:\Users\pentest30\Documents\Visual Studio 2013\Projects\GM.App\GM.UI\Reporting\ReportMagasin.rdlc";
            ReportViewer.LocalReport.DataSources.Clear();
            ReportViewer.LocalReport.DataSources.Add(new Microsoft.Reporting.WinForms.ReportDataSource("DataSet1",
                articles));
            ReportViewer.RefreshReport();
            ReportViewer.Show();
        }
    }
}

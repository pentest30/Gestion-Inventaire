using System;
using System.Collections.Generic;
using GM.UI.ModelView;

namespace GM.UI.Views
{
    /// <summary>
    /// Interaction logic for ReportFrm.xaml
    /// </summary>
    public partial class ReportFrm
    {
        public ReportFrm(IEnumerable<ArticleViewModel> articles)
        {
            InitializeComponent();
            ReportViewer.LocalReport.ReportPath = Environment.CurrentDirectory + @"\ReportMagasin.rdlc";
            ReportViewer.LocalReport.DataSources.Clear();
            ReportViewer.LocalReport.DataSources.Add(new Microsoft.Reporting.WinForms.ReportDataSource("DataSet1",
                articles));
            ReportViewer.RefreshReport();
            ReportViewer.Show();
        }
    }
}

using System;
using System.Collections.Generic;
using GM.UI.ModelView;
using Microsoft.Reporting.WinForms;

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
            
            ReportViewer.LocalReport.ReportPath = ReportViewer.LocalReport.ReportPath = Environment.CurrentDirectory + @"\Reporting\ReportMagasin.rdlc";
            ReportViewer.LocalReport.DataSources.Clear();
            ReportViewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet1",
                articles));
            ReportViewer.RefreshReport();
            ReportViewer.Show();
        }
    }
}

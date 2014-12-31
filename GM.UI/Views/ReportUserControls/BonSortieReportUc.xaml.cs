using System;
using System.Collections.Generic;
using GM.UI.ModelView;

namespace GM.UI.Views.ReportUserCOntrols
{
    /// <summary>
    /// Interaction logic for BonSortieReportUc.xaml
    /// </summary>
    public partial class BonSortieReportUc
    {
        public BonSortieReportUc(IEnumerable<BonSortieReportView> articles)
        {
            InitializeComponent();
            ReportViewer.LocalReport.ReportPath = ReportViewer.LocalReport.ReportPath = Environment.CurrentDirectory + @"\Reporting\RpBonSortie.rdlc";
            ReportViewer.LocalReport.DataSources.Clear();
            ReportViewer.LocalReport.DataSources.Add(new Microsoft.Reporting.WinForms.ReportDataSource("DataSet1",
                articles));
            ReportViewer.RefreshReport();
            ReportViewer.Show();
        }
    }
}

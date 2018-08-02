using System.Windows;
using System.Windows.Controls;

namespace NationalInstruments.Examples.GenerateMAXReport
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            worker = new ReportWorker();
            mainGrid.DataContext = worker;
        }

        private ReportWorker worker;

        private void OnGenerateReportClick(object sender, RoutedEventArgs e)
        {

            if (ReportTypeBox.Text == "XML")
            {
                worker.reportType = NationalInstruments.SystemConfiguration.ReportType.Xml;
            }
            else if (ReportTypeBox.Text == "HTML")
            {
                worker.reportType = NationalInstruments.SystemConfiguration.ReportType.Html;
            }
            else if (ReportTypeBox.Text == "Technical Support")
            {
                worker.reportType = NationalInstruments.SystemConfiguration.ReportType.TechnicalSupportZip;
            }
            else
            {
                worker.reportType = NationalInstruments.SystemConfiguration.ReportType.Xml;
            }

            worker.GenerateReport(passwordBox.Password);
            

        }

        private void OnOverwriteClick(object sender, RoutedEventArgs e)
        {

        }

    }
}

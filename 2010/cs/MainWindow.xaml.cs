using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;
using NationalInstruments.SystemConfiguration;

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

        private void OnBrowseClick(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileExplorer = new OpenFileDialog();
            fileExplorer.Title = "Choose a report directory and filename";
            fileExplorer.CheckFileExists = false;
            fileExplorer.ShowDialog();
            filePathBox.Text = fileExplorer.FileName;
        }

        private void OnGenerateReportClick(object sender, RoutedEventArgs e)
        {

            if (ReportTypeBox.Text == "HTML")
            {
                worker.ReportType = ReportType.Html;
            }
            else if (ReportTypeBox.Text == "Technical Support")
            {
                worker.ReportType = ReportType.TechnicalSupportZip;
            }
            else
            {
                worker.ReportType = ReportType.Xml;
            }

            worker.GenerateReport(passwordBox.Password);
        }
    }
}

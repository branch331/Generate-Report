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
        private string reportTypeExtension;

        private void OnBrowseClick(object sender, RoutedEventArgs e)
        {
            ReturnReportType();
            OpenFileDialog fileExplorer = new OpenFileDialog();
            fileExplorer.Title = "Choose a report directory and filename";
            fileExplorer.CheckFileExists = false;
            // Restrict file type of file dialog. For instance, the filter for XML is: "XML files (*.xml)|*.xml".
            fileExplorer.Filter = reportTypeExtension + " files (*." + reportTypeExtension + ")|*." + reportTypeExtension; 
            fileExplorer.ShowDialog();
            filePathBox.Text = fileExplorer.FileName;
        }

        private void OnGenerateReportClick(object sender, RoutedEventArgs e)
        {
            worker.ReportType = ReturnReportType();
           
            if (string.IsNullOrEmpty(filePathBox.Text))
            {
                filePathBox.Text = System.IO.Path.GetTempPath() + "MAXReport." + reportTypeExtension; 
            }

            worker.GenerateReport(passwordBox.Password);
        }

        private ReportType ReturnReportType()
        {
            if (ReportTypeBox.Text == "XML")
            {
                reportTypeExtension = "xml";
                return ReportType.Xml;
            }
            else if (ReportTypeBox.Text == "Technical Support")
            {
                reportTypeExtension = "zip";
                return ReportType.TechnicalSupportZip;
            }
            else
            {
                reportTypeExtension = "html";
                return ReportType.Html;
            }
        }
    }
}

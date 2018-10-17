using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;
using NationalInstruments.SystemConfiguration;

namespace NationalInstruments.Examples.GenerateMAXReport
{
    public partial class MainWindow : Window
    {
        private ReportWorker worker;
        private string reportTypeExtension;

        public MainWindow()
        {
            InitializeComponent();
            worker = new ReportWorker();
            mainGrid.DataContext = worker;
        }

        private void OnBrowseClick(object sender, RoutedEventArgs e)
        {
            GetReportType(); // Get Report Type to coerce Browse window to allow only the specified file type.
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
            worker.ReportType = GetReportType();

            if (string.IsNullOrEmpty(filePathBox.Text))
            {
                filePathBox.Text = System.IO.Path.GetTempPath() + "MAXReport." + reportTypeExtension;
            }

            worker.GenerateReport(passwordBox.Password);
        }

        private ReportType GetReportType()
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

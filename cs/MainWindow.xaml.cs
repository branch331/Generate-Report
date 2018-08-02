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
            worker.GenerateReport(passwordBox.Password);
            //System.Environment.Exit(0);
        }

        private void OnOverwriteClick(object sender, RoutedEventArgs e)
        {

        }

    }
}

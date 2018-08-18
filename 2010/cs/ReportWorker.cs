using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using NationalInstruments.SystemConfiguration;

namespace NationalInstruments.Examples.GenerateMAXReport
{
    class ReportWorker : INotifyPropertyChanged
    {
        private bool canGenerateReport;
        public event PropertyChangedEventHandler PropertyChanged;

        public ReportWorker()
        {
            CanGenerateReport = true;
        }

        public string Target
        {
            get;
            set;
        }

        public string Username
        {
            get;
            set;
        }

        public string FilePath
        {
            get;
            set;
        }

        public bool Overwrite
        {
            get;
            set;
        }

        public bool CanGenerateReport
        {
            get { return canGenerateReport; }
            set
            {
                if (canGenerateReport != value)
                {
                    canGenerateReport = value;
                    NotifyPropertyChanged("CanGenerateReport");
                }
            }
        }

        public ReportType reportType
        {
            get;
            set;
        }

        public void GenerateReport(string password)
        {
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += new DoWorkEventHandler(delegate(object o, DoWorkEventArgs args)
            {
                CanGenerateReport = false;
                try
                {
                    SystemConfiguration.SystemConfiguration session = new SystemConfiguration.SystemConfiguration(Target, Username, password);
                    session.GenerateMAXReport(reportType, FilePath, Overwrite); 
                }
                catch (SystemConfigurationException ex)
                {
                    string errorMessage = string.Format("GenerateMAXReport threw a System Configuration Exception.\n\nError Code: {0:X}\n{1}", ex.ErrorCode, ex.Message);
                    MessageBox.Show(errorMessage, "System Configuration Exception");
                }
                finally
                {
                    CanGenerateReport = true;
                }
            }
            );
            worker.RunWorkerAsync();
        }
    

        protected virtual void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

    }
}
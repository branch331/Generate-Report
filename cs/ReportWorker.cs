using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using NationalInstruments.SystemConfiguration;

namespace NationalInstruments.Examples.GenerateMAXReport
{
    class ReportWorker : INotifyPropertyChanged
    {
        //private string status;
        //private bool exception;
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
        /*
        public string Status
        {
            get { return status; }
            set
            {
                if (status != value)
                {
                    status = value;
                    NotifyPropertyChanged("Status");
                }
            }
        }
        */
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

        /*
        private void worker_RunWorkerCompleted(object o, RunWorkerCompletedEventArgs e)
        {
            if (!exception)
            {
                Status = "Complete!";
            }
            else
            {
                Status = "Error Generating Report.";
            }
        }
        */

        public void GenerateReport(string password)
        {
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += new DoWorkEventHandler(delegate(object o, DoWorkEventArgs args)
            {
                CanGenerateReport = false;
                try
                {
                    SystemConfiguration.SystemConfiguration session = new SystemConfiguration.SystemConfiguration(Target, Username, password);
                    session.GenerateMAXReport(reportType, FilePath, Overwrite); //0 - xml, 1-html, 2-technicalsupportzip
                }
                catch (SystemConfigurationException ex)
                {
                    string errorMessage = string.Format("GenerateMAXReport threw a System Configuration Exception.\n\nError Code: {0:X}\n{1}", ex.ErrorCode, ex.Message);
                    MessageBox.Show(errorMessage, "System Configuration Exception");
                    //exception = true;
                }
                finally
                {
                    CanGenerateReport = true;
                }
            }
            );
            //worker.RunWorkerCompleted += worker_RunWorkerCompleted;
            //Status = "Generating Report...";
            //exception = false;
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
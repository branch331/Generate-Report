using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using NationalInstruments.SystemConfiguration;

namespace NationalInstruments.Examples.GenerateMAXReport
{
    class ReportWorker
    {
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
                    try
                    {
                        SystemConfiguration.SystemConfiguration session = new SystemConfiguration.SystemConfiguration(Target, Username, password);
                        session.GenerateMAXReport(reportType, FilePath, Overwrite); //0 - xml, 1-html, 2-technicalsupportzip
                    }
                    catch (SystemConfigurationException ex)
                    {
                        string errorMessage = string.Format("GenerateMAXReport threw a System Configuration Exception.\n\nError Code: {0:X}\n{1}", ex.ErrorCode, ex.Message);
                        MessageBox.Show(errorMessage, "System Configuration Exception");
                    }
                }
            );
        worker.RunWorkerAsync();
        }
    }
}
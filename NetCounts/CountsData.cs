using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Collections.ObjectModel;

using System.ComponentModel;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using OfficeOpenXml;
using Microsoft.Win32;
using System.Data;
using CsvHelper;

namespace NetCounts
{
    public class AnalyticsCountsStructure
    {

        public string DOCKET { get; set; }
        public string JOB { get; set; }
        public string APPEAL { get; set; }
        public string PACKAGE { get; set; }
        public string SOURCE { get; set; }
        public string PTP { get; set; }
        public string DATAPANEL { get; set; }
        public int QTY { get; set; }
    }

    public class CountsData : INotifyPropertyChanged
    {
        // Fields
        private string csvFileName;
        private string reportFileName;
        private ExcelPackage xlsxPackage;

        private ObservableCollection<AnalyticsCountsStructure> m_results;
        private string m_hasData = "False";

        // Properties
        public ObservableCollection<AnalyticsCountsStructure> Results
        {
            get
            {
                return m_results;
            }
        }

        public string HasData
        {
            get
            {
                return m_hasData;
            }
        }

        // INotifyPropertyChanged Implementation
        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        // Constructors
        public CountsData()
        {
            m_results = new ObservableCollection<AnalyticsCountsStructure>();
            // this.ChooseFile();
        }

        // Members
        public void Clear()
        {
            m_results.Clear();
        }

        public string ChooseFile()
        {

            // Use the open file dialog box to choose a file
            // Configure dialog box parameters
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.DefaultExt = ".csv";
            dlg.Filter = "CSV Files (.csv)|*.csv";

            // Show the dialog box
            Nullable<bool> result = dlg.ShowDialog();

            // If the user chooses a file successfully, assign it to csvFileName.
            // Otherwise the default (empty) string is returned;
            if (result == true)
            {
                csvFileName = dlg.FileName;
            }
            return csvFileName;
        }

        public void ReadFile()
        {
            // Ensure a file name has been populated
            if (String.IsNullOrEmpty(csvFileName))
            {
                return;
            }

            // Empty out results
            Clear();

            // Read the file, row by row, using CsvHelper.CsvReader
            try
            {
                using (StreamReader reader = File.OpenText(csvFileName))
                {
                    var csv = new CsvReader(reader);
                    while (csv.Read())
                    {
                        var record = csv.GetRecord<AnalyticsCountsStructure>();
                        m_results.Add(record);
                    }
                }
                if (m_results.Count > 0)
                {
                    m_hasData = "True";
                    NotifyPropertyChanged("Results");
                    NotifyPropertyChanged("HasData");
                }
            }
            catch (IOException ex)
            {
                MessageBox.Show("[ERROR]: " + ex.Message, "File Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


    }
}

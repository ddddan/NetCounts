using System;
using System.Collections.Generic;
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

    public class CountsData
    {
        // Fields
        private string csvFileName;
        private string reportFileName;
        private List<AnalyticsCountsStructure> m_results;
        private AnalyticsCountsStructure csvFile;
        private DataSet csvData;
        private ExcelPackage xlsxPackage;

        // Properties
        public List<AnalyticsCountsStructure> Results
        {
            get
            {
                return m_results;
            }
        }

        // Constructors
        public CountsData()
        {
            m_results = new List<AnalyticsCountsStructure>();
            this.ChooseFile();
        }

        // Members
        public void Clear() {
            m_results.Clear();
            csvData.Dispose();
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
            }
            catch (IOException ex)
            {
                MessageBox.Show("[ERROR]: " + ex.Message);
            }
        }
 

    }
}

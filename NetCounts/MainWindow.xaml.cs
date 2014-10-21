using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NetCounts
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private CountsData cData;

        public MainWindow()
        {
            InitializeComponent();

            // Initialize the CountsData object
            cData = new CountsData();

            // Use this object for data binding
            this.DataContext = cData;
        }

        private void btnChooseFile_Click(object sender, RoutedEventArgs e)
        {
            // If data has already been loaded, ensure user wants to choose a different file
            // If not, do not continue
            if (cData.HasData == "True")
            {
                MessageBoxResult result = MessageBox.Show("Counts file has already been loaded. Do you wish to load a new file?", "Counts File Loaded", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.No)
                {
                    return;
                } 
            }

            // Open the file and read the contents
            cData.ChooseFile();
            cData.ReadFile();

        }

        private void btnCreateReport_Click(object sender, RoutedEventArgs e)
        {
            // Open Customize Dialog Box
            CustomizeDlg dlg = new CustomizeDlg();

            dlg.Owner = this;
            dlg.ShowDialog();
            
        }


    }
}

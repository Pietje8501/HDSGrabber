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
using CanvasGrabber.Client.Download;

namespace CanvasGrabber
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private HLSGrabber _grabber;

        public MainWindow()
        {
            _grabber = new HLSGrabber();
            InitializeComponent();
        }

        private async void btnDownload_Click(object sender, RoutedEventArgs e)
        {
            if (chkManifest.IsChecked.Value)
            {
                if (!String.IsNullOrEmpty(txtManifestUri.Text))
                {
                    txtProgress.Text = "Starting grabber...";
                    _grabber.setManifest(txtManifestUri.Text);
                    _grabber.start();
                }
                else
                {
                    txtProgress.Text = "No manifest Uri provided...";
                    progressDownload.Visibility = Visibility.Hidden;
                }
            }
            else
            {
                if(!String.IsNullOrEmpty(txtWebsiteUri.Text)){
                    txtProgress.Text = "Searching for manifest file...";
                    progressDownload.Visibility = Visibility.Visible;
                    bool found = await _grabber.searchManifest(txtWebsiteUri.Text);
                    if(!found){
                        progressDownload.Visibility = Visibility.Hidden;
                        txtProgress.Text = "Manifest not found...";
                    } else {
                        txtProgress.Text = "Starting grabber...";
                        _grabber.start();
                    }
                }
                else
                {
                    txtProgress.Text = "No website Uri provided...";
                    progressDownload.Visibility = Visibility.Hidden;
                }
                
            }
        }


    }
}

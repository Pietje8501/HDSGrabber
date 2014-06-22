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
using CanvasGrabber.MVC.Interfaces;

namespace CanvasGrabber
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, ViewInterface
    {
        private HDSGrabber _grabber;

        public MainWindow()
        {
            _grabber = new HDSGrabber();
            _grabber.Model.AddListener(this);
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnDownload_Click(object sender, RoutedEventArgs e)
        {
            //TODO check for correct uri; starts with http etc
            if (chkManifest.IsChecked.Value)
            {
                if (!String.IsNullOrEmpty(txtManifestUri.Text))
                {
                    var success = await _grabber.SetManifest(txtManifestUri.Text);
                    if (success)
                    {
                        _grabber.start();
                    }
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
                    bool found = await _grabber.SearchPlaylist(txtWebsiteUri.Text);
                    if(!found){
                        progressDownload.Visibility = Visibility.Hidden;
                    } else {
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

        public void UpdateView()
        {
            if (!String.IsNullOrEmpty(txtProgress.Text))
            {
                if (txtProgress.Text != _grabber.Model.GrabberStatus)
                {
                    txtProgress.Text = _grabber.Model.GrabberStatus;
                }
            }
        }
        
    }
}
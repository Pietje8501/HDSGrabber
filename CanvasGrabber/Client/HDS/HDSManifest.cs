using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Threading.Tasks;
using CanvasGrabber.MVC;
namespace CanvasGrabber.Client.HDS
{
    class HDSManifest
    {
        private Uri _manifestUri;

        public Uri ManifestUri
        {
            get { return _manifestUri; }
            set { _manifestUri = value; }
        }

        private GrabberModel _model;

        public GrabberModel Model
        {
            get { return _model; }
            set { _model = value; }
        }

        public HDSManifest(string uri)
        {
            ManifestUri = new Uri(uri);
        }

        public async Task<bool> ParseManifest()
        {
            try
            {
                Model.GrabberStatus = "Downloading Manifest";
                WebClient client = new WebClient();
                var manifest = await client.DownloadStringTaskAsync(ManifestUri);
            }
            catch (WebException e)
            {
                Model.GrabberStatus = e.Message;
                return false;
            } 
        }
    }
}

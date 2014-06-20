using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CanvasGrabber.Assets;
using CanvasGrabber.MVC;

namespace CanvasGrabber.Client.Download
{
    class HLSGrabber
    {
        private GrabberModel _model;

        public GrabberModel Model
        {
            get { return _model; }
            set { _model = value; }
        }

        public HLSGrabber()
        {
            Model = new GrabberModel();    
        }

        private Uri _grabberManifestUri;

        public Uri GrabberManifestUri
        {
            get { return _grabberManifestUri; }
            set { _grabberManifestUri = value; }
        }

        public async void start()
        {

        }

        public async Task<bool> SearchPlaylist(string uri)
        {
            try
            {
                WebClient client = new WebClient();
                var result = await client.DownloadStringTaskAsync(new Uri(uri));
                var match = new Regex(Constants.guidIdRegex).Match(result);
                if (match.Success)
                {
                    var guid = match.Value.Split('\'').ToList()[1];
                    SearchManifestUri(Constants.baseUri + guid + Constants.baseUriFormat);
                    return true;
                }
            }
            catch (Exception e)
            {
                Model.GrabberStatus = e.Message;
                return false;
            }
            return false;
        }

        public void SearchManifestUri(string playlistUri)
        {

        }

        public void SetManifest(string uri)
        {
            GrabberManifestUri = new Uri(uri);
        }



    }
}

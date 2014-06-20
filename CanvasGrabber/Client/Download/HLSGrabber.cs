using System;
using System.Data;
using System.Net;
using System.Json;
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
                    var res = await SearchManifestUri(Constants.baseUri + guid + Constants.baseUriFormat);
                    return res;
                }
            }
            catch (Exception e)
            {
                Model.GrabberStatus = e.Message;
                return false;
            }
            return false;
        }

        public async Task<bool> SearchManifestUri(string playlistUri)
        {
            try
            {
                WebClient client = new WebClient();
                string result = await client.DownloadStringTaskAsync(new Uri(playlistUri));
                JsonObject obj = JsonValue.Parse(result) as JsonObject;    
                
            }
            catch (Exception e)
            {
                Model.GrabberStatus = e.Message;
                return false;
            }
            return false;
        }

        public void SetManifest(string uri)
        {
            GrabberManifestUri = new Uri(uri);
        }



    }
}

using System;
using System.Data;
using System.Json;
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

        private List<Uri> _grabberManifestUri;

        public List<Uri> GrabberManifestUri
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
                // System.Json is not a fan of the stuff in front of the braces
                string json = result.Substring(result.IndexOf('(') + 1, result.LastIndexOf(')') - 1 - result.IndexOf('('));
                JsonObject obj = JsonValue.Parse(json) as JsonObject;
                //JsonArray items = (JsonArray)obj[Constants.channelKey][Constants.itemsKey];
                var manifest = obj[Constants.channelKey][Constants.itemsKey][0][Constants.itemKey][Constants.playlistKey];
                var title = obj[Constants.channelKey][Constants.itemsKey][0][Constants.itemKey][Constants.titleKey];
                if (!String.IsNullOrEmpty(manifest.ToString()))
                {
                    Model.GrabberStatus = "Found manifest for " + title;
                    var unescaped = manifest.ToString().Replace("\\","").Replace("\"", "");
                    SetManifest(unescaped);
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

        public void SetManifest(string uri)
        {
            if (GrabberManifestUri == null)
            {
                GrabberManifestUri = new List<Uri>();
            }
            GrabberManifestUri.Add(new Uri(uri));
        }



    }
}

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
using CanvasGrabber.Client.HDS;

namespace CanvasGrabber.Client.Download
{
    class HDSGrabber
    {
        private GrabberModel _model;

        public GrabberModel Model
        {
            get { return _model; }
            set { _model = value; }
        }

        public HDSGrabber()
        {
            Model = new GrabberModel();    
        }

        private HDSManifest _grabberManifest;

        public HDSManifest GrabberManifest
        {
            get { return _grabberManifest; }
            set { _grabberManifest = value; }
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
                // Extract some information in a rather dirty way from Json object
                var manifest    = obj[Constants.channelKey][Constants.itemsKey][0][Constants.itemKey][Constants.playlistKey];
                var title       = obj[Constants.channelKey][Constants.itemsKey][0][Constants.itemKey][Constants.titleKey];
                if (!String.IsNullOrEmpty(manifest.ToString()))
                {
                    Model.GrabberStatus = "Found manifest for " + title;
                    var unescaped = manifest.ToString().Replace("\\","").Replace("\"", "");
                    var success = await SetManifest(unescaped);
                    return success;
                }
            }
            catch (Exception e)
            {
                Model.GrabberStatus = e.Message;
                return false;
            }
            return false;
        }

        public async Task<bool> SetManifest(string uri)
        {
            GrabberManifest = new HDSManifest(uri);
            GrabberManifest.Model = Model;
            var success = await GrabberManifest.ParseManifest();
            if (success)
            {
                Model.GrabberStatus = "Manifest successfully parsed... Starting grabber...";
                return true;
            }
            else
            {
                Model.GrabberStatus = "Error parsing manifest...";
            }
            return false;
        }
    }
}

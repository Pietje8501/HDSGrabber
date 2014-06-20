using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CanvasGrabber.Client.Download
{
    class HLSGrabber
    {
        private Uri _grabberManifestUri;

        public Uri GrabberManifestUri
        {
            get { return _grabberManifestUri; }
            set { _grabberManifestUri = value; }
        }

        public async void start()
        {

        }

        public async Task<bool> searchManifest(string uri)
        {
            return false;
        }

        public void setManifest(string uri)
        {
            GrabberManifestUri = new Uri(uri);
        }



    }
}

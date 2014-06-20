using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CanvasGrabber.Assets
{
    static class Constants
    {
        public const string baseUri         = "http://mp.vrt.be/api/playlist/details/";
        public const string baseUriFormat   = ".json";
        public const string titleKey        = "title";
        public const string playlistKey     = "media_content_url";
        public const string channelKey      = "channel";
        public const string itemsKey        = "items";
        public const string itemKey         = "item";
        public const string guidIdRegex      = "theGuid = '[0-9]+'";
    }
}
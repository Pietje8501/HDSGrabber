using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CanvasGrabber.MVC.Interfaces;

namespace CanvasGrabber.MVC
{
    class GrabberModel : ModelInterface
    {
        private List<ViewInterface> listeners;

        private int nbTotalFragments;

        public int NbTotalFragments
        {
            get { return nbTotalFragments; }
            set { nbTotalFragments = value; }
        }

        private int nbCompletedFragments;

        public int NbCompletedFragments
        {
            get { return nbCompletedFragments; }
            set { nbCompletedFragments = value; }
        }

        private string videoTitle;

        public string VideoTitle
        {
            get { return videoTitle; }
            set { videoTitle = value; }
        }

        public GrabberModel();

        private override void notifyListeners()
        {
            foreach (ViewInterface vi in listeners)
            {
                vi.updateView();
            }
        }

    }
}

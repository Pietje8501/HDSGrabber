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

        private string grabberStatus;

        public string GrabberStatus
        {
            get { return grabberStatus; }
            set { grabberStatus = value; }
        }

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

        public GrabberModel()
        {

        }

        public void NotifyListeners()
        {
            foreach (ViewInterface vi in listeners)
            {
                vi.UpdateView();
            }
        }

        public void AddListener(ViewInterface vi)
        {
            if (!listeners.Contains(vi))
            {
                listeners.Add(vi);
            }
        }

    }
}

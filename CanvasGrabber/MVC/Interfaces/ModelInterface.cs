using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CanvasGrabber.MVC.Interfaces
{
    interface ModelInterface
    {
        public void notifyListeners();
        public void addListener(ViewInterface view);
    }
}

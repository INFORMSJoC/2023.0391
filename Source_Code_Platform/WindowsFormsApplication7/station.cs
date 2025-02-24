using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Timers;
using System.Drawing.Drawing2D;

namespace WindowsFormsApplication7
{
    [Serializable]
    public class station
    {
        public int Stationindex;
        public string StationName;
        public int Dwellmin;
        public int Dwellmax;
        public int YPos;
        public int TrackNum;
        public decimal GetOnDix;
        public int Qs;
        public int Qr;
        public station()
        {
            TrackNum = 1;
            GetOnDix = 0.1M;
            Qs = 1000;
        }
    }
    
}

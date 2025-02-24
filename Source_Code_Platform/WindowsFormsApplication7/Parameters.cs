using System;
using System.Collections.Generic;
using System.Linq;
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
    public class Parameters
    {
        //public System.String[] StationNameYizhuang = 
        //    { "宋家庄", "肖村", "小红门", "旧宫", "亦庄桥", "亦庄文化园", "万源街", "荣京东街", "荣昌东街",
        //"同济南路", "经海路", "次渠南", "次渠"};
        public System.String[] StationNameYizhuang =
    { "四惠东", "四惠", "大望路", "国贸", "永安里", "建国门", "东单", "王府井", "天安门东",
        "天安门西", "西单", "复兴门", "南礼士路", "木樨地", "军博", "公主坟", "万寿路", "五棵松", "玉泉路", "八宝山", "八角", "古城","苹果园"};
        public Font TimeStrFont = new Font("Times New Roman", 15);
        public Font ServiceFont = new Font("Times New Roman", 15);
        public Font StaNameFont = new Font("楷体", 14);
        //public RightToLeft RightToLeft = RightToLeft.Yes;  
        public Decimal StaWidth = new Decimal(1.0);
        public Decimal TimeWidth = new Decimal(1.0);
        public Color StaBush = Color.Green;
        public Color TimeBush = Color.Green;
        public Color VehicleBush = Color.Black;
        public Decimal VehicleWidth = new Decimal(2);
        public int fenge = 1800;
        public decimal Hengdix = 0.06M;
        // 0.2*0.6
        public decimal Zongdix = 0.18M;
        // 0.4* 0.6

        public Color DefaultTrainBush = Color.Red;
        public Decimal DefaultTrainWidth = new Decimal(1);
        public float DavisA = 0;
        public float DavisB = 0.02F;
        public int TimeSec = 20 * 3600 + 3600;
        public int StartTime = 4 * 3600;
        public int ShowStartTime = 4 * 3600;
        public float CurrentTime = 0 * 3600;
        public int PlanDisplayTime = 3600;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication7
{
    [Serializable]
    public class section
    {
        public int SectionIndex;
        public int RubTime;
        public float VelocityLimit = 50;
        public float SectionLength;
        public List<float> Velocity;
        public section()
        {
            Velocity = new List<float>();
        }
    }
}

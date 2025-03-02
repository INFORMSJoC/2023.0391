﻿using System;
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
    public class train
    {
        public int TrainDirection;
        // =0 updirection; =1 downdirection
        public int Trainnidex;
        public string TrainNO;
        public string VehicleNo;
        public List<float> Arrive;
        public List<float> Depart;
        public List<string> PassStations;
        public bool IfSelected;
        public Color ShowColor;
        public decimal ShowWidth;
        public string ShowStyle;
        public int Segment;
        public float Position;
        public float Velocity;
        public float DwellTime;
        public float CurrentDwellTime;
        public float RemainTime;
        public void TrainSimulation()
        {
            // Trains in the depot
            if ((float)this.Arrive[0] > Form1.MySet.CurrentTime)
            {
                this.Segment = 0;
                CurrentDwellTime = 100;
            }
            else if (Segment != PassStations.Count)
            {
                // if ((Form1.MySet.CurrentTime > (float)this.Depart[Segment])&&CurrentDwellTime>=DwellTime)
                if ((Form1.MySet.CurrentTime > (float)this.Depart[Segment]))
                //if(RemainTime<=0)
                // Trains on the segment
                // && this.Velocity == 0
                {
                    this.Segment += 1; // Trains into the next segment
                    this.RemainTime = (float)this.Depart[Segment] - (float)this.Depart[Segment - 1];
                    this.Position = 0;
                    this.Velocity = 0;
                    this.CurrentDwellTime = 0;
                }
                this.TrainDynamics();
            }
        }
        public train()
        {
            Arrive = new List<float>();
            Depart = new List<float>();
            PassStations = new List<string>();
            ShowColor = Color.Red;
            ShowWidth = 3.0M;
            ShowStyle = "虚线";
            Segment = 0;
            Position = 0;
            Velocity = 0;
            RemainTime = 0;
            DwellTime = 30;
            TrainDirection = 0;
            IfSelected = false;
        }
        private void TrainDynamics()
        {
            //int transegment = Convert.ToInt32(this.PassStations[Segment - 1]) - 1- TrainDirection;
            if (TrainDirection == 0)
            {
                Position += (1.0F / (float)Form1.MySet.Hengdix) / Form1.Sections[12 - Segment].RubTime;
            }
            else
            {
                Position += (1.0F / (float)Form1.MySet.Hengdix)/Form1.Sections[Segment-1].RubTime;
            }
            
            if (Position > 1)
            {
                Position = 1;
                CurrentDwellTime += 1;
            }
            Velocity +=1;
            RemainTime -= (1.0F / (float)Form1.MySet.Hengdix);
        }
        //virtual TrainSimulator();
        
    }
}

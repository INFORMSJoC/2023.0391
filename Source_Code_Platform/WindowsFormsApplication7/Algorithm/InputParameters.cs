using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication7.Algorithm
{
    static class InputParameters
    {
        public static double MaxConstValue = 10000000;
        // Basic operation parameters
        public static int[] TrainPosition = {2, 24}; // 初始时刻列车停靠位置
        // public static List<int> TrainPosition = Resheduling.VehicleSegment;
        // public static int TurnDirectionTime = 150; //中转站折返时间200秒
        public static int TurnAroundTime = 150; //车站折返时间300秒
        public static int MinHeadwayTime = 500;
        public static int CurrentTime = 17400;   //初始时刻：T=500
        public static int DwellingTime = 30;
        public static int MaxDwellingTime = 60;
        public static int[] DepotPosition = { 22, 45 };
        public static int[] DepotNum = { 1, 1 };
        // Model parameters
        // Objective parameters
        public static int CancelPenalty = 100000;
        public static int DelayPenalty = 0;
        public static int SegmentTimeVariation = 10;
        public static int TimeUnit = 10;
        public static int MaximumIterationNumber = 10;
        public static int TravelTime(int i, int j) // Arrive at station i, arrive at station j
        {
            int time = 0;
            if (i < j)
            {
                for (int initial = i; initial < j; initial++)
                {
                    time += DwellingTime + Form1.Sections[initial].RubTime;
                }
            }
            else if (i > j)
            {
                Console.WriteLine("ERROR!");
                while (1 == 0) { };
            }
            return time;
        }
    }
}

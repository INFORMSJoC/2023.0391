using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication7.Algorithm
{
    class Tour
    {
        public List<int> PathRoute;
        public List<int> PathArrive;
        public List<int> PathDepart;
        public int ServiceNo;
        public Tour()
        {
            PathRoute = new List<int>();
            PathArrive = new List<int>();
            PathDepart = new List<int>();
            ServiceNo = new int();
        }
    }
    class path
    {
        // Trips to be covered
        public List<Event> PathEvent;
        public List<Tour> TripTour;
        public double ReducedPathCost;
        //int EndDepot;
        public path()
        {
            TripTour = new List<Tour>();
            PathEvent = new List<Event>();
            ReducedPathCost = 0;
            //EndDepot = TripTour[TripTour.Count - 1].PathRoute.Last();
        }
        public int EndDepot(int d)
        {
            int a = 0;
            //if (TripTour.Count != 0)
            {
                if (TripTour[Math.Max(0, TripTour.Count - 1)].PathRoute.Last() == d)
                {
                    a = 1;
                }
            }
            //else
            //{
            //    //Console.WriteLine("A train direct gones to the end");
            //    //if (PathEvent.Last().ArrivalStation == d)
            //    //{
            //    //    a = 1;
            //    //}
            //}

            return a;
        }
        public int PathEventConject(Event e)
        {
            int nopass = 0;
            //foreach (Tour t in TripTour)
            //{
            //    for (int i = 0; i < t.PathRoute.Count; i++)
            //    {
            //        if (e.ArrivalStation  == t.PathRoute[i] && e.ServiceNo == t.ServiceNo)
            //        {
            //            nopass = 1;
            //        }
            //    }
            //}
            foreach (Event ep in PathEvent)
            {
                if (e.index == ep.index)
                {
                    nopass = 1;
                }
            }
            return nopass;
        }
        public int PathEventTime(Event e)
        {
            int time = 0;
            int nopass = 0;
            foreach (Event ep in PathEvent)
            {
                if (e.index == ep.index)
                {
                    time = ep.ArrivalTime;
                }
            }
            //foreach (Tour t in TripTour)
            //{
            //    for (int i = 0; i < t.PathRoute.Count; i++)
            //    {
            //        if (e.ArrivalStation == t.PathRoute[i] && e.ServiceNo == t.ServiceNo)
            //        {
            //            time = t.PathArrive[i];
            //            nopass = 1;
            //        }
            //    }
            //}
            //if (nopass == 0)
            //{
            //    time = 0;
            //}
            return time;
        }
        public int IncludeService(int s)
        {
            int a = 0;
            for (int q = 0; q < this.TripTour.Count; q++)
            {
                if (this.TripTour[q].ServiceNo == s)
                {
                    a = 1;
                }
            }
            return a;
        }
        public int DelayPentalty(List<train> s)
        {
            int objective = 0;

            foreach (Tour t in this.TripTour)
            {
                int ServiceCanclePenaty = 0; int ServiceDelayPenaty = 0;
                for (int i = 0; i < s[t.ServiceNo].PassStations.Count; i++)
                {
                    int ServiceServed = 1;
                    for (int j = 0; j < t.PathRoute.Count; j++)
                    {
                        if (Convert.ToInt32(s[t.ServiceNo].PassStations[i]) - 1 == t.PathRoute[j])
                        {
                            ServiceServed = 0;
                            ServiceDelayPenaty += (t.PathArrive[j] - (int)s[t.ServiceNo].Arrive[i]) * InputParameters.DelayPenalty;
                        }
                    }
                    ServiceCanclePenaty += ServiceServed * InputParameters.CancelPenalty;
                }
                objective += ServiceCanclePenaty + ServiceDelayPenaty;
            }
            return objective;
        }
        public int PathCost(List<Event> Elist)
        {
            int cost = 0;
            foreach (Event e in Elist)
            {
                cost += InputParameters.DelayPenalty * PathEventTime(e) - InputParameters.CancelPenalty * PathEventConject(e);
            }
            return cost;
        }
        public path ptransform()
        {
            if (PathEvent.Count != 0)
            {
                int serviceno = -1;
                Event e = PathEvent[0];
                {
                    if (e.ServiceNo != serviceno)
                    {
                        TripTour.Add(new Tour());
                        {
                            // Transfer event to a new triptour 
                            TripTour.Last().ServiceNo = serviceno;
                            while (e.ArrivalStation >= 0)
                            {
                                TripTour.Last().PathRoute.Add(e.ArrivalStation);
                                TripTour.Last().PathArrive.Add(e.ArrivalTime);
                                TripTour.Last().PathDepart.Add(e.ArrivalTime + 30);
                                TripTour.Last().ServiceNo = e.ServiceNo;
                                e = e.AfterEvent[0];
                            }
                        }
                        serviceno = e.ServiceNo;
                    }
                }
            }
            return this;
        }
        // Arrival and departure times at each station
    }
}

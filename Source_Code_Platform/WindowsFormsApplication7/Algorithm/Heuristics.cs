using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication7.Algorithm
{
    class Heuristics
    {
        public path HoldStrategy(List<train> s, int k)
        // s: planned train schedule
        {
            path q = new path();
            int index = 0;
            for (int i = 0; i < s.Count; i++)
            {
                if (Convert.ToInt32(s[i].VehicleNo) == k + 1)
                {
                    q.TripTour.Add(new Tour());
                    q.TripTour[index].ServiceNo = i;
                    index++;
                }
            }
            int ServiceToTake = q.TripTour[0].ServiceNo;
            if (InputParameters.TrainPosition[k] == Convert.ToInt32(s[ServiceToTake].PassStations[0]) - 1) // Train and service start at the same station
            {
                // To be revised here
                int TimeGap = InputParameters.CurrentTime - (int)s[ServiceToTake].Arrive[0];
                foreach (Tour j in q.TripTour)
                {
                    j.PathRoute.AddRange(s[j.ServiceNo].PassStations.Select<string, int>(x => (Convert.ToInt32(x) - 1)));
                    j.PathArrive.AddRange(s[j.ServiceNo].Arrive.Select<float, int>(x => Convert.ToInt32(x + TimeGap)));
                    j.PathDepart.AddRange(s[j.ServiceNo].Depart.Select<float, int>(x => Convert.ToInt32(x + TimeGap)));
                }
            }
            else// Train and service do not begin at the same station
            {
                for (int i = InputParameters.TrainPosition[k]; i < Convert.ToInt32(s[ServiceToTake].PassStations[0]) - 1; i++)
                {
                    int Arrive = InputParameters.CurrentTime + InputParameters.TravelTime(InputParameters.TrainPosition[k], i);
                    int Depart = Arrive + InputParameters.DwellingTime;
                    q.TripTour[0].PathRoute.Add(i);
                    q.TripTour[0].PathArrive.Add(Arrive);
                    q.TripTour[0].PathDepart.Add(Depart);
                }
                int TimeGap = InputParameters.TravelTime(InputParameters.TrainPosition[k],
                    Convert.ToInt32(s[ServiceToTake].PassStations[0]) - 1) +
                    InputParameters.CurrentTime - (int)s[ServiceToTake].Arrive[0];
                foreach (Tour j in q.TripTour)
                {
                    j.PathRoute.AddRange(s[j.ServiceNo].PassStations.Select<string, int>(x => (Convert.ToInt32(x) - 1)));
                    j.PathArrive.AddRange(s[j.ServiceNo].Arrive.Select<float, int>(x => Convert.ToInt32(x + TimeGap)));
                    j.PathDepart.AddRange(s[j.ServiceNo].Depart.Select<float, int>(x => Convert.ToInt32(x + TimeGap)));
                }
            }
            return q;
        }

        public path TimetableAdjustment(List<train> s, int k)
        {
            path q = new path();
            return q;
        }
        // Nearest route
        public path RouteAdjustment(List<train> s, int k)
        {
            int index = 0;
            int nearest = 1000;
            path q = new path();
            string vehicleNo = "A";
            // Find a service that is very near to the existing train
            for (int i = 0; i < s.Count; i++)
            {
                if (InputParameters.TrainPosition[k] - Convert.ToInt32(s[i].PassStations[0]) < nearest
                    && InputParameters.TrainPosition[k] - Convert.ToInt32(s[i].PassStations[0]) >= 0)
                {
                    index = i;
                    nearest = InputParameters.TrainPosition[k] - Convert.ToInt32(s[i].PassStations[0]);
                }
            }
            for (int i = index; i < s.Count; i++)
            {
                if (vehicleNo == "A")
                {
                    q.TripTour.Add(new Tour());
                    q.TripTour[0].ServiceNo = index;
                    q.TripTour[0].PathRoute.AddRange(s[index].PassStations.Select<string, int>(x => (Convert.ToInt32(x) - 1)));
                    q.TripTour[0].PathArrive.AddRange(s[index].Arrive.Select<float, int>(x => (Convert.ToInt32(x))));
                    q.TripTour[0].PathDepart.AddRange(s[index].Depart.Select<float, int>(x => (Convert.ToInt32(x))));
                    vehicleNo = s[index].VehicleNo;
                }
                else if (vehicleNo == s[i].VehicleNo)
                {
                    q.TripTour.Add(new Tour());
                    q.TripTour.Last().ServiceNo = i;
                    q.TripTour.Last().PathRoute.AddRange(s[i].PassStations.Select<string, int>(x => (Convert.ToInt32(x) - 1)));
                    q.TripTour.Last().PathArrive.AddRange(s[i].Arrive.Select<float, int>(x => (Convert.ToInt32(x))));
                    q.TripTour.Last().PathDepart.AddRange(s[i].Depart.Select<float, int>(x => (Convert.ToInt32(x))));
                }
            }
            return q;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using ILOG.Concert;
using ILOG.CPLEX;
using System.Runtime.Serialization.Formatters.Binary;

namespace WindowsFormsApplication7.Algorithm
{
    class Method
    {
        public static T DeepCopyByBinary<T>(T obj)
        {
            object retval;
            using (MemoryStream ms = new MemoryStream())
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(ms, obj);
                ms.Seek(0, SeekOrigin.Begin);
                retval = bf.Deserialize(ms);
                ms.Close();
            }
            return (T)retval;
        }

        public static double[] OptimalSolutionUpdate;
        public static List<path> GeneratedPaths = new List<path>(); // Set of all paths
        public static List<List<path>> PathsExistingTrains = new List<List<path>>();

        public static List<EventPair> EventParis = new List<EventPair>();
        public static List<Event> EventSet = new List<Event>();

        public static List<int> OptimalSolution = new List<int>();
        public static List<int> CurrentOptimalSolution = new List<int>(); // Optimal solution in each iteration
        static List<path> PathSet = new List<path>();
        static List<List<int>> PathIndex = new List<List<int>>();


        public bool ReportResult = true;
        public static Event OriginNode = new Event();
        public List<Event> DepotNode = new List<Event>();
        public static List<path> pp = new List<path>();
        public static NetworkForSP Network;
        public static List<SpaceTimeNode> SPNode = new List<SpaceTimeNode>();
        public static List<SpaceTimeLink> SPLink = new List<SpaceTimeLink>();
        public static double[] DualTau = new double[InputParameters.TrainPosition.Count()];
        public static double[] DualPi = new double[EventSet.Count];
        public static double[] DualPhi = new double[EventParis.Count];
        public static double[] DualLamda = new double[InputParameters.DepotNum.Count()];
        //public static double[,] MatrixPhi = new double[EventSet.Count, EventSet.Count];
        public static double[,] MatrixPhi;

        // Methods
        Heuristics ReschedulingStrategies = new Heuristics();
        public int NodeConvert(int k) { return SPNode.Count - (InputParameters.TrainPosition.Count() - k + 1); }

        void ReadInputData()
        {
            OriginNode.ServiceNo = -1;
            OriginNode.ArrivalStation = -1;
            OriginNode.ArrivalTime = 0;
            OriginNode.VehicleNo = "-1";
            List<string> vehicles = new List<string>();
            vehicles.Add(Form1.Trains[0].VehicleNo);
            Console.WriteLine("列车运行方向"+Form1.Trains[0].TrainDirection);
            for (int s = 1; s < Form1.Trains.Count; s++)
                if (vehicles.Exists(t => t == Form1.Trains[s].VehicleNo) == false)
                {vehicles.Add(Form1.Trains[s].VehicleNo); Console.WriteLine("列车运行方向" + Form1.Trains[s].TrainDirection); }
                   
            // Convert the original timetable into a set of events 
            int Index = 0;
            foreach (string vehicle in vehicles)
            {
                Event FormerEvent = null;
                // FormerEvent.ServiceNo = 
                for (int s = 0; s < Form1.Trains.Count; s++)
                {
                    // Events of the original timetable
                    if (Form1.Trains[s].VehicleNo == vehicle)
                    {
                        for (int arr = 0; arr < Form1.Trains[s].PassStations.Count; arr++)
                        {
                            if (FormerEvent == null)
                            {
                                FormerEvent = OriginNode;
                            }
                            Event ev = new Event();
                            ev.FormerEvent.Add(FormerEvent);
                            ev.VehicleNo = (vehicle);
                            ev.ServiceNo = s;
                            if (Form1.Trains[s].TrainDirection == 0)
                            {
                                ev.ArrivalStation = Convert.ToInt32(Form1.Trains[s].PassStations[arr]) - 1;
                            }
                            else
                            {
                                ev.ArrivalStation = Form1.Stations.Count * 2 - Convert.ToInt32(Form1.Trains[s].PassStations[arr]);
                            }                        
                            ev.ArrivalTime = (int)Form1.Trains[s].Arrive[arr];
                            ev.index = Index;
                            EventSet.Add(ev);
                            Index++;
                            FormerEvent = ev;
                        }
                    }
                }
            }

            foreach (Event e in EventSet)
            {
                e.AfterEvent.Add(new Event());
                int findafter = 0;
                foreach (Event ee in EventSet)
                {
                    if (ee.FormerEvent[0] == e && findafter == 0)
                    {
                        e.AfterEvent[0] = ee;
                        findafter = 1;
                    }
                }
                if (findafter == 0)
                {
                    e.AfterEvent[0] = OriginNode;
                }
            }
            // Add feasible adjusted events to the original event set (Link feasible events as former event)
            foreach (Event e in EventSet)
            {
                // (1) Change vehicle no can be realized only at depot
                if (e.ArrivalStation == 2*Form1.Stations.Count - 1 || e.ArrivalStation == Form1.Stations.Count - 1)
                {
                    foreach (Event ee in EventSet)
                    {
                        if (ee.ArrivalStation + e.ArrivalStation == Form1.Stations.Count - 1 && ee.ArrivalTime > e.ArrivalTime && ee.VehicleNo != e.VehicleNo)
                        {
                            e.AfterEvent.Add(ee);
                        }
                    }
                }
            }
            Console.WriteLine("The number of events = " + EventSet.Count());
            foreach (Event e in EventSet) // Find a follower event
            {
                //Console.WriteLine("Event: " + e.index+", arrival station: "+e.ArrivalStation+", arrival time: "+e.ArrivalTime);
                EventPair evpair = new EventPair(); int a = 0;
                Event efollow = new Event(); efollow.ArrivalStation = 100; efollow.ArrivalTime = 0; efollow.ServiceNo = 0;
                for (int ef = 0; ef < EventSet.Count; ef++)
                {
                    if (a == 0)
                    {
                        if (EventSet[ef].ArrivalStation == e.ArrivalStation && e.ArrivalTime < EventSet[ef].ArrivalTime)
                        {
                            efollow = EventSet[ef];
                            a = 1;
                        }
                    }
                    else if (EventSet[ef].ArrivalStation == efollow.ArrivalStation && e.ArrivalTime < EventSet[ef].ArrivalTime && EventSet[ef].ArrivalTime < efollow.ArrivalTime) // if find a nearer event
                    {
                        efollow = EventSet[ef];
                    }
                }
                if (a == 1)
                {
                    evpair.FormerEvent = e;
                    evpair.CurrentEvent = efollow;
                    EventParis.Add(evpair);
                }
            }
            Console.WriteLine("Number of event pairs = " + EventParis.Count());
            for (int i = 0; i < EventParis.Count(); i++)
            {
                EventParis[i].CurrentEvent.EventPairFormer = EventParis[i].FormerEvent;
                EventParis[i].FormerEvent.EventPairFollower = EventParis[i].CurrentEvent;
                //Console.WriteLine("Former event: " + EventParis[i].FormerEvent.ArrivalStation + ", time: " + EventParis[i].FormerEvent.ArrivalTime
                //    + "Current event " + EventParis[i].CurrentEvent.ArrivalTime);
            }
            //MatrixPhi.SetValue;
            MatrixPhi = new double[EventSet.Count, EventSet.Count];
        }

        void SPNetwork()
        {
            // Step 1: Space time node for each event
            foreach (Event e in EventSet)
            {
                if (e.ArrivalTime+InputParameters.MinHeadwayTime>=InputParameters.CurrentTime)
                    // Only consider the nodes that are not too late
                {
                    for (int t = (e.ArrivalTime - InputParameters.SegmentTimeVariation) / InputParameters.TimeUnit;
                        t < (e.ArrivalTime + InputParameters.MinHeadwayTime) / InputParameters.TimeUnit + 1; t++)
                    {
                        SpaceTimeNode sp = new SpaceTimeNode { ArriveTime = t, ArriveStation = e.ArrivalStation, index = e.index };
                        SPNode.Add(sp);
                    }
                }
            }
            Console.WriteLine("Number of SP events = " + SPNode.Count);
            SPNode.RemoveAll((test) => test.ArriveTime < InputParameters.CurrentTime / InputParameters.TimeUnit);
            for (int p = 0; p < SPNode.Count - 1; p++)
            {
                for (int q = p + 1; q < SPNode.Count; q++)
                {
                    if (SPNode[p].ArriveStation == SPNode[q].ArriveStation && SPNode[p].ArriveTime == SPNode[q].ArriveTime)
                    {
                        // Remove same nodes
                        SPNode.RemoveAt(p);
                        continue;
                    }
                }
            }
            // Dummy node for each on-line train
            for (int k = 0; k < InputParameters.TrainPosition.Count(); k++)
            {
                SpaceTimeNode sp = new SpaceTimeNode
                {
                    ArriveTime = InputParameters.CurrentTime,
                    ArriveStation = InputParameters.TrainPosition[k],
                    index = -1
                };
                SPNode.Add(sp);
            }
            // Dummy destination
            SpaceTimeNode dummysp = new SpaceTimeNode
            {
                ArriveTime = (int)InputParameters.MaxConstValue,
                ArriveStation = 2*Form1.Stations.Count+1,
                index = -2
            };
            SPNode.Add(dummysp);
            Console.WriteLine("Number of SP events after elimation = " + SPNode.Count);
            // Step 2: Add links
            // Number of different types of links
            int LinkNormal=0, LinkTurnAround = 0, LinkDummyOrigin=0, LinkDummyDestination=0;
            int index = 0; foreach (SpaceTimeNode sp in SPNode) { sp.NodeIndex = index; index++; }
            foreach (SpaceTimeNode p in SPNode) // Check the feasibility of link from node p to q
            {
                foreach (SpaceTimeNode q in SPNode)
                {
                    if (q.ArriveStation == p.ArriveStation + 1 && p.index >= 0)
                    {
                        if (q.ArriveTime - p.ArriveTime >= (Form1.Sections[p.ArriveStation].RubTime + InputParameters.DwellingTime - InputParameters.SegmentTimeVariation) / InputParameters.TimeUnit
                            && q.ArriveTime - p.ArriveTime <= (Form1.Sections[p.ArriveStation].RubTime + InputParameters.DwellingTime + InputParameters.MinHeadwayTime) / InputParameters.TimeUnit)
                        {
                            SpaceTimeLink link = new SpaceTimeLink() { origin=p,destination=q};
                            p.OutFlowLinks.Add(link); q.InFlowLinks.Add(link);
                            SPLink.Add(link); LinkNormal++;  
                        }
                    }
                    else if (q.ArriveStation == 0 && (p.ArriveStation == 2*Form1.Stations.Count - 1) && q.ArriveTime - p.ArriveTime >= InputParameters.TurnAroundTime / InputParameters.TimeUnit)
                    {
                        SpaceTimeLink link = new SpaceTimeLink() { origin = p, destination = q  };
                        p.OutFlowLinks.Add(link); q.InFlowLinks.Add(link);
                        SPLink.Add(link); LinkNormal++; LinkTurnAround++;
                    }
                }
            }
            // Dummy link from the origin: Assign each origin dummy node to the nearest (earliest time) sp node of each station
            Console.WriteLine("Number of TurnAround links = "+LinkTurnAround);
            Console.WriteLine("Test the dummy node = " + SPNode[NodeConvert(0)].NodeIndex);
            Console.WriteLine("Test the dummy node = " + SPNode[NodeConvert(0)].index);
            Console.WriteLine("Test the dummy node = " + SPNode[NodeConvert(0)].ArriveStation);
            Console.WriteLine("Test the existing sp nodes");
            for (int k = 0; k < InputParameters.TrainPosition.Count(); k++)
            {
                int xx = 0; int time = 10000000;
                foreach (SpaceTimeNode sp in SPNode)
                {
                    if (sp.ArriveStation == SPNode[NodeConvert(k)].ArriveStation && sp.index >= 0)
                    {
                        if (sp.ArriveTime < time)
                        {
                            xx = sp.NodeIndex;
                            time = sp.ArriveTime;
                        }
                    }
                }
                SpaceTimeLink dummylink = new SpaceTimeLink { origin = SPNode[NodeConvert(k)], destination = SPNode[xx] };
                SPNode[NodeConvert(k)].OutFlowLinks.Add(dummylink);
                SPNode[xx].InFlowLinks.Add(dummylink);
                SPLink.Add(dummylink);
                LinkDummyOrigin++;
            }
            // Dummy link to the destination
            foreach (SpaceTimeNode p in SPNode)
            {
                if ((p.index >= 0) && (p.ArriveStation == Form1.Stations.Count - 1 || p.ArriveStation == Form1.Stations.Count*2 - 1))
                {
                    SpaceTimeLink dummylink = new SpaceTimeLink { origin = p, destination = SPNode.Last() };
                    p.OutFlowLinks.Add(dummylink);
                    SPNode.Last().InFlowLinks.Add(dummylink);
                    SPLink.Add(dummylink);
                    LinkDummyDestination++;
                }
            }
            Console.WriteLine("Number of space time links = " + SPLink.Count +"/n; Number of Normal links = "
                +LinkNormal+"; /n Number of links of origin "+LinkDummyOrigin+"; /n Number of links of destination = "
                +LinkDummyDestination);
        }

        void InitialSolution(int k) // Each existing train has only one pass: holding strategy
        {
            PathsExistingTrains[k].Add(ReschedulingStrategies.HoldStrategy(Form1.Trains, k)); // Hold for train k
        }

        void RouteAdjustment(int k)
        {
            path pp = new path();
            pp = Network.GreedyNearestPath(InputParameters.TrainPosition[k]).ptransform();
            PathsExistingTrains[k].Add(pp);
        }

        void ColumnGeneration()
        {
            // Generate one set of paths for each exsiting trains on the line
            foreach (int k in InputParameters.TrainPosition) { PathsExistingTrains.Add(new List<path> { }); } // Inilitiaze;
            for (int k = 0; k < InputParameters.TrainPosition.Count(); k++)
            {
                //  InitialSolution(k);
                RouteAdjustment(k);
            }
            MasterProblem(0);
            // Console.WriteLine("The optimal solution without inserting paths = " + CurrentOptimalSolution);
            int ite = 0;
            while (ite < InputParameters.MaximumIterationNumber)
            {
                // Solve the master problem
                foreach (SpaceTimeNode spnode in SPNode) { spnode.UpdateNodeCost(); }
                foreach (SpaceTimeLink splink in SPLink) { splink.UpdateLinkCost(); }
                //for (int k = 0; k < InputParameters.TrainPosition.Count(); k++)
                int ReducedCostIndicator = 0;
                for (int k = 0; k < InputParameters.TrainPosition.Count(); k++)
                {
                    Console.WriteLine("Train number " + k);
                    path ppp = new path();
                    ppp = Network.ShortestPath(k, SPNode[NodeConvert(k)], SPNode.Last()).ptransform();
                    //Console.WriteLine("Path is ");
                    //foreach (Event e in ppp.PathEvent) { Console.WriteLine("Station " + e.ArrivalStation + ", Time " + e.ArrivalTime); }
                    if (ppp.ReducedPathCost < 0)
                    {
                        PathsExistingTrains[k].Add(ppp);
                        ReducedCostIndicator = 1;
                    }
                }
                if (ReducedCostIndicator == 1)
                {
                    MasterProblem(ite);
                }
                else
                {
                    ite = InputParameters.MaximumIterationNumber + 1;
                    Console.WriteLine("No more path with reduced cost can be added any more!!");
                }
                ite++;
            }
            //MasterProblem();
        }

        void MasterProblem(int ite)
        {
            int index = 0;
            //PathIndex
            PathIndex = new List<List<int>>();
            PathSet = new List<path>();
            for (int k = 0; k < InputParameters.TrainPosition.Count(); k++)
            {
                PathIndex.Add(new List<int>());
                PathSet.AddRange(PathsExistingTrains[k]);
                foreach (path p in PathsExistingTrains[k]) { PathIndex[k].Add(index); index++; }
            }
            Console.WriteLine("Path size = " + PathSet.Count());
            Console.WriteLine("Path scale =  " + index);
            PathIndex.Add(new List<int>());
            PathSet.AddRange(GeneratedPaths);
            foreach (path p in GeneratedPaths) { PathIndex[InputParameters.TrainPosition.Count()].Add(index); index++; }
            Console.WriteLine("Path from the depot = " + GeneratedPaths.Count);
            Cplex cplex = new Cplex();
            // double[] DualPi = new double[]
            INumVar[] xp;
            Console.WriteLine("Solve the master problem");
            xp = cplex.NumVarArray(index, 0, 1, NumVarType.Float);
            IRange[] cons = new IRange[InputParameters.TrainPosition.Count()];
            IRange[] cons2 = new IRange[EventSet.Count()];
            IRange[] cons3 = new IRange[EventParis.Count];
            IRange[] cons4 = new IRange[InputParameters.DepotNum.Count()];

            // Train cover
            for (int k = 0; k < InputParameters.TrainPosition.Count(); k++)
            {
                ILinearNumExpr cons1 = cplex.LinearNumExpr();
                INumExpr expr = cplex.NumExpr();
                for (int i = 0; i < PathsExistingTrains[k].Count; i++)
                {
                    expr = cplex.Sum(expr, xp[PathIndex[k][i]]);
                }
                cons[k] = cplex.AddEq(expr, 1);
            }
            // Set cover
            for (int e = 0; e < EventSet.Count(); e++)
            {
                INumExpr expr = cplex.NumExpr();
                for (int p = 0; p < index; p++)
                {
                    expr = cplex.Sum(expr, cplex.Prod(PathSet[p].PathEventConject(EventSet[e]), xp[p]));
                }
                cons2[e] = cplex.AddLe(expr, 1);
            }
            // Headway
            for (int e = 0; e < EventParis.Count; e++)
            {
                double barte = InputParameters.MinHeadwayTime;
                INumExpr expr = cplex.NumExpr();
                for (int p = 0; p < index; p++)
                {
                    expr = cplex.Sum(expr, cplex.Prod(PathSet[p].PathEventTime(EventParis[e].CurrentEvent)
                        - PathSet[p].PathEventTime(EventParis[e].FormerEvent)
                        - PathSet[p].PathEventConject(EventParis[e].CurrentEvent) * (PathSet[p].PathEventTime(EventParis[e].FormerEvent) + InputParameters.MinHeadwayTime)
                        , xp[p]));
                    barte += -(PathSet[p].PathEventTime(EventParis[e].FormerEvent) + InputParameters.MinHeadwayTime);
                }
                cons3[e] = cplex.AddGe(expr, barte);
            }

            // Depot
            for (int d = 0; d < InputParameters.DepotNum.Count(); d++)
            {
                INumExpr expr = cplex.NumExpr();
                for (int p = 0; p < index; p++)
                {
                    expr = cplex.Sum(expr, cplex.Prod(PathSet[p].EndDepot(InputParameters.DepotPosition[d]), xp[p]));
                }
                //cons4[d] = cplex.AddEq(expr, InputParameters.DepotNum[d]);
            }
            ILinearNumExpr obj = cplex.LinearNumExpr();
            for (int p = 0; p < index; p++)
            {
                obj.AddTerm(PathSet[p].PathCost(EventSet), xp[p]);
            }

            cplex.AddMinimize(obj);
            cplex.Solve();
            Console.WriteLine("Solution status = " + cplex.GetStatus()); //求解状态    
            int objective = (int)cplex.ObjValue;
            CurrentOptimalSolution.Add(objective);
            Console.WriteLine(" Minobjective = " + (int)cplex.ObjValue);  //目标最优值 
            for (int p = 0; p < index; p++) { Console.Write(cplex.GetValue(xp[p]) + ", "); } // 决策变量
            DualTau = cplex.GetDuals(cons);
            DualPi = cplex.GetDuals(cons2);
            DualPhi = cplex.GetDuals(cons3);
            // DualLamda = cplex.GetDuals(cons4);
            //MatrixPhi
            if (DualPhi!=null)
            {
            for (int ep = 0; ep < DualPhi.Count(); ep++)
            {
                // Update matrixphi
                MatrixPhi[EventParis[ep].FormerEvent.index, EventParis[ep].CurrentEvent.index] = DualPhi[ep];
            }
            }

            // Test the results here
            //Console.WriteLine("Test the results here ");
            //for (int i = 0; i < EventSet.Count; i++)
            //{
            //    for (int j = 0; j < EventSet.Count; j++)
            //    {
            //        Console.Write(MatrixPhi[i, j]);
            //    }
            //    Console.Write("\n");
            //}
            int objectivevalue2 = 0;
            //foreach (Event e in EventSet)
            //{
            //    objectivevalue2 += (InputParameters.CancelPenalty - e.ArrivalTime * InputParameters.DelayPenalty);
            //}

            // Dual Variables
            Console.WriteLine("%%% --- The Dual Variables --- %%%");
            Console.WriteLine("Tau:  "); for (int i = 0; i < DualTau.Count(); i++) { Console.Write(DualTau[i] + ", "); }
            Console.Write("\n");
            Console.WriteLine("Pi:  "); for (int i = 0; i < DualPi.Count(); i++) { Console.Write(DualPi[i] + ", "); }
            Console.Write("\n");
            if (DualPhi != null)
            {Console.WriteLine("Phi:  "); for (int i = 0; i < DualPhi.Count(); i++) { Console.Write(DualPhi[i] + ", "); }
            Console.Write("\n"); }
            //Console.WriteLine("Lamda:  "); for (int i = 0; i < DualLamda.Count(); i++) { Console.Write(DualLamda[i] + ", "); }
            Console.Write("\n");
            OptimalSolutionUpdate = new double[PathSet.Count()];
            OptimalSolutionUpdate = cplex.GetValues(xp);
            for (int i = 0; i < index; i++)
            {
                OptimalSolution.Add((int)cplex.GetValue(xp[i]));
            }
            //ShowResult();
        }

        void ShowResult()
        {
            FileStream tfilestream = new FileStream("Output data\\输出时刻表.csv", FileMode.Create, FileAccess.Write);
            StreamWriter rs = new StreamWriter(tfilestream, System.Text.Encoding.UTF8);
            //rs = new StreamWriter(tfilestream, Encoding.Default);
            string data = "";
            data += "列车号"; data += ","; data += "服务号"; data += ","; data += "车站"; data += ","; data += "到达"; data += ","; data += "出发";
            rs.WriteLine(data);
            int vehicleNo = 100;
            for (int p = 0; p < PathSet.Count; p++)
            {
                for (int k = 0; k < PathIndex.Count; k++)
                {
                    if (PathIndex[k].Contains(p))
                        vehicleNo = k + 1;
                }
                if (OptimalSolutionUpdate[p] != 0)
                {
                    for (int i = 0; i < PathSet[p].TripTour.Count; i++)
                    {
                        for (int j = 0; j < PathSet[p].TripTour[i].PathArrive.Count; j++)
                        {
                            data = "";
                            data += vehicleNo.ToString();
                            data += ",";
                            data += PathSet[p].TripTour[i].ServiceNo;
                            data += ",";
                            data += (PathSet[p].TripTour[i].PathRoute[j] + 1);
                            data += ",";
                            data += PathSet[p].TripTour[i].PathArrive[j];
                            data += ",";
                            data += PathSet[p].TripTour[i].PathDepart[j];
                            rs.WriteLine(data);
                        }
                    }
                }
            }
            rs.Close();
            tfilestream.Close();

            tfilestream = new FileStream("Output data\\时空网络节点.csv", FileMode.Create, FileAccess.Write);
            rs = new StreamWriter(tfilestream, System.Text.Encoding.UTF8);
            //rs = new StreamWriter(tfilestream, Encoding.Default);
            data = "";
            data += "节点编号"; data += ","; data += "车站"; data += ","; data += "时间"; data += ","; data += "Cost";
            rs.WriteLine(data);
            foreach (SpaceTimeNode sp in SPNode)
            {
                data = "";
                data += sp.NodeCost;
                data += ",";
                data += sp.ArriveStation;
                data += ",";
                data += sp.ArriveTime;
                data += ",";
                data += sp.NodeCost;
                // data += ",";
                rs.WriteLine(data);
            }
            rs.Close();
            tfilestream.Close();

            tfilestream = new FileStream("Output data\\时空网络路径.csv", FileMode.Create, FileAccess.Write);
            rs = new StreamWriter(tfilestream, System.Text.Encoding.UTF8);
            //rs = new StreamWriter(tfilestream, Encoding.Default);
            data = "";
            data += "路径编号"; data += ","; data += "起点"; data += ","; data += "终点"; data += ","; data += "Cost";
            rs.WriteLine(data);
            int q = 0;
            foreach (SpaceTimeLink sp in SPLink)
            {
                data = "";
                data += q;
                data += ",";
                data += sp.origin.NodeIndex;
                data += ",";
                data += sp.destination.NodeIndex;
                data += ",";
                data += sp.LinkCost;
                q++;
                rs.WriteLine(data);
            }
            rs.Close();
            tfilestream.Close();

            if (ReportResult == false)
            {
                // Show the information of paths
                Console.WriteLine("The total number of paths");
                Console.WriteLine("Paths from the depot = " + GeneratedPaths.Count);
                for (int i = 0; i < PathsExistingTrains.Count; i++)
                {
                    Console.WriteLine("Paths from the existing train " + i + " =" + PathsExistingTrains[i].Count);
                }
                // Details of each paths
                int k = 0;
                Console.WriteLine("%%%%%%%Detailed information of paths from existing n");
                for (int i = 0; i < PathsExistingTrains[k].Count; i++)
                {
                    Console.WriteLine("Number of tours here: " + PathsExistingTrains[k][i].TripTour.Count);
                }
                for (int j = 0; j < PathsExistingTrains[k][0].TripTour.Count; j++)
                {
                    Console.WriteLine("This is service " + PathsExistingTrains[k][0].TripTour[j].ServiceNo);
                    Console.Write("Path Route : ");
                    for (int i = 0; i < PathsExistingTrains[k][0].TripTour[j].PathRoute.Count; i++)
                    {
                        Console.Write(PathsExistingTrains[k][0].TripTour[j].PathRoute[i] + ", ");
                    }
                    Console.Write("\n");
                    Console.Write("Path Arrive : ");
                    for (int i = 0; i < PathsExistingTrains[k][0].TripTour[j].PathRoute.Count; i++)
                    {
                        Console.Write(PathsExistingTrains[k][0].TripTour[j].PathArrive[i] + ", ");
                    }
                    Console.Write("\n");
                    Console.Write("Path Depart : ");
                    for (int i = 0; i < PathsExistingTrains[k][0].TripTour[j].PathRoute.Count; i++)
                    {
                        Console.Write(PathsExistingTrains[k][0].TripTour[j].PathDepart[i] + ", ");
                    }
                    Console.Write("\n");
                }
            }
        }

        void TestOriginTimetable()
        {
            Console.WriteLine("Planned Train Schedule");
            for (int i = 0; i < Form1.Trains.Count; i++)
            {
                Console.Write("Service " + i + "; Taken by Train " + Form1.Trains[i].VehicleNo + "\n");
                Console.WriteLine("Pass stations and arrival time");
                for (int j = 0; j < Form1.Trains[i].PassStations.Count; j++)
                {
                    Console.Write(Form1.Trains[i].PassStations[j] + " ");
                }
                Console.Write("\n");
                for (int j = 0; j < Form1.Trains[i].PassStations.Count; j++)
                {
                    Console.Write(Form1.Trains[i].Arrive[j] + " ");
                }
                Console.Write("\n");
            }
        }

        public void NewMain(EventArgs Ebegin)
        {
            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            sw.Start(); 
            //Form1.Sections[0].
            // Read data: Link travel time, stations, services
            Console.WriteLine("算法开始...");
            ReadInputData();
            sw.Stop();
            TimeSpan ts = sw.Elapsed;
            Console.WriteLine("数据读取结束，用时" + ts.TotalMilliseconds + "ms, 开始构建网络...");
            sw.Restart();
            //sw.Start();
            SPNetwork();
            Network = new NetworkForSP();
            sw.Stop();
            ts = sw.Elapsed;
            Console.WriteLine("网络构建完成，用时" + ts.TotalMilliseconds + "ms, 开始计算...");
            sw.Restart();
            ColumnGeneration();
            sw.Stop();
            ts = sw.Elapsed;
            Console.WriteLine("计算完成，用时" + ts.TotalMilliseconds + "ms, 开始格式转换...");
            Console.WriteLine("输出调整后运行图");
            int NumberOfRescheduledPath = 0;
            int vehicleNo = 100;
            for (int p = 0; p < PathSet.Count; p++)
            {
                for (int k = 0; k < PathIndex.Count; k++)
                {
                    if (PathIndex[k].Contains(p))
                        vehicleNo = k + 1;
                }
                if (OptimalSolutionUpdate[p] != 0)
                {
                    NumberOfRescheduledPath++;
                    //foreach (Event e in PathSet[p].PathEvent) { Console.WriteLine("Path "+p+": Station "+ e.ArrivalStation+", Time "+e.ArrivalTime); }
                    
                    Event FirstE = PathSet[p].PathEvent.First();
                    while (true)
                    {
                        Console.WriteLine("Path " + p + ": Station " + FirstE.ArrivalStation + ", Time " + FirstE.ArrivalTime);
                        FirstE = FirstE.AfterEvent[0];
                        if (FirstE == PathSet[p].PathEvent.Last()) { break; }
                    }
                    for (int i = 0; i < PathSet[p].TripTour.Count; i++)
                    {
                        train tt = new train();
                        tt.VehicleNo = PathSet[p].PathEvent[0].VehicleNo;
                        tt.TrainNO = PathSet[p].TripTour[i].ServiceNo.ToString();
                        for (int j = 0; j < PathSet[p].TripTour[i].PathArrive.Count; j++)
                        {
                            if (PathSet[p].TripTour[i].PathRoute[j] < Form1.Stations.Count)
                            {
                                tt.PassStations.Add((PathSet[p].TripTour[i].PathRoute[j] + 1).ToString());
                            }
                            else
                            {
                                tt.PassStations.Add((Form1.Stations.Count*2 - PathSet[p].TripTour[i].PathRoute[j]).ToString());
                            }
                            tt.Arrive.Add(PathSet[p].TripTour[i].PathArrive[j]);
                            tt.Depart.Add(PathSet[p].TripTour[i].PathDepart[j]);
                        }
                        Form1.RescheduledTrains.Add(tt);
                    }
                }
            }
            // Form1.RescheduledTrains
            Console.WriteLine("转换完成，开始输出...");
            Console.WriteLine("优化后，一共"+NumberOfRescheduledPath+"条调整路径， 一共"
                + Form1.RescheduledTrains.Count+"个车次");
            // Form1.Trains

            #region Test the result
            // Two classes, Form1.Trains, Form1.ResceduledTrains
            // Planned timetable
            Console.WriteLine("计划列车运行到发时刻");
            foreach (train t in Form1.Trains)
            {
                Console.WriteLine("列车经过车站： ");
                for (int i = 0; i < t.PassStations.Count; i++)
                {
                    Console.Write(t.PassStations[i] + ", ");
                }
                Console.WriteLine("列车到达时刻： ");
                for (int i = 0; i < t.PassStations.Count; i++)
                {
                    Console.Write(t.Arrive[i] + ", ");
                }
            }
            Console.WriteLine("实际列车运行到发时刻");
            foreach (train t in Form1.RescheduledTrains)
            {
                Console.WriteLine("列车经过车站： ");
                for (int i = 0; i < t.PassStations.Count; i++)
                {
                    Console.Write(t.PassStations[i]+", ");
                }
                Console.WriteLine("列车到达时刻： ");
                for (int i = 0; i < t.PassStations.Count; i++)
                {
                    Console.Write(t.Arrive[i] + ", ");
                }
            }
            Console.WriteLine("区间运行时间");
            for (int i = 0; i < Form1.Sections.Count; i++)
            {
                Console.Write(Form1.Sections[i].RubTime+", ");
            }
            #endregion

        }
    }
}

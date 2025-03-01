using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication7.Algorithm
{
    class Network
    {
    }
    [Serializable]
    public class Event
    {
        public int ServiceNo;
        public int ArrivalStation;
        public int ArrivalTime;
        public string VehicleNo;
        public double EventCost;
        public List<Event> FormerEvent;
        public List<Event> AfterEvent;
        public double EventQ;
        public double EventXi;
        public Event EventPairFormer;
        public Event EventPairFollower;
        public double TimeWindowLeft;
        public double TimeWindowRight;
        public int index;
        public bool IfSelected;
        public Event()
        {
            VehicleNo = "NULL";
            ServiceNo = new int();
            ArrivalStation = new int();
            ArrivalTime = new int();
            EventCost = new double();
            EventQ = new double();
            EventXi = new double();
            index = new int();
            TimeWindowLeft = 0;
            TimeWindowRight = ArrivalTime + InputParameters.MinHeadwayTime;
            FormerEvent = new List<Event>();
            AfterEvent = new List<Event>();
            IfSelected = false;
        }

        void UpdateValue(double phi, double phif, double pi, double q)
        {
            EventQ = 1 - phi - phif;
            EventXi = InputParameters.MinHeadwayTime * phi - pi - q;
        }
    }

    class EventPair
    {
        public Event FormerEvent;
        public Event CurrentEvent;
        public EventPair()
        {
            FormerEvent = new Event();
            CurrentEvent = new Event();
        }
    }
    // Space time network with "small arcs"
    class SpaceTimeNode
    {
        public int ArriveStation;
        public int ArriveTime;
        public int index;
        public double NodeCost;
        public int NodeIndex = 0;
        public List<SpaceTimeLink> InFlowLinks;
        public List<SpaceTimeLink> OutFlowLinks;
        public SpaceTimeNode()
        {
            ArriveStation = 0;
            ArriveTime = 0;
            index = 0;
            NodeIndex = 0;
            InFlowLinks = new List<SpaceTimeLink>();
            OutFlowLinks = new List<SpaceTimeLink>();
            NodeCost = 0;
        }

        public void UpdateNodeCost()
        {
            double Qe = 0;
            double Xie = 0;
            double Phiee = 0; // e and its former event
            double Phiefe = 0; // e and its follower event

            //Xie = InputParameters.MinHeadwayTime * Program.DualPhi[0] - Program.DualPi[0] - Qe;
            if (index >= 0) // Node cost for non-dummy nodes
            {
                if (Method.EventSet[index].EventPairFormer != null)
                {
                    Phiee = Method.MatrixPhi[index, Method.EventSet[index].EventPairFormer.index];
                }
                else
                {
                    Phiee = 0;
                }
                if (Method.EventSet[index].EventPairFollower != null)
                {
                    Phiefe = Method.MatrixPhi[Method.EventSet[index].EventPairFollower.index, index];
                }
                else
                {
                    Phiefe = 0;
                }

                Qe = (InputParameters.TimeUnit * ArriveTime - InputParameters.MinHeadwayTime) * Phiee - InputParameters.TimeUnit * ArriveTime * Phiefe;
                // Xie = InputParameters.MinHeadwayTime * Phiee - Program.DualPi[index] - InputParameters.CancelPenalty;
                NodeCost = InputParameters.TimeUnit * ArriveTime - Method.DualPi[index] - InputParameters.CancelPenalty - Qe;
                //NodeCost = -1000 + ArriveTime * InputParameters.TimeUnit - (Program.DualPi[index] + InputParameters.CancelPenalty);
            }
            else // Node cost for dummy origin and destination nodes
            {
                NodeCost = 0;
            }
            //NodeCost = Qe * ArriveTime * InputParameters.TimeUnit + Xie;
        }
    }
    class SpaceTimeLink
    {
        public SpaceTimeNode origin;
        public SpaceTimeNode destination;
        public int LinkIndex; // map a link to an event
        public double LinkCost;
        public int LinkTravelTime;
        public SpaceTimeLink()
        {
            LinkCost = 0;
            LinkTravelTime = 0;
            origin = new SpaceTimeNode();
            destination = new SpaceTimeNode();
        }
        public void UpdateLinkCost()
        {
            if (destination.index == -2)
            {
                for (int i = 0; i < InputParameters.DepotPosition.Count(); i++)
                {
                    if (origin.ArriveStation == InputParameters.DepotPosition[i])
                    {
                        //LinkCost = -Method.DualLamda[i];
                    }
                }
            }
            else
            {
                LinkCost = destination.NodeCost;
            }
        }
    }

    class NetworkForSP
    {
        public NetworkForSP() { }
        int mListFront;
        int mListTail;
        int[] mSENodeList = new int[Method.SPNode.Count];
        double[] NodeLabelCost = new double[Method.SPNode.Count];
        int[] NodeStatus = new int[Method.SPNode.Count];
        int[] NodePredecessor = new int[Method.SPNode.Count];
        void SEListClear()
        {
            mListFront = -1;
            mListTail = -1;
        }
        bool SEListEmpty()
        {
            return (mListFront == -1);
        }
        int SEListFront()
        {
            return mListFront;
        }
        void SEListPushBack(int node)
        {
            if (mListFront == -1) // Start from empty
            {
                mListFront = node;
                mListTail = node;
                mSENodeList[node] = -1;
            }
            else
            {
                mSENodeList[mListTail] = node;
                mSENodeList[node] = -1;
                mListTail = node;
            }
        }
        void SEListPopFront()
        {
            int tempFront = mListFront;
            mListFront = mSENodeList[mListFront];
            mSENodeList[tempFront] = -1;
        }
        // Use shortest path to find
        public path ShortestPath(int k, SpaceTimeNode origin, SpaceTimeNode destination)
        {
            path q = new path();
            //Console.WriteLine("The size of node label cost is "+ Program.SPNode.Count());
            //Console.WriteLine("The size of node label cost is " + NodeLabelCost.Count());
            for (int i = 0; i < Method.SPNode.Count; i++)
            {
                //NodeStatus
                NodeStatus[i] = 0;
                //NodeLabelCost.
                NodeLabelCost[i] = InputParameters.MaxConstValue;
                NodePredecessor[i] = -1;
            }
            NodeLabelCost[origin.NodeIndex] = 0; // Set the value of first node
            SEListClear();
            SEListPushBack(origin.NodeIndex);
            while (!SEListEmpty())
            {
                int FromNode = SEListFront();
                SEListPopFront();
                foreach (SpaceTimeLink Splink in Method.SPNode[FromNode].OutFlowLinks)
                {
                    double NewToNodeCost = NodeLabelCost[FromNode] + Splink.LinkCost;
                    if (NewToNodeCost < NodeLabelCost[Splink.destination.NodeIndex])
                    {
                        // Update label cost and node/time predecessor
                        NodeLabelCost[Splink.destination.NodeIndex] = NewToNodeCost;
                        NodePredecessor[Splink.destination.NodeIndex] = Splink.origin.NodeIndex;
                        if (Splink.destination != origin)
                        {
                            if (NodeStatus[Splink.destination.NodeIndex] == 0)
                            {
                                SEListPushBack(Splink.destination.NodeIndex);
                                NodeStatus[Splink.destination.NodeIndex] = 1;
                            }
                        }
                    }
                }
            }
            q.ReducedPathCost = NodeLabelCost[destination.NodeIndex] - Method.DualTau[k];
            //q = GreedyNearestPath(3);
            //q.TestPATHCCC = (int) NodeLabelCost[Program.SPNode.Last().NodeIndex];
            // Transfer a space time path to a series of events
            // Test a given path
            int xx = Method.SPNode.Last().NodeIndex;
            if (NodePredecessor[xx] == -1)
            {
                Console.WriteLine("There is no feasible path!!!");
            }
            //q.PathEvent.Add(new Event());
            int InitialNode = 0;
            while (NodePredecessor[xx] != -1)
            {
                xx = NodePredecessor[xx];
                if (Method.SPNode[xx].index >= 0) // xx is not the first sp node
                {

                    Event ColoneEvent = new Event();
                    //ColoneEvent = Method.DeepCopyByBinary(Method.EventSet[Method.SPNode[xx].index]);
                    //ColoneEvent.ArrivalTime = 10 * Method.SPNode[xx].ArriveTime;
                    ColoneEvent = CopyNewEvent(Method.EventSet[Method.SPNode[xx].index]);
                    if (InitialNode == 0) // The last event
                    {
                        InitialNode = 1;
                        ColoneEvent.AfterEvent[0] = Method.OriginNode;
                    }
                    else
                    {
                        //q.PathEvent.Last().AfterEvent[0] = ColoneEvent;
                        ColoneEvent.AfterEvent[0] = q.PathEvent.Last();
                    }
                    q.PathEvent.Add(ColoneEvent);
                }
            }
            //Console.WriteLine("Shortest path begins; PathEventCount = "+q.PathEvent.Count());
            Console.WriteLine("Train " + k + " :");
            Console.WriteLine("Event num = " + q.PathEvent.Count);
            Console.WriteLine("Start station =" + origin.ArriveStation);
            Console.WriteLine("Start time = " + origin.ArriveTime);
            Console.WriteLine("NodeLabelCost = " + NodeLabelCost[destination.NodeIndex]);
            if (k == 1 && q.PathEvent.Count == 2)
            {
                Console.WriteLine("Event index = " + q.PathEvent[0].index);
                Console.WriteLine("Event index = " + q.PathEvent[1].index);
            }
            Console.WriteLine("Reduced cost = " + q.ReducedPathCost);
            //foreach (Event e in q.PathEvent)
            //{
            //    Console.WriteLine("Arrive at station " + e.ArrivalStation + "; Arrive time " + e.ArrivalTime);
            //}
            q.PathEvent.Reverse();
            return q;
        }
        public int cmin(int a)
        {
            if (a > 1) return 1;
            else return 0;
        }
        // Use greedy algorithm to generate a path
        public path GreedyNearestPath(int k) // k 表示path的起始车站
        {
            path q = new path();
            // Head
            int nearest = 100000000;
            q.PathEvent.Add(new Event());
            Event StartEvent = new Event();
            // Using a greedy to find a nearest beginning node


            foreach (Event e in Method.EventSet)
            {
                if (e.IfSelected == false)
                {
                    if (e.ArrivalStation == k)
                    {
                        if (e.ArrivalTime >= InputParameters.CurrentTime && e.ArrivalTime - InputParameters.CurrentTime < nearest)
                        {
                            StartEvent = e;
                            nearest = e.ArrivalTime - InputParameters.CurrentTime;
                        }
                    }
                }
            }

            if (StartEvent.ArrivalTime == 0)
            {
                Console.WriteLine("Output error!");
            }

            // Get the path from the nearest beginning node
            q.PathEvent[0] = StartEvent;
            int xindex = cmin(StartEvent.AfterEvent.Count);
            while (StartEvent.AfterEvent[xindex].ArrivalStation >= 0 )
            {
                q.PathEvent.Add(StartEvent.AfterEvent[xindex]);
                StartEvent = StartEvent.AfterEvent[xindex];
                if (StartEvent.AfterEvent.Count == 0) break;
                xindex = cmin(StartEvent.AfterEvent.Count);
                
            }
            Event xx = q.PathEvent.Last();
            while (true)
            {
                xx.IfSelected = true;
                if (xx.FormerEvent.Count == 0) break;
                else xx = xx.FormerEvent[0];
            }
            return q;
        }
        // Test the path generation method
        public path TestPathGenerate(int k)
        {
            path q = new path();
            // Head
            int nearest = 1000;
            q.PathEvent.Add(new Event());
            Event StartEvent = new Event();
            // Using a greedy to find a nearest beginning node
            foreach (Event e in Method.EventSet)
            {
                if (e.ArrivalStation == k)
                {
                    if (e.ArrivalTime >= InputParameters.CurrentTime && e.ArrivalTime - InputParameters.CurrentTime < nearest)
                    {
                        StartEvent = e;
                        nearest = e.ArrivalTime - InputParameters.CurrentTime;
                    }
                }
            }
            // Get the path from the nearest beginning node
            q.PathEvent[0] = StartEvent;

            return q;
        }

        public Event CopyNewEvent(Event input)
        {
            Event XX = new Event();
            XX.ArrivalStation = input.ArrivalStation;
            XX.ArrivalTime = input.ArrivalTime;
            XX.EventCost = input.EventCost;
            XX.EventQ = input.EventQ;
            XX.EventXi = input.EventXi;
            XX.EventPairFollower = input.EventPairFollower;
            XX.EventPairFormer = input.EventPairFormer;
            XX.AfterEvent = input.AfterEvent;
            XX.FormerEvent = input.FormerEvent;
            XX.AfterEvent = input.AfterEvent;
            XX.index = input.index;
            XX.ServiceNo = input.ServiceNo;
            XX.VehicleNo = input.VehicleNo;
            XX.TimeWindowLeft = input.TimeWindowLeft;
            XX.TimeWindowRight = input.TimeWindowRight;
            return XX;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication7.Algorithm
{
    class BCTree
    {
        //public int Key;
        public BCTNode Root;
        public List<BCTNode> Tree;
        public double CurrentBound;
        public int[] CurrentBestSolution;
        public double CurrentBestValue;
        public static int[] ConvertDoubleArrayToIntArray(double[] adDoubleArray)
        {
            return adDoubleArray.Select(d => (int)d).ToArray();
        }
        // DFS
        public void Branch(BCTNode root)
        {
            BCTNode CurrentNode = Root;
            // (1) Branch and find
            while (!CurrentNode.NodeFractionJudge())
            {
                //Branch(CurrentNode);
                //CurrentNode = CurrentNode.AddLeftNode();
                AddIntNode(CurrentNode);
                CurrentNode = Tree.Last();
            }
            CurrentBestSolution = ConvertDoubleArrayToIntArray(Tree.Last().FractionalSolution);
        }
        public BCTree()
        {
            Tree = new List<BCTNode>();
            Root = new BCTNode(Method.OptimalSolutionUpdate, Method.OptimalLowerBound);
            Tree.Add(Root);
            CurrentBestValue = 10000000;
            CurrentBound = Root.LPBound;
            Branch(Root);
        }
        void AddIntNode(BCTNode Cnode)
        {
            //int i = 0;
            for(int k=0;k<Method.PathsExistingTrains.Count;k++)
            {
                int index=-1;
                double cmax =-10;
                for(int i=0;i<Method.PathsExistingTrains[k].Count;i++)
                {
                    if (Cnode.FractionalSolution[Method.PathIndex[k][i]] != 0
                        && Cnode.FractionalSolution[Method.PathIndex[k][i]] != 1)
                    {
                        if(Cnode.FractionalSolution[Method.PathIndex[k][i]]>cmax)
                        {
                            cmax = Cnode.FractionalSolution[Method.PathIndex[k][i]];
                            index = i;
                        }
                    }
                }
                if (cmax != -10)
                {
                    BCTNode NewNode = new BCTNode(Cnode.FractionalSolution, Cnode.LPBound);
                    for (int i = 0; i < Method.PathsExistingTrains[k].Count; i++)
                    {
                        NewNode.FractionalSolution[Method.PathIndex[k][i]] = 0;
                    }
                    NewNode.FractionalSolution[Method.PathIndex[k][index+1]] = 1;
                    Tree.Add(NewNode);
                    break;
                }
            }
        }
    }
    class BCTNode
    {
        public int Key;
        public double [] FractionalSolution;
        BCTNode LeftChildren;
        BCTNode RightChildren;
        BCTNode Parent;
        public double UPBound;
        public double LPBound;

        public BCTNode(double[] FS, double B)
        {
            FractionalSolution = FS;
            LPBound = B;
        }

        /// <summary>
        /// Public methods
        /// </summary>
        /// <returns></returns>

        public bool NodeFractionJudge()
        {
            foreach(double x in FractionalSolution) if (x != 1 && x!=0) return false;
            return true;
        }
    }
}

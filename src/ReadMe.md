# Real-Time Rolling Stock and Timetable Rescheduling in Urban Rail Transit Systems

The software and data in this repository are a snapshot of the software and data
that were used in the research reported on in the paper "Real-Time Rolling Stock and Timetable Rescheduling in Urban Rail Transit Systems" by J. Yin, L. Yang, Z.Liang, et al.
## Description

This folder contains the source codes that are embedded into a software platform (based on .NET) for the simulation and optimization of trains in Beijing metro. Next, we briefly introduce the contents of the project, the main functions, key algrithms and the way of implementations. 

## Code structure 
Overall, the project involves three types of codes: 

* A software platform, involving the files Program.cs, Parameters.cs, Fangzhen.Designer.cs, etc, to load the input data, transform the data, visualize the train timetable, and output the results. In particular, here we define several classes (Line.cs, section.cs, station.cs, train.cs) as data structures and some other classes (Display.cs, etc) to display and adjust the train timetable in the software.
* Some key functions and algorithms to reschedule the train timetable. Basically, these functions are detaild in folder [Algorithm](src/WindowsFormsApplication7/Algorithm). In this folder: InputParameters.cs specifies the input parameters (after loading the input instance file as given in [Input](Input_data)); Network.cs constructs the event-based network; path.cs gives the paths of services; Heuristics.cs designs the H-method as specified in our paper; BCTree.cs helps to construct the BC algorithm; and Method.cs presents the most important componements (involving an event based formulation and a path based formulation), as well as the output of solutions and performance indicators.
* A train simulation core, to test the performance of rescheduled train timetables. 

## Main functions and algorithms
The key algorithms to reschedule trains are located at [Algorithm](src/WindowsFormsApplication7/Algorithm). Here, we introduce the functions of each file:

* [Parameters](src/WindowsFormsApplication7/Algorithm/InputParameters.cs) This file defines some basic parameters, either from the input file (planned travel time between any two stations) or static parameters (TurnAroundTime, Minimal train headway, Dwelling time, Depot position, Penalty of train calcelations, SegmentTimeVariant, etc)
* [Network](src/WindowsFormsApplication7/Algorithm/Network.cs) This file constructs the class of event-activity network, as defined in the paper, involving the definition of event, event pair, space-time event/event pair (where the latter networks are used in the label correcting process). In addition, a shortest-path algorithm with label correcting (public path ShortestPath(int k, SpaceTimeNode origin, SpaceTimeNode destination)) is designed as a function to generate shortesting paths in the column generation; a greedy-baesd path finding algoithm (public path GreedyNearestPath(int k)) is designed as a function to generate a greedy path in the H-method (see the paper for more details).
* [Path](src/WindowsFormsApplication7/Algorithm/path.cs) This file is mainly used to define the paths in the column generation process, to update the dual prices on each event in the generated paths.
* [Heuristics](src/WindowsFormsApplication7/Algorithm/Heuristics.cs) This file specifies a practical experienced based heuristic, i.e., H-method, as given in our paper, as a benchmark.
* [BCTree](src/WindowsFormsApplication7/Algorithm/BCTree.cs) defines a branch and price tree for the designed algorithms.
* [Method](src/WindowsFormsApplication7/Algorithm/Method) The most important file in the project, which calls Network.cs to construct an event-activity network, and gives two alternative formulations for the investigated RSTO problem, i.e., ERST and PRST. From Line 711 to 1088, the function EventBasedFormulation() gives the event-based formulation, which is built through the definition of variables, constraints and objective functions with CPLEX, as given in E.1. Online appendix in our paper. The other contents, e.g., functions ColumnGeneration(), MasterProblem(ite), etc, are used for solving the path based formulations.

## Implementation
There are two alternatives to run the codes for all the results in our paper from Section 6.3 to Section 6.5: 

* Copy the files from Algorithm folder into a new C# project; Call function ReadInputData() to load the parametres and the planned train timetable from [Input](Input_data) for each instance; Construct the event-activity network (constructed with big-arcs as default setting) by object instantiation of class Network.cs. After construction of the event-activity network, our codes provide the following ways to reschedule train schedule (as specified in our paper): (1) ERST: For running ERST, call function EventBasedFormulation() directly from NewMain (i.e., the main function), which will construct an MILP for ESRT and then solve it with CPLEX using its default parameters, which termed as MIP in our paper; (2) PRST: For running PRST, first instantiate the BCTree object, which will develop an initial solution CurrentBestSolution (termed as H-strategy in our paper) and then procede to find near optimal solutions with column generations (i.e., BAP). These above methods will get you the results in Section 6.3 (by setting a time limit of 180s) and Section 6.4 (without time limit). For comparison of the four strategies (with/without SR/AR), you can do the setting as follows: if you want to use SR, just set public static int[] DepotPosition as the positions of SRs; if you want to use AR, just add numbers to public static int[] TrainPosition to indicate the adding of ARs. These will help get the results in Section 6.5. For the results in Section 6.6, you just need to alter the value of public static int CancelPenalty from its initial value to a new value and then run these four strateties.

* 2) Run the code with our software. First, compile and build the entire project with Visual Studio (or other IDE that supports .NET framework/core); Then, run the exe file WindowsFormsApplication7.exe to start the software. [User Guide](src/User_guide.pdf) gives a detailed user guide for running the software. Please following the steps to do that! 

## Other remarks

Note that: As most code comments and user guidance of the software are written in Chinese, please direct contact the developer Jiateng Yin (jtyin@bjtu.edu.cn) if you have any issues when running the project. Thanks!

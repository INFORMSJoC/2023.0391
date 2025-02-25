[![INFORMS Journal on Computing Logo](https://INFORMSJoC.github.io/logos/INFORMS_Journal_on_Computing_Header.jpg)](https://pubsonline.informs.org/journal/ijoc)

# Real-Time Rolling Stock and Timetable Rescheduling in Urban Rail Transit Systems

This archive is distributed in association with the [INFORMS Journal on
Computing](https://pubsonline.informs.org/journal/ijoc) under the [MIT License](LICENSE).

The software and data in this repository are a snapshot of the software and data
that were used in the research reported on in the paper 
[Real-time rolling stock and timetable rescheduling in urban rail transit systems](https://doi.org/10.1287/ijoc.2023.0391) by J. Yin, L. Yang, Z. Liang et al. 
The snapshot is based on 
[this SHA](https://github.com/tkralphs/JoCTemplate/commit/f7f30c63adbcb0811e5a133e1def696b74f3ba15) 
in the development repository. 

**Important: This code is being developed on an on-going basis at 
https://github.com/JerryYINJIATENG/RSTO-platform. Please go there if you would like to
get a more recent version or would like support**

## Cite

To cite the contents of this repository, please cite both the paper and this repo, using their respective DOIs.

https://doi.org/10.1287/ijoc.2023.0391

https://doi.org/10.1287/ijoc.2023.0391.cd

Below is the BibTex for citing this snapshot of the repository.

```
@misc{CacheTest,
  author =        {J. Yin, L. Yang. Z. Liang, A. D'Ariano, Z. Gao},
  publisher =     {INFORMS Journal on Computing},
  title =         {Real-Time Rolling Stock and Timetable Rescheduling in Urban Rail Transit Systems},
  year =          {2025},
  doi =           {10.1287/ijoc.2023.0391.cd},
  url =           {https://github.com/INFORMSJoC/2023.0391},
  note =          {Available for download at https://github.com/INFORMSJoC/2023.0391},
}  
```

## Description

This repository includes the data set, computational results, additional materials and source codes (with no conflict of interest) for the experiments presented in the paper.

## Data set

In our paper, we construct 30 instances to test our algorithms. The [Input_data](Input_data) folder contains all the parameters and samples used in our 30 experiments. The fold involves four sub-folders:
* Small_instances presents the parameters in the 10 small-scale instances
* Medium_instances presents the parameters in 10 medium-scale instances
* Large_instances presents the parameters in the 10 large-scale instances
* and Layout_information gives the detailed information of section running time, station information etc.
We refer to the [Parameters_config](Input_data/Parameters_config.md) file for more details regarding the input data set.

## Results

## Detailed results of the paper

[Results](Output_Results/Details_results_30_instances.xlsx) details the following results in the paper:

* The comparison among MIP, BDP and BAP (corresponding to Table 1 and Table 5in the paper)
* The comparison among H-strategy, MIP and BAP (corresponding to Table 2 and Table 6 in the paper)
* The comparison among AR and SR strategies (corresponding to Table 3)

[Time-test](Output_Results/time_test.xlsx) details the comparison of computational time (unit: s) among four strategies (corresponding to Figure 9 in the paper) over the 30 instances.


[Performance comparison](Output_Results/obj_test_2.xlsx) details the comparison of objective function values among four strategies (corresponding to Figure 10 in the paper).

[Performance comparison with different weights](Output_Results/weight_test.xlsx) details the comparison of objective function values among four strategies with different weight coefficient wf (corresponding to the resutls in Figure 12 in the paper).

[[Performance comparison between PRST and R-PRST](Output_Results/P-RST-VS-RST.xlsx)] details the comparison of objective function values between PRST and R-PRST formulations (corresponding to the results in Table 7 in online appendix of the paper)

[[Performance comparison with accelerations](Output_Results/acceleration.xlsx)] details the comparison of computational time (unit: s) with and without acceleration techniques (corresponding to the results in Table 8 in online appendix of the paper)

## Additional Examples and Supplements to the Paper

We include three additional examples and pseudocodes of the algorithms developed in our paper. We refer to [Results](Output_Results/ReadMd.md) for more details on these additional materials. 


## Replicating

For replicating these experiments, the following requirements should be satisfied
* a PC with at least 16 GB RAM
* C# run on Windows 10 (with SDK higher than 10.0.150630.0)
* CPLEX 12.80 Academic version.

We provide two alternatives to run the codes for all the results in our paper from Section 6.3 to Section 6.5: 

* Copy the files from Algorithm folder into a new C# project; Call function ReadInputData() to load the parametres and the planned train timetable from [Input](Input_data) for each instance; Construct the event-activity network (constructed with big-arcs as default setting) by object instantiation of class Network.cs. After construction of the event-activity network, our codes provide the following ways to reschedule train schedule (as specified in our paper):
    * (1) ERST: For running ERST, call function EventBasedFormulation() directly from NewMain (i.e., the main function), which will construct an MILP for ESRT and then solve it with CPLEX using its default parameters, which termed as MIP in our paper;
    * (2) PRST: For running PRST, first instantiate the BCTree object, which will develop an initial solution CurrentBestSolution (termed as H-strategy in our paper) and then procede to find near optimal solutions with column generations (i.e., BAP). These above methods will get you the results in Section 6.3 (by setting a time limit of 180s) and Section 6.4 (without time limit).
    * (3) For comparison of the four strategies (with/without SR/AR), you can do the setting as follows: if you want to use SR, just set public static int[] DepotPosition as the positions of SRs; if you want to use AR, just add numbers to public static int[] TrainPosition to indicate the adding of ARs. These will help get the results in Section 6.5. For the results in Section 6.6, you just need to alter the value of public static int CancelPenalty from its initial value to a new value and then run these four strateties.

* Run the code with our software. First, compile and build the entire project with Visual Studio (or other IDE that supports .NET framework/core); Then, run the exe file WindowsFormsApplication7.exe to start the software. [User Guide](src/User_guide.pdf) gives a detailed user guide for running the software. Please following the steps to do that! 

We involve a [ReadMe](src/ReadMe.md) file for the detailed instructions for replicating the results, the explaination of the code structures in the source files, as well as some useful notes. Please refer to that file for more details.

## Ongoing Development

This code is being developed on an on-going basis at the author's
[Github site](https://github.com/JerryYINJIATENG/RSTO-platform).

## Support

For support in using this software, submit an
[issue](https://github.com/JerryYINJIATENG/RSTO-platform/issues/new).

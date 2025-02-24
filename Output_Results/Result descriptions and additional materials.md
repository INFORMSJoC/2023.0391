# Real-Time Rolling Stock and Timetable Rescheduling in Urban Rail Transit Systems

The software and data in this repository are a snapshot of the software and data
that were used in the research reported on in the paper "Real-Time Rolling Stock and Timetable Rescheduling in Urban Rail Transit Systems" by J. Yin, L. Yang, Z.Liang, et al.
## Description

This repository includes illustrative examples, pseudocodes of the algorithm, source codes, computational results, and source data for the experiments presented in the paper, with no conflict of interest. Please note that the source data and codes are available for academic purposes only and cannot be used commercially. We encourage academic researchers to utilize these resources to replicate our findings and build upon our work.

## Requirements
For these experiments, the following requirements should be satisfied
* a PC with at least 16 GB RAM
* C# run on Windows 10 (with SDK higher than 10.0.150630.0)
* CPLEX 12.80 Academic version.

## Pseudocodes of BRC algorithm and ILS algorithm

![Algorithm 1. BRC algorithm to compute the optimal cost of big arcs](https://github.com/JerryYINJIATENG/RSTO-platform/blob/master/Materials/ILS.png)


![Algorithm 2. Improved label setting algorithm](https://github.com/JerryYINJIATENG/RSTO-platform/blob/master/Materials/LabelCorrecting.jpg)

## Illustrative examples to compute BAM model with different dual costs
Consider the following example: The network involves four stations indexed by 0 to 3; the planned running times are 2 (between station 0 and station 1), 3 (between station 1 and station 2) and 2 (between station 2 and station 3), respectively; and the slack time is 1 for each section. 

In the example, we consider only one train, and the following input:
![Input](https://github.com/JerryYINJIATENG/RSTO-platform/blob/master/Materials/input.png)

Consider that the event time for the beginning/ending of this big arc is: $t_{e_0}=3$, $t_{e_3}=10$. Now let’s consider the calculation of optimal values of $t_{e_1}$ and  $t_{e_2}$ 	according to the values of $q_e$ using the propositions specified in the paper.

Example 1:
![Example1](https://github.com/JerryYINJIATENG/RSTO-platform/blob/master/Materials/example1.png)

Example 2:
![Example2](https://github.com/JerryYINJIATENG/RSTO-platform/blob/master/Materials/example2.png)

Example 3:
![Example3](https://github.com/JerryYINJIATENG/RSTO-platform/blob/master/Materials/example3.png)


## Parameters in the experiments

There are a total of 23 stations in Beijing metro line 1. Among these stations, there are six stations and two depots that allow trains to turn their directions. The names of these stations are BJ, YQL, GZF, XD, WFJ and GM. The names of the tweo depots are GY deopt and WS depot.

![Line1](https://github.com/JerryYINJIATENG/RSTO-platform/blob/master/Materials/Line1Layout.jpg)

The basic operational parameters, involving running time between two stations, dwelling time at the platforms, minimum turnaround time, safety headway time  and slack time, etc.

* Number of stations $|I|$: 23
* Number of depots $|D|$: 2, located at GY station and WS station
* Minimum headway $\bar{h}$: 60 (unit: s)
* Turnaround time $T_{turn}$: 150 (unit: s)
* Slack travel time $\epsilon_e$: 10 (unit: s)
* Planned travel time of each section (peak-hour period): 200, 150, 150, 120, 140, 150, 110, 100, 100, 120, 60, 120, 110, 90, 90, 90, 110, 120, 90, 120, 140, 120 (unit: s)
  |Section|GY-GC|GC-BJ|BJ-BBS|BBS-YQL|YQL-WKS|WKS-WSK|WSL-GZF|GZF-JB|JB-MXD|MXD-NLSL|NLSL-FXM|FXM-XD|XD-TMX|TMX-TMD|TMD-WFJ| WFJ-DD| DD-JGM| JGM - YAL| YAL-GM| GM-DWL| DWL-SH| SH-SHD|
  |----|----|----|----|----|----|----|----|----|----|----|----|----|----|----|----|----|----|----|----|----|----|----|
  |Up-direction|200|150|150|120|140|150|110|100|100|120|80|120|110|90|90|90|110|120|90|120|140|120|
  |Down-direction|200|150|150|120|140|150|110|100|100|120|60|120|110|90|90|90|110|120|90|120|140|120|
* Planned dwelling time of each station (peak-hour period): 20, 30, 30, 25, 35, 40, 35, 45, 50, 30, 27, 50, 44, 26, 28, 30, 45, 45, 30, 45, 45, 30, 20 (unit: s)
|Station|GY|GC|BJ|BBS|YQL|WKS|WSL|GZF|JB|MXD|NLSL|FXM|XD|TMX|TMD| WFJ| DD| JGM| YAL| GM| DWL| SH| SHD|
  |----|----|----|----|----|----|----|----|----|----|----|----|----|----|----|----|----|----|----|----|----|----|----|----|
  |Up-direction|20| 30| 30| 25| 35| 40| 35| 45| 50| 30| 27| 50| 44| 26| 28| 30| 45| 45| 30| 45| 45| 30| 20|
  |Down-direction|20| 30| 30| 25| 35| 40| 35| 45| 50| 30| 27| 50| 44| 26| 28| 30| 45| 45| 30| 45| 45| 30| 20|

* Planned travel time of each section (off-peak-hour period): 210, 160, 150, 120, 150, 150, 120, 100, 100, 120, 70, 120, 110, 90, 90, 100, 110, 120, 90, 120, 140, 130 (unit: s)
  |Section|GY-GC|GC-BJ|BJ-BBS|BBS-YQL|YQL-WKS|WKS-WSK|WSL-GZF|GZF-JB|JB-MXD|MXD-NLSL|NLSL-FXM|FXM-XD|XD-TMX|TMX-TMD|TMD-WFJ| WFJ-DD| DD-JGM| JGM - YAL| YAL-GM| GM-DWL| DWL-SH| SH-SHD|
  |----|----|----|----|----|----|----|----|----|----|----|----|----|----|----|----|----|----|----|----|----|----|----|
  |Up-direction|210|160|150|120|150| 150| 120| 100| 100| 120| 80| 120| 110| 90| 90| 100| 110| 120| 90| 120| 140|130|
  |Down-direction|210|160|150|120|150| 150| 120| 100| 100| 120| 70| 120| 110| 90| 90| 100| 110| 120| 90| 120| 140|130|
  
* Planned dwelling time of each station (off-peak-hour period): 20, 30, 30, 25, 35, 35, 30, 35, 40, 30, 27, 40, 44, 26, 28, 30, 45, 30, 30, 30, 30, 30, 20 (unit: s)
|Station|GY|GC|BJ|BBS|YQL|WKS|WSL|GZF|JB|MXD|NLSL|FXM|XD|TMX|TMD| WFJ| DD| JGM| YAL| GM| DWL| SH| SHD|
  |----|----|----|----|----|----|----|----|----|----|----|----|----|----|----|----|----|----|----|----|----|----|----|----|
  |Up-direction|20| 30 | 30 | 25| 35| 35| 30| 35| 40| 30| 27| 40| 44| 26| 28| 30| 45| 30| 30| 30| 30| 30| 20|
  |Down-direction|20| 30 | 30 | 25| 35| 35| 30| 35| 40| 30| 27| 40| 44| 26| 28| 30| 45| 30| 30| 30| 30| 30| 20|

  
## Results

Disaggrated results for Table 3 (Average performance of four rescheduling strategies over the 30 instances)

|Instance|Number of canceled events|Total delay time ($\times 10^3$)|Computation time (second)|Number of canceled events|Total delay time ($\times 10^3$)|Computation time(second)|Number of canceled events|Total delay time ($\times 10^3$)|Computation time(second)|Number of canceled events|Total delay time ($\times 10^3$)|Computation time(second)|Total number of events|
|----|----|----|----|----|----|----|----|----|----|----|----|----|----|
| | Strategy 1| | |Strategy 2| | |Strategy 3| | |Strategy 4| | | |
|Small-scale instances|  | | | |  | | |  |  || | | |
|I_1| 398 | 94.2 | 1.88| 298 | 75.8 | 2.52 | 202 | 86.3 | 3.55  | 178 | 70.4 | 3.58 | 829 |
|I_2| 102 | 117.2 | 2.13 | 94 | 90.1 | 2.34 | 78| 102.4 | 3.92 |66| 95| 4.1 | 1010 |
|I_3| 128 | 121.2 | 1.08 | 122 | 94.4 | 2.49 | 100 | 111.3 | 2.78 |88.4| 88.4 | 2.88 | 688 |
|I_4|137 | 122.3 | 1.98 | 135| 90.2 | 2.20 | 104 | 149.8 | 2.89 |86| 80.2 | 3.01 | 792|
|I_5|252 | 105.8 | 2.21 | 236 | 72.3 | 2.98 | 108 | 92.5 | 3.09 | 100|72.3| 3.12 | 1002 |
|I_6| 1104 | 84.6 | 8.60 | 1010 | 68.1 | 12.1 | 563 | 87.3 | 12.0 |524| 65.9| 12.5 | 3247|
|I_7| 897 | 91.3 | 34.5 | 803 | 70.5 | 38.8 | 481 | 95.5 | 34.5 |488| 60.3 | 40.4 | 3108 |
|I_8| 674 | 131.6 | 37.3 | 662 | 99 | 59.0 | 403 | 132.0 | 42.8 |378| 89 | 62.3 | 2928|
|I_9| 889 | 117.1 | 55.3 | 784 | 75.7 | 78.1 | 442 | 101.1 | 60.2 |424| 70.5| 80.3| 3105|
|I_10| 1101 | 99.9 |20.6 | 998 | 72.2 | 26.4 | 560 | 95.9 | 25.9 |560| 68.2 | 26.9 | 4094|
|I_11| 1360 | 168 | 45.0 | 1064 | 114.4 | 58.4 | 608 | 129.8 | 50.1 |588| 104.2| 60.3| 1823|
|I_12| 1660 | 138 | 32.8 | 1460 | 100.1 | 56.7 | 638 | 131.4 | 36.8 |642|98.8 | 58.8 | 2012|
|I_13| 1059 | 93.1 | 180.0 | 1003 | 78.6 | 180.0 | 682 | 109.8 | 180.0 |648| 69.4| 180| 2944 |
|I_14| 1658 | 175.2 | 115.3 | 1422 | 113.6 | 143.1 | 632 | 124.6 | 128.8 | 620| 101.2| 160.3| 1885|
|I_15| 1274 | 104.6 | 154.2 | 1200 | 74.3 | 180.0 | 686 | 85.2 | 160.2 |668| 68.4 | 180 | 2992|
|I_16| 398 | 96.2 | 1.90 | 358| 75.5 | 1.92 | 212 | 98.7 | 2.84 |188| 69.9 | 3.00 | 2508|
|I_17| 102 | 117.8 | 2.13 | 102 | 89.7 | 3.30 | 94 | 99.4 | 3.52 |84| 82.5 | 3.58 | 2646|
|I_18| 398 | 94.2 | 1.10 | 342 | 70.0 | 2.80 | 178 | 85.4 | 2.39 |176| 66.3 | 2.44 | 2425|
|I_19| 110 | 124.1 | 1.98 | 104 | 91.3 | 2.52 | 88 | 117.3 | 3.77 |80| 84.2 | 4.02 | 2941|
|I_20| 192 | 116.8 | 2.24 | 188 | 89.4 | 2.99 | 138 | 130.2 | 4.23 |130| 81.8| 4.25| 2984|
|I_21| 897 | 86.3 | 28.5 | 808 | 66.6 | 42.3 | 542 | 89.5 | 33.2 |550| 62 | 44.2 | 1042|
|I_22| 1004 | 98.6 | 30.3 | 960 | 71.3 | 38.3 | 680 | 88.3 | 31.2 |618| 64.3| 40.1 | 1104|
|I_23| 897 | 91.3 | 60.2 | 842 | 68.9 | 75 | 578 | 91.2 | 68.8 |572| 65.7 | 76.5 | 712|
|I_24| 1104 | 84.6 | 50.4 | 988 | 69.4 | 55.1 | 608 | 90.7 | 55.9 |558| 61.2| 55.4| 704|
|I_25| 923 | 111.7 | 68.3 | 920 | 90.0 | 80.0 | 574 | 114.4 | 70.3 |528| 87| 82| 886|
|I_26| 1358 | 176.2 | 180 | 1250 | 121.0 | 180 | 622 | 140.0 | 180 |622| 103| 180| 3486|
|I_27| 1460 | 143 | 180.0 | 1298 | 102.4 | 180.0 | 630 | 134.1 | 180.0 |618| 100.1| 180| 3644|
|I_28| 978 | 108.2 | 48.3 | 930 | 74.1 | 68.7 | 564 | 90.4 | 53.8 |580| 68.3| 75.5| 2899|
|I_29| 1327  | 153.3 | 87.6 | 1180 | 112.5 | 91.4 | 496 | 124.3| 91.4| 490| 100.9| 95.1| 3248|
|I_30| 878 | 156.2 | 49.9 | 808 | 108.4 | 65.9 | 434 | 125.4| 55.8 |434| 89.4| 70.9| 2452|
|Average| 823.97 | 118.26 | 45.53 |745.67 | 86.33 | 52.1 | 424.18 |108.47 | 47.09 | 409.42 | 79.63 | 54.63| |

We test the performance of PRST and R-PRST, where the latter method relaxes the hard constraints on the ending inventories of rolling stocks, but with a penality function. Table 7 in the main text reports results for six instances, and we here refer to Materials/P-RST-VS-RST.xlsx for the detailed results for the remaining 24 instances.


We test the performance improvement of different acceleration schemes, i.e., our proposed big-arc strategy, the greedy-based column generation (termed as S1), and bi-directional search (termed as S2), in which the latter heuristics are widely used in the literature. This allows us to compare eight approaches for solving the instances: no-acceleration (NS), only S1, only S2, S1 plus S2 (S1+S2), both with and without big-arcs. The following table represents the details results for the constructed 30 instances.

|Instance|NS|S1|S2|S1+S2|NS|S1|S2|S1+S2|
|  ----  | ----  |----  |----  |----  |----  |----  |----  |----  |
| |Without big-arcs| |  | |With big-arcs| | | |
|I_1|4.8|3.98|2.45|2.43|4.57|4.50|1.90|1.88|
|I_2|5.51|5.02|4.22|2.98|5.28|4.45|2.88|2.13|
|I_3|1.03|1.04|1.03|1.03|1.03|1.05|1.04|1.08|
|I_4|2.98|2.88|2.65|2.48|2.78|2.55|2.09|1.98|
|I_5|2.96|2.92|2.92|2.87|2.54|2.42|2.33|2.21|
|I_6|14.3|12.2|13.2|10.2|10.5|9.2|10.4|8.6|
|I_7|76.8|55.6|68.7|52.3|54.3|36.4|52.1|34.5|
|I_8|70.3|58.3|63.2|48.4|50.9|39.2|48.8|37.3|
|I_9|99.8|85.4|	89.7|77.2|	80.4|	64.2|	78.5|	55.3|
|I_10|20.9|17.9|16.4|15.3|15.7|12.0|14.3|10.6|
|I_11|60.1|	52.3|	59.1|	51.2|	54.3|	47.8|	50.2|	45.0|
|I_12|50.0|42.4|48.7|39.1|45.2|37.5|35.5|32.8|
|I_13|288.2|250.4|277.5|242.3|229.9|188.9|220.4|186.0|
|I_14|180.7|166.9|178.4|164.2|134.7|130.3|133.5|115.3|
|I_15|190.8|179.4|185.5|174.3|179.4|160.9|168.6|154.2|
|I_16|2.21|2.01|2.20|1.95|2.02|2.01|2.20|1.90|
|I_17|3.53|2.98|2.99|2.21|2.45|2.23|2.44|2.21|
|I_18|2.32|2.40|1.22|1.08|1.90|1.90|1.23|1.10|
|I_19|3.01|3.00|1.90|1.89|2.20|2.11|2.10|1.98|
|I_20|3.87|3.74|2.98|2.25|3.01|2.98|2.42|2.24|
|I_21|49.8|42.2|40.3|32.3|33.7|30.5|29.8|28.5|
|I_22|42.5|40.9|40.0|38.0|41.4|40.8|33.5|30.3|
|I_23|80.4|80.1|80.0|78.1|74.4|74.3|60.8|60.2|
|I_24|95.4|90.9|90.3|82.5|69.2|69.1|52.5|50.4|
|I_25|94.1|87.6|89.2|83.9|76.9|70.4|74.2|68.3|
|I_26|264.9|235.0|252.3|223.4|210.9|202.5|209.0|198.7|
|I_27|288.9|275.5|287.4|272.2|270.3|265.4|268.8|254.4|
|I_28|75.5|69.5|70.0|59.8|60.4|55.2|56.6|48.3|
|I_29|122.2|120.2|117.9|101.2|98.8|90.3|91.2|87.6|
|I_30|80.1|71.3|70.9|68.6|65.6|52.4|55.4|49.9|
|Average|75.9|68.8|72.1|64.5|62.8|56.8|58.8|45.93|

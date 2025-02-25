[![INFORMS Journal on Computing Logo](https://INFORMSJoC.github.io/logos/INFORMS_Journal_on_Computing_Header.jpg)](https://pubsonline.informs.org/journal/ijoc)

# Real-Time Rolling Stock and Timetable Rescheduling in Urban Rail Transit Systems

This archive is distributed in association with the [INFORMS Journal on
Computing](https://pubsonline.informs.org/journal/ijoc) under the [MIT License](LICENSE).

The software and data in this repository are a snapshot of the software and data
that were used in the research reported on in the paper 
[This is a Template](https://doi.org/10.1287/ijoc.2023.0391) by J. Yin, L. Yang, Z. Liang et al. 
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

The [Input_data](Input_data) folder contains all the parameters and samples used in our experiments. The fold involves four sub-folders, Small_instances presents the parameters in the 10 small-scale instances, Medium_instances presents the parameters in 10 medium-scale instances, Large_instances presents the parameters in the 10 large-scale instances, and Layout_information gives the detailed information of section running time, station information etc. We refer to the [Parameters_config](Input_data/Parameters_config) file for more details regarding the input data set.

## Additional Examples and Supplements to the Paper

d



## Results

* The comparison among MIP, BDP and BAP (corresponding to Table 1 and Table 5in the paper)
* The comparison among H-strategy, MIP and BAP (corresponding to Table 2 and Table 6 in the paper)
* The comparison among AR and SR strategies (corresponding to Table 3)

## Replicating

For replicating these experiments, the following requirements should be satisfied
* a PC with at least 16 GB RAM
* C# run on Windows 10 (with SDK higher than 10.0.150630.0)
* CPLEX 12.80 Academic version.
* 
To replicate the results in [Figure 1](results/mult-test), do either

```
make mult-test
```
or
```
python test.py mult
```
To replicate the results in [Figure 2](results/sum-test), do either

```
make sum-test
```
or
```
python test.py sum
```

## Ongoing Development

This code is being developed on an on-going basis at the author's
[Github site](https://github.com/JerryYINJIATENG/RSTO-platform).

## Support

For support in using this software, submit an
[issue](https://github.com/JerryYINJIATENG/RSTO-platform/issues/new).

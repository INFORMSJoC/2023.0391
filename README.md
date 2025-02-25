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

This repository includes the data set, computational results, additional materials and source data (with no conflict of interest) for the experiments presented in the paper.

## Data set

The [Input_data](Input_data) folder contains all the parameters and samples used in our experiments.



```
make mult
```

Alternatively, to build the version that sums the elements of a vector (used
to obtain the results [Figure 2](results/sum-test.png) in the paper), stepping K
elements at a time, do the following.

```
make clean
make sum
```

Be sure to make clean before building a different version of the code.

## Additional examples for the supplement of the paper

d

## Results

* The comparison among MIP, BDP and BAP (corresponding to Table 1 and Table 5in the paper)
* The comparison among H-strategy, MIP and BAP (corresponding to Table 2 and Table 6 in the paper)
* The comparison among AR and SR strategies (corresponding to Table 3)

## Replicating

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

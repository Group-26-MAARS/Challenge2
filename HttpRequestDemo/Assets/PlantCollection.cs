using System;
using System.Collections.Generic;

public class PlantCollection
{
    private List<Plant> plantList;

    internal List<Plant> PlantList { get => plantList; set => plantList = value; }
}
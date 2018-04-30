using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public enum MapGenPhase
    {
        TilePlacement,
        CityPlacement,
        TownPlacement,
        TerritoryDefinition,
        NavMapDefinition,
        RoadPlacement,
        CapitalDefinition,
        Finished
    }

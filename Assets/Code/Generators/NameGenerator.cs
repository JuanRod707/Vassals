using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class NameGenerator
{
    private static string[] cities =
    {
        "Boroughton", "Bredon", "Aelinmiley", "Culcheth", "Holden", "Beachcastle", "Narfolk", "Stanmore", "Garmsby",
        "Bredwardine", "Draydon Keep", "Howers Fortress", "Stowe Citadel", "Iron Hill", "Black Tower", "Gorst-Penn", "Skystead", "Tergaron"
    };

    private static string[] towns =
    {
        "Cesterfield", "Daemarrel", "Ivywood", "Spalding", "Jongvale", "Clarcton", "Kirekwall", "Lewes", "Wintervale",
        "Fernsworth", "Stormband", "Rivermore", "Fearmond", "Wildeholde", "Rockstrand", "Windvalley"
    };

    private static string[] houses =
    {
        "Audouin", "Lambequin", "Yvon", "Otebon", "Gawn", "Sewal", "Girardus", "Honfroy", "Symond", "Jehanel", "Girout",
        "Anselmun", "Thybaut", "Garratt", "Foukaut", "Ilbert", "Danyel", "Von Krieg", "Orwell", "Sterling"
    };

    private static string[] generalTitle =
    {
        "Lord", "General", "Chancellor", "Commander", "Jarl", "Commodore", "Warlord"
    };

    private static string[] generalNames =
    {
        "Lougard", "Strong", "Lancer", "Kroos", "Damesto", "Cullen", "Astar", "Coulie", "Thomason", "Elfson", "Mattock",
        "Arcturus", "Longspear", "Floran", "Saul", "Koran"
    };

    private static string[] maleNames =
    {
        "Franc", "Stamar", "Albert", "John", "Hans", "Robert", "Edward", "Connor", "Stuart", "Francois", "Edmund",
        "Earl", "Joseph", "Dunnar", "Leonardo", "Angelo", "Lucius", "Ivan", "Darius"
    };

    public static string GenerateCityName()
    {
        return cities.PickOne();
    }

    public static string GenerateTownName()
    {
        return towns.PickOne();
    }

    public static string GenerateLordName()
    {
        return string.Format("{0} {1}", maleNames.PickOne(), houses.PickOne());
    }

    public static string GenerateHouseName()
    {
        return houses.PickOne();
    }

    public static string GenerateGeneralName()
    {
        return string.Format("{0} {1}", generalTitle.PickOne(), generalNames.PickOne());
    }
}

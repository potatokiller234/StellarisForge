using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class data_system : data_base
{
    public List<data_planet> planets;
    public List<data_asteroid> asteroids;
    public string initializerName;
    public string systemClass;
    public DLC currentDLC;
    public bool isRandom;

    public data_system(string initializerName)
    {
        currentDLC = DLC.noDLC;
        planets = new List<data_planet>();
        asteroids = new List<data_asteroid>();
        unknownData = new List<string>();
        flags = new List<string>();
        this.initializerName = initializerName;
        name = "randDefault";
        isRandom = false;
    }
    /// <summary>
    /// Only used when trying to create a null init system (IE - Random System)
    /// </summary>
    public data_system()
    {
        currentDLC = DLC.noDLC;
        //planets = new List<data_planet>();
        //asteroids = new List<data_asteroid>();
        //unknownData = new List<string>();
        //flags = new List<string>();
        //this.initializerName = initializerName;
        name = "randDefault";
        isRandom = true;
    }
    public data_system(string initializerName, string modName, string fileName)
    {
        currentDLC = DLC.noDLC;
        planets = new List<data_planet>();
        asteroids = new List<data_asteroid>();
        unknownData = new List<string>();
        flags = new List<string>();
        this.initializerName = initializerName;
        this.modName = modName;
        this.fileName = fileName;
        name = "randDefault";
        isRandom = false;
    }

    //public static bool operator ==(data_system first, data_system second)
    //{
    //    if (ReferenceEquals(first, second))
    //        return true;
    //    if (ReferenceEquals(first, null))
    //        return false;
    //    if (ReferenceEquals(second, null))
    //        return false;
    //    return first.Equals(second);
    //}
    //public static bool operator !=(data_system first, data_system second) => !(first == second);
    //public bool Equals(data_system other)
    //{
    //    if (ReferenceEquals(other, null))
    //        return false;
    //    if (ReferenceEquals(this, other))
    //        return true;
    //    return planets.Count == other.planets.Count && asteroids.Count == other.asteroids.Count &&
    //        initializerName == other.initializerName && systemClass == other.systemClass;
    //}


    public bool doesEqual(data_system other)
    {
        return planets.Count == other.planets.Count && asteroids.Count == other.asteroids.Count &&
            initializerName == other.initializerName && systemClass == other.systemClass;
    }


    //public data_system(string initializerName, string name, string systemClass)
    //{
    //    planets = new List<data_planet>();
    //    asteroids = new List<data_asteroid>();
    //    unknownData = new List<string>();
    //    flags = new List<string>();
    //    this.initializerName = initializerName;
    //    this.name = name;
    //    this.systemClass = systemClass;
    //}

    public void addPlanet(data_planet planetToAdd)
    {
        planets.Add(planetToAdd);
    }
    public void addAsteroid(data_asteroid asteroidToAdd)
    {
        asteroids.Add(asteroidToAdd);
    }
    public void addUnknownData(string unknownDataToAdd)
    {
        unknownData.Add(unknownDataToAdd);
    }
    public void setDLC(DLC dlcToAdd)
    {
        //myStaticScripts.staticScripts.debugMessage(dlcToAdd + " THIS DLC WAS ADDED---------------");
        currentDLC = dlcToAdd;
    }
    public void addFlag(string flagToAdd)
    {
        flags.Add(flagToAdd);
    }
    public string printOutPlanet(List<data_planet> planets)
    {
        string planetData = "";
        for(int i =0;i<planets.Count;i++)
        {
            planetData += "planet_name-" + planets[i].planet_name + "\n";
            planetData += "planet_countMax-" + planets[i].planet_countMax + "\n";
            planetData += "planet_countMin-" + planets[i].planet_countMin + "\n";
            planetData += "planet_orbit_distanceMax-" + planets[i].planet_orbit_distanceMax + "\n";
            planetData += "planet_orbit_distanceMin-" + planets[i].planet_orbit_distanceMin + "\n";
            planetData += "planet_orbit_angleMax-" + planets[i].planet_orbit_angleMax + "\n";
            planetData += "planet_orbit_angleMin-" + planets[i].planet_orbit_angleMin + "\n";
            planetData += "planet_sizeMax-" + planets[i].planet_sizeMax + "\n";
            planetData += "planet_sizeMin-" + planets[i].planet_sizeMin + "\n";
            planetData += "planet_class-" + planets[i].planet_class + "\n";
            planetData += "hasRing-" + planets[i].hasRing + "\n";
            planetData += "home_planet-" + planets[i].home_planet + "\n";
            planetData += "\n";
        }
        return planetData;
    }
    public string printOutAsteroids(List<data_asteroid> asteroids)
    {
        string Asteroids = "";
        for (int i = 0; i < asteroids.Count; i++)
        {
            Asteroids += "asteroid_type-" + asteroids[i].type + "\n";
            Asteroids += "asteroid_radius-" + asteroids[i].radius + "\n";
            Asteroids += "\n";
        }
        return Asteroids;
    }
    public string printOutUnkownData(List<string> unknownData)
    {
        string UnknownData = "";
        for (int i = 0; i < unknownData.Count; i++)
        {
            UnknownData += unknownData[i];
        }
        return UnknownData;
    }
    public string printOutFlags(List<string> flags)
    {
        string Flags = "";
        for (int i = 0; i < flags.Count; i++)
        {
            Flags += "Flags-" + asteroids[i].type + ",";
        }
        return Flags;
    }

}

public enum DLC
{
    noDLC,
    symbols_of_domination,
    arachnoid,
    signup_bonus,
    plantoid,
    creatures_of_the_void,
    leviathans,
    horizon_signal,
    utopia,
    anniversary,
    synthetic_dawn,
    apocalypse,
    humanoids,
    distant_stars,
    megacorp,
    ancient_relics,
    lithoids,
    federations,
    necroids,
    nemesis,
    aquatics
}


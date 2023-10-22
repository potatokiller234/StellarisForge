using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class data_planet : data_base
{
    #region planet_variables
    /// <summary>
    /// maxCount num, if countMax = countMin then countMax will be chosen
    /// </summary>
    public float planet_countMax;
    public float planet_countMin;

    /// <summary>
    /// distanceMax num, if distanceMax = distanceMin then distanceMax will be chosen
    /// </summary>
    public float planet_orbit_distanceMax;
    public float planet_orbit_distanceMin;

    /// <summary>
    /// angleMax num, if angleMax = angleMin then angleMax will be chosen
    /// </summary>
    public float planet_orbit_angleMax;
    public float planet_orbit_angleMin;

    /// <summary>
    /// sizeMax num, if sizeMax = angleMax then sizeMax will be chosen
    /// </summary>
    public float planet_sizeMax;
    public float planet_sizeMin;

    public string planet_class;
    public string planet_name;
    public bool hasRing;// Unkown
    public bool home_planet;//Neet to see this up 
    public List<data_planet> moons;

    #endregion

    /// <summary>
    /// Input for dataPlanet (NOTE: if null use -1 or lower)
    /// </summary>
    /// <param name="planet_countMax"></param>
    /// <param name="planet_countMin"></param>
    /// <param name="planet_orbit_distanceMax"></param>
    /// <param name="planet_orbit_distanceMin"></param>
    /// <param name="planet_orbit_angleMax"></param>
    /// <param name="planet_orbit_angleMin"></param>
    /// <param name="planet_orbit_sizeMax"></param>
    /// <param name="planet_orbit_sizeMin"></param>
    /// <param name="planet_class"></param>
    /// <param name="hasRing"></param>
    /// <param name="planet_name"></param>

    public data_planet(int planet_countMax, int planet_countMin, int planet_orbit_distanceMax, int planet_orbit_distanceMin,
                       int planet_orbit_angleMax, int planet_orbit_angleMin, int planet_sizeMax, int planet_sizeMin,
                       string planet_class, bool hasRing, string planet_name)
    {
        this.planet_countMax = planet_countMax;
        this.planet_countMin = planet_countMin;

        this.planet_orbit_distanceMax = planet_orbit_distanceMax;
        this.planet_orbit_distanceMin = planet_orbit_distanceMin;

        this.planet_orbit_angleMax = planet_orbit_angleMax;
        this.planet_orbit_angleMin = planet_orbit_angleMin;

        this.planet_sizeMax = planet_sizeMax;
        this.planet_sizeMin = planet_sizeMin;

        this.planet_class = planet_class;
        this.hasRing = hasRing;
        this.planet_name = planet_name;

        moons = new List<data_planet>();
        unknownData = new List<string>();
        flags = new List<string>();
    }
    public data_planet()
    {
        this.planet_countMax = 1;
        this.planet_countMin = 1;

        this.planet_orbit_distanceMax = -1;
        this.planet_orbit_distanceMin = -1;

        this.planet_orbit_angleMax = -1;
        this.planet_orbit_angleMin = -1;

        this.planet_sizeMax = -1;
        this.planet_sizeMin = -1;

        this.planet_class = null;
        this.hasRing = false;
        this.planet_name = null;


        moons = new List<data_planet>();
        unknownData = new List<string>();
        flags = new List<string>();
    }

    public void addMoon(data_planet moonToAdd)
    {
        moons.Add(moonToAdd);
    }

    //public string printOutMoons()
    //{
    //    string planetData = "";
    //    for (int i = 0; i < moons.Count; i++)
    //    {
    //        planetData += "moon_name-" + moons[i].planet_name + "\n";
    //        planetData += "moon_countMax-" + moons[i].planet_countMax + "\n";
    //        planetData += "moon_countMin-" + moons[i].planet_countMin + "\n";
    //        planetData += "moon_orbit_distanceMax-" + moons[i].planet_orbit_distanceMax + "\n";
    //        planetData += "moon_orbit_distanceMin-" + moons[i].planet_orbit_distanceMin + "\n";
    //        planetData += "moon_orbit_angleMax-" + moons[i].planet_orbit_angleMax + "\n";
    //        planetData += "moon_orbit_angleMin-" + moons[i].planet_orbit_angleMin + "\n";
    //        planetData += "moon_sizeMax-" + moons[i].planet_sizeMax + "\n";
    //        planetData += "moon_sizeMin-" + moons[i].planet_sizeMin + "\n";
    //        planetData += "moon_class-" + moons[i].planet_class + "\n";
    //        planetData += "hasRing-" + moons[i].hasRing + "\n";
    //        planetData += "home_planet-" + moons[i].home_planet + "\n";
    //        planetData += "\n";
    //    }
    //    return planetData;

    //}


    /// <summary>
    /// If min == max then return array with max, else return array with max and min
    /// </summary>
    /// <returns></returns>
    public int[] get_planet_count()
    {
        int[] thingToReturn = null;

        if(planet_countMax == planet_countMin)
        {
            thingToReturn = new int[] { (int)planet_countMax };
        }
        else
        {
            thingToReturn = new int[] { (int)planet_countMax, (int)planet_countMin };
        }
        return thingToReturn;
    }
    /// <summary>
    /// If min == max then return array with max, else return array with max and min
    /// </summary>
    /// <returns></returns>
    public int[] get_planet_orbit_distance()
    {
        int[] thingToReturn = null;

        if (planet_orbit_distanceMax == planet_orbit_distanceMin)
        {
            thingToReturn = new int[] { (int)planet_orbit_distanceMax };
        }
        else
        {
            thingToReturn = new int[] { (int)planet_orbit_distanceMax, (int)planet_orbit_distanceMin };
        }
        return thingToReturn;
    }
    /// <summary>
    /// If min == max then return array with max, else return array with max and min
    /// </summary>
    /// <returns></returns>
    public int[] get_planet_orbit_angle()
    {
        int[] thingToReturn = null;

        if (planet_orbit_angleMax == planet_orbit_angleMin)
        {
            thingToReturn = new int[] { (int)planet_orbit_angleMax };
        }
        else
        {
            thingToReturn = new int[] { (int)planet_orbit_angleMax, (int)planet_orbit_angleMin };
        }
        return thingToReturn;
    }
    
    /// <summary>
    /// If min == max then return array with max, else return array with max and min
    /// </summary>
    /// <returns></returns>
    public int[] get_planet_size()
    {
        int[] thingToReturn = null;

        if (planet_sizeMax == planet_sizeMin)
        {
            thingToReturn = new int[] { (int)planet_sizeMax };
        }
        else
        {
            thingToReturn = new int[] { (int)planet_sizeMax, (int)planet_sizeMin };
        }
        return thingToReturn;
    }




}

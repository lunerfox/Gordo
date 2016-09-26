using UnityEngine;
using System.Collections;

//A collection of useful and not so useful functions.

public static class Utils : object {

    

    public static string getRandomName()
    {
        //Top 20 names for boys and girls in 2008.
        string[] names = new string[] {"PolyBug Jacob", "PolyBug Michael", "PolyBug Ethan", "PolyBug Joshua", "PolyBug Daniel", "PolyBug Alexander", "Polybug William",  "Polybug Anthony", "Polybug Christopher", "Polybug Matthew"
                                    ,"Polybug Emma",  "Polybug Isabella",  "Polybug Emily",  "Polybug Olivia",  "Polybug Ava",  "Polybug Madison",  "Polybug Sophia",  "Polybug Abigail",  "Polybug Elizabeth",  "Polybug Chloe"};

        int val = Random.Range(0, names.Length);

        return names[val];
    }

}

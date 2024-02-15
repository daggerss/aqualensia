using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureDatabase : MonoBehaviour
{
    [SerializeField] private Creature[] coralReefCreatures;
    [SerializeField] private Creature[] seagrassBedCreatures;
    [SerializeField] private Creature[] openOceanCreatures;

    public Creature[] GetCreatures(Biome biome)
    {
        if (biome == Biome.CoralReef)
        {
            return coralReefCreatures;
        }

        else if (biome == Biome.SeagrassBed)
        {
            return seagrassBedCreatures;
        }

        else if (biome == Biome.OpenOcean)
        {
            return openOceanCreatures;
        }

        return null;
    }
}

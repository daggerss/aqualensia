using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureDatabase : MonoBehaviour
{
    [SerializeField] private Creature[] coralReefCreatures;
    [SerializeField] private Creature[] seagrassBedCreatures;
    [SerializeField] private Creature[] openOceanCreatures;

    private Creature[] _allCreatures;
    public Creature[] AllCreatures => _allCreatures;

    void Awake()
    {
        // Set total num of creatures
        int totalNum = coralReefCreatures.Length + seagrassBedCreatures.Length +
                       openOceanCreatures.Length;
        _allCreatures = new Creature[totalNum];

        // Merge all creatures via copy to
        coralReefCreatures.CopyTo(_allCreatures, 0);
        seagrassBedCreatures.CopyTo(_allCreatures, coralReefCreatures.Length);
        openOceanCreatures.CopyTo(_allCreatures, coralReefCreatures.Length + seagrassBedCreatures.Length);
    }

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

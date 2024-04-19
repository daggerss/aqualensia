using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CreatureDatabase : MonoBehaviour
{
    [Header("By Biome")]
    [SerializeField] private Creature[] coralReefCreatures;
    [SerializeField] private Creature[] seagrassBedCreatures;
    [SerializeField] private Creature[] openOceanCreatures;

    [Header("By Location: Coral Reefs")]
    [SerializeField] private Creature[] creaturesC1;
    [SerializeField] private Creature[] creaturesC2;
    [SerializeField] private Creature[] creaturesC3;

    [Header("By Location: Seagrass Beds")]
    [SerializeField] private Creature[] creaturesS1;
    [SerializeField] private Creature[] creaturesS2;
    [SerializeField] private Creature[] creaturesS3;

    [Header("By Location: Open Ocean")]
    [SerializeField] private Creature[] creaturesO1;
    [SerializeField] private Creature[] creaturesO2;
    [SerializeField] private Creature[] creaturesO3;

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

    public Creature[] GetCreatures(string loc)
    {
        if (loc == "C1")
        {
            return creaturesC1;
        }

        else if (loc == "C2")
        {
            return creaturesC2;
        }

        else if (loc == "C3")
        {
            return creaturesC3;
        }

        else if (loc == "S1")
        {
            return creaturesS1;
        }

        else if (loc == "S2")
        {
            return creaturesS2;
        }

        else if (loc == "S3")
        {
            return creaturesS3;
        }

        else if (loc == "O1")
        {
            return creaturesO1;
        }

        else if (loc == "O2")
        {
            return creaturesO2;
        }

        else if (loc == "O3")
        {
            return creaturesO3;
        }

        return null;
    }

    public bool HasIdentified()
    {
        return _allCreatures.Any(creature => creature.CaptureStatus == CreatureStatus.Identified);
    }
}

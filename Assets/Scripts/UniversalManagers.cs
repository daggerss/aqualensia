using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniversalManagers : MonoBehaviour
{
    public static UniversalManagers instance;

    void Awake()
    {
        // Destroy duplicate instances
        if (instance == null)
        {
            instance = this;

            // Keep managers between scenes
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }
}

/* -------------------------------------------------------------------------- */
/*                               Universal Enums                              */
/* -------------------------------------------------------------------------- */

// WORLD

// ! TimeOfDay: Semantics/Clarity Note
// If I had dawn and dusk, I'd use flags
// or total 4 for crepuscular and cathemeral creatures
// But for now, this is okay
public enum TimeOfDay { Day, Night, Both };

// ENVIRONMENT
public enum OceanZone { Sunlight, Twilight, Midnight, Abyssal, Hadal };
public enum Biome {CoralReef, SeagrassBed, OpenOcean};

// CREATURES
public enum CreatureStatus { Unknown, Captured, Identified };
public enum ConservationStatus { NE, DD, LC, NT, VU, EN, CR };

// BLOCKERS
public enum BlockerType { Pollution, DestructiveFishing, Development };

// UI
public enum ZoneState { Default, WithinRange, Error };
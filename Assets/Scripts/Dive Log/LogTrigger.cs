using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LogTrigger : MonoBehaviour
{
    private LogManager diveLog;
    private CreatureInDive creatureInstance;
    private Creature creature;

    void OnEnable()
    {
        // Add log to capture event
        PhotoCapture.OnPhotoCapture += Log;
    }

    void Start()
    {
        // Dive Log
        diveLog = UniversalManagers.instance.GetComponentInChildren<LogManager>();
    }

    // Get Creature In View
    private void OnTriggerStay2D(Collider2D other)
    {
        creatureInstance = other.gameObject.GetComponent<CreatureInDive>();
        creature = creatureInstance.Creature;
    }

    // Remove Creature
    private void OnTriggerExit2D(Collider2D other)
    {
        creature = null;
    }

    // Log Shots + Creature
    private void Log()
    {
        diveLog.CurrentBestShots += 1;
        LogCreature();
    }

    // Log Creature Info
    private void LogCreature()
    {
        // A creature is in view and it's an unencountered instance
        if (creature != null && !creatureInstance.WasCaptured)
        {
            // Creature in Dive
            creatureInstance.WasCaptured = true;

            // Scriptable Creature
            creature.CaptureCount += 1;

            /* -------------------------- Dive Log -------------------------- */
            // Get dive log's index of creature in view
            int index = diveLog.CapturedCreatures.FindIndex(x =>
                        x.CapturedCreature.ScientificName ==
                        creature.ScientificName);

            // Already captured in this dive
            if (index != -1)
            {
                // Logged Creature
                diveLog.CapturedCreatures[index].CaptureCount += 1;
            }

            // New capture in this dive
            else
            {
                CreatureLog logInfo = new CreatureLog();

                logInfo.CapturedCreature = creature;
                logInfo.CaptureCount = 1;

                // New capture overall
                if (creature.CaptureStatus == CreatureStatus.Unknown)
                {
                    // Scriptable Creature
                    creature.CaptureStatus = CreatureStatus.Captured;

                    // Logged Creature
                    logInfo.isNew = true;
                }

                // Previously captured overall
                else
                {
                    logInfo.isNew = false;
                }

                // Add to log
                diveLog.CapturedCreatures.Add(logInfo);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LogTrigger : MonoBehaviour
{
    private LogManager diveLog;
    private Creature creatureInView;

    void Start()
    {
        // Dive Log
        diveLog = UniversalManagers.instance.GetComponentInChildren<LogManager>();
    }

    // Log Shots + Creature
    public void Log()
    {
        diveLog.CurrentBestShots += 1;
        LogCreature();
    }

    // Get Creature In View
    private void OnTriggerStay2D(Collider2D other)
    {
        creatureInView = other.gameObject.GetComponent<CreatureInDive>().Creature;
    }

    // Remove Creature
    private void OnTriggerExit2D(Collider2D other)
    {
        creatureInView = null;
    }

    // Log Creature Info
    private void LogCreature()
    {
        if (creatureInView != null)
        {
            // Scriptable Creature
            creatureInView.CaptureCount += 1;

            // Get dive log's index of creature in view
            int index = diveLog.CapturedCreatures.FindIndex(x =>
                        x.CapturedCreature.ScientificName ==
                        creatureInView.ScientificName);

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

                logInfo.CapturedCreature = creatureInView;
                logInfo.CaptureCount = 1;

                // New capture overall
                if (creatureInView.CaptureStatus == CreatureStatus.Unknown)
                {
                    // Scriptable Creature
                    creatureInView.CaptureStatus = CreatureStatus.Captured;

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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleDetector : MonoBehaviour
{
    [SerializeField] private int idleTimeoutSeconds;

    private float lastIdleTime;

    void Awake()
    {
        // Set last idle time
        lastIdleTime = Time.time;
    }

    void Update()
    {
        // Check any input
        if (Input.anyKey || Input.GetAxis("Mouse X") != 0f || Input.GetAxis("Mouse Y") != 0f)
        {
            // Reset last idle time
            lastIdleTime = Time.time;
        }
    }

    // Check if idle
    public bool IsIdle()
    {
        return Time.time - lastIdleTime > idleTimeoutSeconds;
    }
}

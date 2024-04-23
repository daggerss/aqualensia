using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private DialogueManager dialogueManager;
    [SerializeField] private TutorialObjSequence[] objectSequences;
    [SerializeField] private TutorialTrigger[] triggers;

    private int currentSequence;
    private TutorialObjSequence currentObjSequence;

    private TutorialTrigger currentTrigger;

    void Awake()
    {
        // Add methods to dialogue action
        DialogueManager.OnNextDialogue += ActivateTutorialSequence;
        DialogueManager.OnNextDialogue += SetUpTrigger;

        // If new game
        if (PlayerPrefs.GetInt("NewGame", 1) == 1)
        {
            currentSequence = PlayerPrefs.GetInt("TutorialSequence", 0);

            if (currentSequence > 7)
            {
                PlayerPrefs.SetInt("NewGame", 0);
                return;
            }

            SetUpTrigger();

            if (currentTrigger == null) // If no trigger, auto play
            {
                StartSequence();
            }
        }
    }

    void Update()
    {
        // If new game
        if (PlayerPrefs.GetInt("NewGame", 1) == 1)
        {
            if (currentTrigger == null && !dialogueManager.SequenceComplete)
            {
                // Play Dialogue
                dialogueManager.PlaySequence(currentSequence);
            }
        }
    }

    // Start tutorial sequence
    private void StartSequence()
    {
        // Set + initialize current object sequence
        currentObjSequence = FindSequence(currentSequence);
        
        if (currentObjSequence != null)
        {
            ActivateTutorialSequence();
        }

        // Initialize dialogue play
        dialogueManager.StartSequence(currentSequence);
    }

    // Add start to trigger
    private void SetUpTrigger()
    {
        if (triggers != null)
        {
            // Check each trigger for matching sequence & index
            foreach (TutorialTrigger trigger in triggers)
            {
                // Match sequence and index
                if (currentSequence == trigger.SequenceID && dialogueManager.DialogueIndex == trigger.DialogueIndex)
                {
                    // Add onclick
                    trigger.Button.onClick.AddListener(StartSequence);
                    currentTrigger = trigger;
                    return;
                }
            }
        }

        currentTrigger = null;
    }

    // Find current object sequence
    private TutorialObjSequence FindSequence(int id)
    {
        int index = Array.FindIndex(objectSequences, x => x.SequenceID == id);

        if (index == -1) // Not found
        {
            return null;
        }

        return objectSequences[index];
    }

    // Play tutorial sequence
    private void ActivateTutorialSequence()
    {
        if (currentObjSequence != null)
        {
            // Check each tutorial obj for matching index
            foreach (TutorialObject obj in currentObjSequence.TutorialObjects)
            {
                // Match index
                if (dialogueManager.DialogueIndex == obj.DialogueIndex)
                {
                    // Do corresponding action
                    ExecuteTutorialObject(obj.Action, obj.GameObjects);
                }
            }
        }
    }

    // Execute action
    private void ExecuteTutorialObject(TutorialAction action, GameObject[] objs)
    {
        // Show
        if (action == TutorialAction.Show)
        {
            ShowObjects(objs);
        }

        // Hide
        else if (action == TutorialAction.Hide)
        {
            HideObjects(objs);
        }

        // Enable
        else if (action == TutorialAction.Enable)
        {
            EnableObjects(objs);
        }

        // Disable
        else if (action == TutorialAction.Disable)
        {
            DisableObjects(objs);
        }
    }

    // Show objects
    private void ShowObjects(GameObject[] gameObjects)
    {
        foreach (GameObject obj in gameObjects)
        {
            obj.SetActive(true);
        }
    }

    // Hide objects
    private void HideObjects(GameObject[] gameObjects)
    {
        foreach (GameObject obj in gameObjects)
        {
            obj.SetActive(false);
        }
    }

    // Enable buttons
    private void EnableObjects(GameObject[] gameObjects)
    {
        foreach (GameObject obj in gameObjects)
        {
            if (obj.TryGetComponent<Button>(out Button btn))
            {
                btn.enabled = true;
            }
        }
    }

    // Disable buttons
    private void DisableObjects(GameObject[] gameObjects)
    {
        foreach (GameObject obj in gameObjects)
        {
            if (obj.TryGetComponent<Button>(out Button btn))
            {
                btn.enabled = false;
            }
        }
    }
}

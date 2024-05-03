using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class DialogueManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TMP_Text bodyText;
    [SerializeField] private GameObject arrow;

    [Header("Behavior")]
    [SerializeField]
    [Range(1f, 100f)]
    private float lettersPerSecond;

    [Header("Content")]
    [SerializeField] private DialogueSequence[] dialogueSequences;

    private int _dialogueIndex;
    public int DialogueIndex => _dialogueIndex;

    private bool _sequenceComplete;
    public bool SequenceComplete => _sequenceComplete;

    [HideInInspector]
    public static event Action OnNextDialogue;

    private bool isPlaying;
    private Coroutine currentCoroutine;

    void OnEnable()
    {
        // Reset index
        _dialogueIndex = 0;

        // Reset sequence state
        _sequenceComplete = false;
    }

    void OnDisable()
    {
        // Reset action
        OnNextDialogue = null;
    }

    // Find current dialogue sequence
    private DialogueSequence FindSequence(int id)
    {
        int index = Array.FindIndex(dialogueSequences, x => x.SequenceID == id);

        if (index == -1) // Not found
        {
            return null;
        }

        return dialogueSequences[index];
    }

    public void StartSequence(int id)
    {
        DialogueSequence currentSequence = FindSequence(id);

        if (currentSequence != null)
        {
            currentCoroutine = StartCoroutine(PlayDialogue(FindSequence(id).Dialogues[_dialogueIndex]));
        }
    }

    public void PlaySequence(int id)
    {
        // Interaction - LMB
        if (Input.GetMouseButtonDown(0))
        {
            // Get sequence
            DialogueSequence currentSequence = FindSequence(id);

            if (currentSequence != null && !_sequenceComplete)
            {
                // Additional actions
                OnNextDialogue?.Invoke(); // ActivateObjSequence, SetUpTrigger
    
                // Check for end
                if (DialogueEnd(currentSequence))
                {
                    return;
                }
    
                // Get Dialogue
                Dialogue currentDialogue = currentSequence.Dialogues[_dialogueIndex];
    
                // Play
                if (!isPlaying)
                {
                    currentCoroutine = StartCoroutine(PlayDialogue(currentDialogue));
                }
    
                // Skip
                else
                {
                    // Stop playing
                    StopCoroutine(currentCoroutine);
    
                    // Clear and set text
                    bodyText.text = "";
                    bodyText.text = currentDialogue.Text;
    
                    // Set state
                    HideArrow(currentDialogue.DisableTrigger);
                    isPlaying = false;
                    _dialogueIndex++;

                    // Check for end
                    if (DialogueEnd(currentSequence))
                    {
                        return;
                    }
                }
            }

            else if (currentSequence == null || currentSequence.Dialogues.Length == 0)
            {
                // Increment Tutorial Sequence
                PlayerPrefs.SetInt("TutorialSequence", PlayerPrefs.GetInt("TutorialSequence", 0) + 1);

                // Disable
                _sequenceComplete = true;
                enabled = false;
                return;
            }
        }
    }

    // Display dialogue
    IEnumerator PlayDialogue(Dialogue dialogue)
    {
        // Set state
        isPlaying = true;
        HideArrow(true);
        SetPosition(dialogue.AnchorUp);
        float delay = 1f / lettersPerSecond;

        // Clear text
        bodyText.text = "";

        // Animation
        for (int i = 0; i < dialogue.Text.Length; i++)
        {
            // Set up text
            string textChunk = "";
            textChunk += dialogue.Text[i];

            // Account for spaces between words
            if (dialogue.Text[i] == ' ' && i < dialogue.Text.Length - 1)
            {
                textChunk = dialogue.Text.Substring(i, 2);
                i++;
            }

            // Escape rich text
            if (dialogue.Text[i] == '<')
            {
                while (dialogue.Text[i] != '>')
                {
                    textChunk += dialogue.Text[i+1];
                    i++;
                }
            }

            // Show
            bodyText.text += textChunk;

            // Delay
            yield return new WaitForSeconds(delay);
        }

        // Set state
        HideArrow(dialogue.DisableTrigger);
        isPlaying = false;
        _dialogueIndex++;
    }

    // Check for end of dialogue
    private bool DialogueEnd(DialogueSequence sequence)
    {
        // Check out of bounds = done
        if (_dialogueIndex == sequence.Dialogues.Length)
        {
            // Increment Tutorial Sequence
            PlayerPrefs.SetInt("TutorialSequence", PlayerPrefs.GetInt("TutorialSequence", 0) + 1);

            // Disable
            _sequenceComplete = true;
            enabled = false;
            return true;
        }

        return false;
    }

    // Hide arrow
    private void HideArrow(bool toHide)
    {
        arrow.SetActive(!toHide);
    }

    // Move up or down
    private void SetPosition(bool moveUp)
    {
        RectTransform rectTransform = GetComponent<RectTransform>();

        if (moveUp)
        {
            rectTransform.anchorMin = new Vector2(0.5f, 0.75f);
            rectTransform.anchorMax = new Vector2(0.5f, 0.75f);
        }

        else
        {
            rectTransform.anchorMin = new Vector2(0.5f, 0);
            rectTransform.anchorMax = new Vector2(0.5f, 0);
        }
    }
}

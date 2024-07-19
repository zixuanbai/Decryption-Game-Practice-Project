using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueUI : MonoBehaviour
{
    public GameObject panel;
    public Text dialogueText;

    private void OnEnable()
    {
        EventHandler.ShowDialogueEvent += ShowDialogue;
    }

    private void OnDisable()
    {
        EventHandler.ShowDialogueEvent -= ShowDialogue;
    }

    private void ShowDialogue(string dialogue)
    {
        if (!string.IsNullOrEmpty(dialogue))
        {
            panel.SetActive(true);
            dialogueText.text = dialogue;
        }
        else
        {
            panel.SetActive(false);
        }
    }
}

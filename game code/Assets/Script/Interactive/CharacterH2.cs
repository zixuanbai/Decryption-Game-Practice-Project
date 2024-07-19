using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DialogueController))]
public class CharacterH2 : Interactive
{
    private DialogueController dialogueController;

    private void Awake()
    {
        dialogueController = GetComponent<DialogueController>();
    }


    public override void EmptyClicked()
    {
        if (isDone)
            dialogueController.ShowDialogueFihish();
        else
            dialogueController.ShowDialogueEmpty();
    }


    protected override void OnClickedAction()
    {
        dialogueController.ShowDialogueFihish();
    }
}

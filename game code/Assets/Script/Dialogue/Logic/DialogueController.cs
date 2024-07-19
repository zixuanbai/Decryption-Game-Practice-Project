using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueController : MonoBehaviour
{
    public DialogueData_SO dialogueEmpty;
    public DialogueData_SO dialogueFinish;

    private Stack<string> dialogueEmptyStack;
    private Stack<string> dialogueFinishStack;

    private bool isTalking;

    private void Awake()
    {
        FillDialogueStack();
    }

    private void FillDialogueStack()
    {
        dialogueEmptyStack = new Stack<string>();
        dialogueFinishStack = new Stack<string>();

        for (int i = dialogueEmpty.dialogueList.Count - 1; i >= 0; i--)
        {
            dialogueEmptyStack.Push(dialogueEmpty.dialogueList[i]);
        }
        for (int i = dialogueFinish.dialogueList.Count - 1; i >= 0; i--)
        {
            dialogueFinishStack.Push(dialogueFinish.dialogueList[i]);
        }
    }

    public void ShowDialogueEmpty()
    {
        if (!isTalking)
        {
            if (dialogueEmptyStack.Count == 0)
            {
                EventHandler.CallShowDialogueEvent(string.Empty);
                FillDialogueStack();
                EventHandler.CallGameStateChangeEvent(GameState.GamePlay); // 恢复游戏状态
            }
            else
            {
                StartCoroutine(DialogueRoutine(dialogueEmptyStack));
            }
        }
    }

    public void ShowDialogueFihish()
    {
        if (!isTalking)
        {
            if (dialogueFinishStack.Count == 0)
            {
                EventHandler.CallShowDialogueEvent(string.Empty);
                FillDialogueStack();
                EventHandler.CallGameStateChangeEvent(GameState.GamePlay); // 恢复游戏状态
            }
            else
            {
                StartCoroutine(DialogueRoutine(dialogueFinishStack));
            }
        }
    }

    private IEnumerator DialogueRoutine(Stack<string> data)
    {
        isTalking = true;
        EventHandler.CallGameStateChangeEvent(GameState.Pause); // 暂停游戏状态
        if (data.TryPop(out string result))
        {
            EventHandler.CallShowDialogueEvent(result);
            yield return null;
            isTalking = false;
        }
        else
        {
            EventHandler.CallShowDialogueEvent(string.Empty);
            isTalking = false;
            EventHandler.CallGameStateChangeEvent(GameState.GamePlay); // 恢复游戏状态
        }
    }
}



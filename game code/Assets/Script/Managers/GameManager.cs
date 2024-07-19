using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Dictionary<string, bool> miniGameStateDict = new Dictionary<string, bool>();

    private void OnEnable()
    {
        EventHandler.AfterSceneloadEvent += OnAfterSceneLoadedEvent;
        EventHandler.GamePassEvent += OnGamePassEvent;
    }

    private void OnDisable()
    {
        EventHandler.AfterSceneloadEvent -= OnAfterSceneLoadedEvent;
        EventHandler.GamePassEvent -= OnGamePassEvent;
    }

    private void OnAfterSceneLoadedEvent()
    {
        foreach (var miniGame in FindObjectsOfType<MiniGame>())
        {
            if (miniGameStateDict.TryGetValue(miniGame.gameName , out bool isPass))
            {
                miniGame.isPass = isPass;
                miniGame.UpdateMiniGameState();
            }
        }
    }
    private void OnGamePassEvent(string gameName)
    {
        miniGameStateDict[gameName] = true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyUI : MonoBehaviour
{
    public GameObject stageUI;

    private void OnLevelWasLoaded(int level)
    {
        if (GameManager.Instance)
        {
            stageUI.SetActive(GameManager.PreviousSceneName != "Lobby");
        }

    }

}

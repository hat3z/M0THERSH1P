using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MSV2_GameController : MonoBehaviour
{
    public static MSV2_GameController Instance;

    public enum gameStage { Loading, Menu, Stage1, Stage2, Stage3, StageFinale, EndCredits};
    public gameStage GameStage;

    [Header("Controllers")]
    public MSV2_SpawnController SpawnController;
    public MSV2_WorldController WorldController;
    public MSV2_UIController UIController;

    // PRIVATES
    string currentGameStage;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        SwitchGameStage("Loading");
    }

    public void SwitchGameStage(string _stageName)
    {
        switch (_stageName)
        {
            case "Loading":
                GameStage = gameStage.Loading;
                SpawnController.Create_MainPool();
                break;
            case "Menu":
                GameStage = gameStage.Menu;
                UIController.MainMenuContent.gameObject.SetActive(true);
                break;
            case "Stage1":
                GameStage = gameStage.Stage1;
                UIController.MainMenuContent.gameObject.SetActive(false);
                SpawnController.Spawn_Tier1_StartupEnemies();
                break;
            case "Stage2":
                GameStage = gameStage.Stage2;
                break;
            case "Stage4":
                GameStage = gameStage.Stage3;
                break;
            case "StageFinale":
                GameStage = gameStage.StageFinale;
                break;
        }
    }

}

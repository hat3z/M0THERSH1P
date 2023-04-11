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


    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        if(GameStage == gameStage.Loading)
        {
            SpawnController.Create_MainPool();
        }
    }

}

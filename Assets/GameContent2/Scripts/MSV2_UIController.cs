using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class MSV2_UIController : MonoBehaviour
{
    public static MSV2_UIController instance;

    public MSV2_PlayerController PlayerController;

    [Header("Player")]
    public Image XpLoadingBar;
    public TextMeshProUGUI XpLevelLabel;
    public Image HealthBar;
    void Awake()
    {
        if(instance == null)
        {
            instance = this;    
        }
    }

    void Update()
    {
        XpLevelLabel.text = PlayerController.currentLevel.ToString();
    }

}

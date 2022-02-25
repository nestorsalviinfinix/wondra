using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public Image[] difficultys;
    void Start()
    {
        SetDifficulty(1);
    }

    public void SetDifficulty(int value)
    {
        foreach (var d in difficultys)
        {
            d.gameObject.SetActive(false);
        }
        difficultys[value].gameObject.SetActive(true);
    }
}

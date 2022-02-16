using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[CreateAssetMenu(fileName ="New Card", menuName = "Card")]
public class CardData : ScriptableObject
{

    public string title;
    [TextArea]
    public string description;
    public Sprite artwork;
}

[System.Serializable]
public class CardInformation
{
    public TextMeshProUGUI title, description, count;
    public Image artwork;
}

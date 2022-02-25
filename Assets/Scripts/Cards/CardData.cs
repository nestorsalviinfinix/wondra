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

    public void CopyInformation(CardInformation other)
    {
        title.text = other.title.text;
        description.text = other.description.text;
        count.text = other.count.text;
        artwork.sprite = other.artwork.sprite;
    }
}
[System.Serializable]
public class CardInStore
{
    public int price = 1;
    public string ownerName;
    public CardData cardData;
    public CardInStore()
    {

    }
    public CardInStore(string _ownerName, int _price, CardData _cardData)
    {
        ownerName = _ownerName;
        price = _price;
        cardData = _cardData;
    }
}

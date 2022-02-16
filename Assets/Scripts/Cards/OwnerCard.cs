using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class OwnerCard : MonoBehaviour
{
    private CardData _cardData;

    public Animator anim;
    public ParticleSystem par;
    public int price = 1;
    public CardInformation cardInfo;
    public void SetCardData(CardData cd)
    {
        _cardData = cd;
        PrintDataInCard();
    }
    public CardData GetCardData()
    {
        return _cardData;
    }

    void Awake()
    {
        anim.Rebind();
        anim.speed = 0;
        par.Stop();
    }

    private void PrintDataInCard()
    {
        cardInfo.title.text = _cardData.title;
        cardInfo.description.text = _cardData.description;
        cardInfo.artwork.sprite = _cardData.artwork;
    }

    public void SelectThisCard()
    {
        anim.speed = 1;
        par.Play();
    }
    public void DeselectThisCard()
    {
        anim.Rebind();
        anim.speed = 0;
        par.Stop();
        par.Clear();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using DG.Tweening;

public class BuyMenuManager : MonoBehaviour
{
    public RectTransform fatherCardMenu;
    public OwnerCard cardModel;
    public OwnerCard cardSelection;
    public CardInformation cardPreview;
    public Animator cardBackaround;
    private List<OwnerCard> _cardsInventory = new List<OwnerCard>();
    private int _costCardNumber;
    public ParticleSystem moneyParticles;
    public TextMeshProUGUI costCard;
    public TextMeshProUGUI myMoneyText;
    public TMP_InputField searchCard;
    public Toggle ownerCard;

    void Start()
    {
        DOTween.Init();
        myMoneyText.text = "" + TransportData.myMoney;
        int countTest = Random.Range(20,20);
        for (int i = 0; i < countTest; i++)
        {
            var c = Instantiate(cardModel, fatherCardMenu);
            c.SetCardData(RandomTest());
            c.name = "Card (" + (i + 1) + ")";
            c.price = Random.Range(1, 100);
            c.cardInfo.count.text = "" + c.price;
        }

        foreach (var o in fatherCardMenu.GetComponentsInChildren<OwnerCard>())
        {
            _cardsInventory.Add(o);
            var btn = o.GetComponent<Button>();
            btn.onClick.AddListener(() => cardBackaround.SetTrigger("back"));
            btn.onClick.AddListener(() => SetCardPreview(btn.GetComponent<OwnerCard>()));
        }
        cardSelection = _cardsInventory.First();
        cardSelection.SelectThisCard();
        SetCardPreview();

        float x = (float)countTest / 6f;

        int y = Mathf.CeilToInt(x) - 2;
        if (y > 0)
        {
            fatherCardMenu.offsetMax = new Vector2(0, +48.45f + (+346.06f * y));
            fatherCardMenu.localPosition = Vector3.zero;
        }
        ownerCard.onValueChanged.AddListener(delegate { ChangeOwnerCard(ownerCard.isOn); });
    }
    private CardData RandomTest()
    {
        var cards = Resources.LoadAll<CardData>("Cards");
        var _cardData = cards[UnityEngine.Random.Range(0, cards.Length)];
        return _cardData;
    }
    public void ChangeOwnerCard(bool b)
    {
        if(!b)
        {
            foreach (var card in _cardsInventory)
            {
                card.gameObject.SetActive(false);
            }
            string[] owners = TransportData.GetCardsDataBase().Select(x => x.data.title).ToArray();
            var lazzy = _cardsInventory.Where(x =>
            {
                foreach (var o in owners)
                {
                    if (o.ToLower() == x.cardInfo.title.text.ToLower())
                    {
                        x.gameObject.SetActive(true);
                        return true;
                    }
                }
                x.gameObject.SetActive(false);
                return false;
            })
                .Select(x => x.GetComponent<IFiltrable>())
                ;
            foreach (var item in SearchFilter.FilterCollection(searchCard.text,lazzy.ToList()))
            {
                item.SetShowObject(true);
            }
        }else
        {
            SearchFilter.FilterCollection(searchCard.text,
                                      _cardsInventory
                                          .Select(x => x.GetComponent<IFiltrable>())
                                          .ToList()
                                      );
        }
    }
    public void SearchCard()
    {
        int maxCount = 0;
        ChangeOwnerCard(ownerCard.isOn);
        //SearchFilter.FilterCollection(  searchCard.text, 
        //                                _cardsInventory
        //                                    .Select(x=>x.GetComponent<IFiltrable>())
        //                                    .ToList()
        //                                );
        float x = (float)maxCount / 6f;

        int y = Mathf.CeilToInt(x);
        if (y > 0)
        {
            fatherCardMenu.offsetMin = Vector2.zero;
            fatherCardMenu.offsetMax = new Vector2(0, +48.45f + (+346.06f * y));
            fatherCardMenu.localPosition = Vector3.zero;
        }
    }

    public void BuyCard()
    {
        if(TransportData.myMoney < cardSelection.price)
        {
            myMoneyText.transform.DOShakePosition(1.5f, new Vector3(15, 0, 0), 30, 0, false, true);
            Color oldColor = new Color(0.9058824f, 0.7333333f, 0.3882353f);
            myMoneyText.color = Color.red;
            myMoneyText.DOBlendableColor(oldColor,2.5f);
            return;
        }

        HistoryCardDataBase hc = new HistoryCardDataBase();
        hc.nameCard = cardSelection.cardInfo.title.text;
        hc.SetDate(System.DateTime.Now.Day, System.DateTime.Now.Month,System.DateTime.Now.Year);
        hc.cost = cardSelection.price;
        hc.wasBuyed = true;
        TransportData.historyCards.Add(hc);

        moneyParticles.Play();
        _cardsInventory.Remove(cardSelection);
        TransportData.myMoney -= cardSelection.price;
        myMoneyText.text = "" + TransportData.myMoney;
        TransportData.AddCardInDatabase(cardSelection.cardInfo.title.text, cardSelection.GetCardData());
        Destroy(cardSelection.gameObject);
        cardSelection = _cardsInventory.First();
        cardSelection.SelectThisCard();
        cardBackaround.SetTrigger("back");
        SetCardPreview(cardSelection);

        ChangeOwnerCard(ownerCard.isOn);
    }
    public void SetCardPreview(OwnerCard oc)
    {
        cardSelection.DeselectThisCard();
        cardSelection = oc;
        cardSelection.SelectThisCard();
        Invoke(nameof(SetCardPreview), .25f);
    }
    private void SetCardPreview()
    {
        cardPreview.title.text = cardSelection.cardInfo.title.text;
        cardPreview.description.text = cardSelection.cardInfo.description.text;
        cardPreview.artwork.sprite = cardSelection.cardInfo.artwork.sprite;
        _costCardNumber = cardSelection.price;
        costCard.text = "" +_costCardNumber;
    }
}

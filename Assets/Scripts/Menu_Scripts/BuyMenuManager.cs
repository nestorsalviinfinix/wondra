using DG.Tweening;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuyMenuManager : MonoBehaviour
{
    [Header("SelectCardMenu")]
    public RectTransform[] fatherMenus;
    public CardFilterMenu cardFilterModel;
    public RectTransform[] objectMenu_1;
    [Header("BuyCards")]
    public OwnerCard cardModel;
    public OwnerCard cardSelection;
    public CardInformation cardPreview;
    public Animator cardBackaround;
    private List<OwnerCard> _cardsInventory = new List<OwnerCard>();
    private int _costCardNumber;
    public ParticleSystem moneyParticles;
    public TextMeshProUGUI costCard;
    public TextMeshProUGUI myMoneyText, sellingFor;
    public TMP_InputField searchCard;
    public Toggle ownerCard;
    [Header("Meta Data")]
    private List<CardInStore> _allCardStock = new List<CardInStore>();

    void Start()
    {
        DOTween.Init();
        CreateFakeStore();
        AccomodateMenus();
        InitialFilterCard();
    }
    #region test
    private void CreateFakeStore()
    {
        List<CardInStore> fakeList = new List<CardInStore>();
        int countTest = Random.Range(10, 60);
        for (int i = 0; i < countTest; i++)
        {
            CardInStore c = new CardInStore();
            c.cardData = RandomTest();
            c.price = Random.Range(1, 100);
            fakeList.Add(c);
        }
        SetAllCardStock(fakeList);
    }
    public void SetAllCardStock(List<CardInStore> cardD)
    {
        foreach (var c in cardD)
        {
            _allCardStock.Add(c);
        }
    }
    private CardData RandomTest()
    {
        var cards = Resources.LoadAll<CardData>("Cards");
        var _cardData = cards[UnityEngine.Random.Range(0, cards.Length)];
        return _cardData;
    }
    #endregion
    private void AccomodateMenus()
    {
        for (int i = 0; i < fatherMenus.Length; i++)
        {
            fatherMenus[i].DOLocalMoveX(2000 * i, .1f, true);
        }
        foreach (var obj in objectMenu_1)
        {
            obj.DOScaleX(0, .01f);
        }
    }
    private void ShowItems(bool b)
    {
        foreach (var obj in objectMenu_1)
        {
            obj.DOScaleX(b ? 1 : 0, .35f);
        }
    }

    public void SwitchMenu(int currentMenu)
    {
        for (int i = 0; i < fatherMenus.Length; i++)
        {
            fatherMenus[i].DOLocalMoveX(2000 * (i - currentMenu), .6f, true);
            fatherMenus[i].DOScaleX(i == currentMenu ? 1 : 0, .35f);
        }
        ShowItems(currentMenu ==0 ?false:true);
    }
    private void InitialFilterCard()
    {
        CardData[] cards = Resources.LoadAll<CardData>("Cards");

        foreach (var c in cards)
        {
            CardFilterMenu card = Instantiate(cardFilterModel, fatherMenus[0]);
            card.artWork.sprite = c.artwork;
            card.title.text = c.title;
            card.description.text = c.description;
            card.GetComponent<Button>().onClick.AddListener(()=> SwitchMenu(1));
            card.GetComponent<Button>().onClick.AddListener(() => SelectCard(c.title));
            card.name = "Filter - Card - " + c.title;
        }
        cardFilterModel.GetComponent<Button>().onClick.AddListener(() => SwitchMenu(1));
        cardFilterModel.GetComponent<Button>().onClick.AddListener(() => SelectCard("null"));
    }
    private void SelectCard(string n)
    {
        if(n == "null")
        {
            CreateCardBuyMenu(_allCardStock);
            return;
        }
        List<CardInStore> filter = new List<CardInStore>();
        filter = _allCardStock.Where(x => x.cardData.title.Contains(n)).ToList();
        Debug.Log("Card Search: " + n + " - " + filter.Count());
        CreateCardBuyMenu(filter);
    }

    private void CreateCardBuyMenu(List<CardInStore> listCards)
    {
        _cardsInventory.Clear();
        var objs = fatherMenus[1].GetComponentsInChildren<OwnerCard>();
        foreach (var d in objs)
        {
            Destroy(d.gameObject);
        }

        myMoneyText.text = "" + TransportData.myMoney;
        foreach (var o in listCards)
        {
            OwnerCard card = Instantiate(cardModel, fatherMenus[1]);
            _cardsInventory.Add(card);
            card.cardInfo.artwork.sprite = o.cardData.artwork;
            card.cardInfo.title.text = o.cardData.title;
            card.cardInfo.description.text = o.cardData.description;
            card.cardInfo.count.text = o.price.ToString();
            card.price = o.price;
            card.myStoreReferent = o;
            string randomName = new GenerateRandomName().Generate();
            card.myStoreReferent.ownerName = randomName;
            card.SetCardData(o.cardData);

            var btn = card.GetComponent<Button>();
            btn.onClick.AddListener(() => cardBackaround.SetTrigger("back"));
            btn.onClick.AddListener(() => SetCardPreview(btn.GetComponent<OwnerCard>()));
        }

        if(_cardsInventory.Count() > 0)
        {
            cardSelection = _cardsInventory.First();
            cardSelection.SelectThisCard();
            SetCardPreview();
            int countCards = listCards.Count();

            float x = (float)countCards / 6f;

            int y = Mathf.CeilToInt(x) - 2;
            Debug.Log("fatherMenus[1].offsetMax " + fatherMenus[1].offsetMax);
            if (y > 0)
            {
                //fatherMenus[1].SetInsetAndSizeFromParentEdge(RectTransform.Edge.Bottom, 0, 0);
                //fatherMenus[1].SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, 0, 0);
                //fatherMenus[1].SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, 0, 0);
                //fatherMenus[1].SetInsetAndSizeFromParentEdge(RectTransform.Edge.Right, 0, 0);
                //fatherMenus[1].offsetMin = Vector2.zero;
                fatherMenus[1].localPosition = Vector3.zero;
                fatherMenus[1].offsetMax = new Vector2(0, +48.45f + (+346.06f * y));
                //fatherMenus[1].localPosition = Vector3.zero;
            }
            ownerCard.onValueChanged.AddListener(delegate { ChangeOwnerCard(ownerCard.isOn); });
        }else
        {
            cardSelection = null;
            SetCardPreview();
        }
    }
    //Initial Old
    /*
    private void InitialCardBuy()
    {
        myMoneyText.text = "" + TransportData.myMoney;
        foreach (var o in allCardStock)
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
            fatherMenus[1].offsetMax = new Vector2(0, +48.45f + (+346.06f * y));
            fatherMenus[1].localPosition = Vector3.zero;
        }
        ownerCard.onValueChanged.AddListener(delegate { ChangeOwnerCard(ownerCard.isOn); });

    }*/
 
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
                        x.gameObject.SetActive(false);
                        return false;
                    }
                }
                x.gameObject.SetActive(true);
                return true;
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
        ChangeOwnerCard(ownerCard.isOn);
        //SearchFilter.FilterCollection(  searchCard.text, 
        //                                _cardsInventory
        //                                    .Select(x=>x.GetComponent<IFiltrable>())
        //                                    .ToList()
        //                                );
        int maxCount = 0;


        float x = (float)maxCount / 6f;

        int y = Mathf.CeilToInt(x);
        if (y > 0)
        {
            //fatherMenus[1].offsetMin = Vector2.zero;
            
            //fatherMenus[1].rect.x = 0;
            //fatherMenus[1].offsetMax = new Vector2(0, +48.45f + (+346.06f * y));
            //fatherMenus[1].localPosition = Vector3.zero;
        }
    }

    public void BuyCard()
    {
        if (_cardsInventory.Count() <= 0)
            return;
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
        TransportData.AddCardInDatabase(cardSelection.cardInfo.title.text, cardSelection.GetCardData());
        _allCardStock.Remove(cardSelection.myStoreReferent);
        _cardsInventory.Remove(cardSelection);
        TransportData.myMoney -= cardSelection.price;
        myMoneyText.text = "" + TransportData.myMoney;
        Destroy(cardSelection.gameObject);
        if (_cardsInventory.Count() > 0)
        {
            cardSelection = _cardsInventory.First();
            cardSelection.SelectThisCard();
            SetCardPreview(cardSelection);
        }else
        {
            SetCardPreview(null);
        }
        cardBackaround.SetTrigger("back");

        ChangeOwnerCard(ownerCard.isOn);
    }
    public void SetCardPreview(OwnerCard oc)
    {
        cardSelection.DeselectThisCard();
        cardSelection = oc;
        if(oc != null)
        {
            cardSelection.SelectThisCard();
        }
            Invoke(nameof(SetCardPreview), .25f);
    }
    private void SetCardPreview()
    {
        if(cardSelection == null)
        {
            cardBackaround.transform.Find("Card").gameObject.SetActive(false);
            cardBackaround.transform.Find("CostMoney").gameObject.SetActive(false);
            cardBackaround.transform.Find("SellingFor").gameObject.SetActive(false);
        }
        else
        {
            cardBackaround.transform.Find("Card").gameObject.SetActive(true);
            cardBackaround.transform.Find("CostMoney").gameObject.SetActive(true);
            cardBackaround.transform.Find("SellingFor").gameObject.SetActive(true);

            cardPreview.title.text = cardSelection.cardInfo.title.text;
            cardPreview.description.text = cardSelection.cardInfo.description.text;
            cardPreview.artwork.sprite = cardSelection.cardInfo.artwork.sprite;
            _costCardNumber = cardSelection.price;
            costCard.text = "" + _costCardNumber;
            sellingFor.text = cardSelection.myStoreReferent.ownerName;
        }
    }
}

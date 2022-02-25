using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using TMPro;

public class ArmoryManager : MonoBehaviour
{
    [Header("ArmoryScroll")]
    public RectTransform father;
    public OwnerCard cardModel;
    private string[] _namePieces = {"Pawn", "Tower", "Knight", "Bishop","Queen","King" };
    public Sprite[] imagesPieces;
    public GameObject[] animationPieces;
    private Dictionary<string, OwnerCard> _cardsInventory = new Dictionary<string, OwnerCard>();
    [Header("Preview Card")]
    public Animator leftMenuAnim;
    private OwnerCard _cardSelect;
    public CardInformation cardInfo;
    public GameObject cardRightObj;
    public CardInformation cardRight;
    public CardInformation cardPreview;
    public TextMeshProUGUI namePiece;
    public Image imagePiece;
    [Header("Select Piece")]
    private int _indexPiece = 0;
    public GameObject previewCardObj;
    public RectTransform selectParticle;
    public RectTransform[] piecesPos;
    public TextMeshProUGUI myMoney;

    [Header("Search")]
    public TMP_InputField searchCard;
    [Header("")]
    [Header("Selling Card")]
    public GameObject sellMenu;
    public TMP_InputField priceInput;
    public CardInformation cardSelling;
    public ParticleSystem moneyRain;

    private void Start()
    {
        //int countTest = Random.Range(6, 20); // TEST
        myMoney.text = TransportData.myMoney.ToString();
        SelectPiece(0);
        int countTest = TransportData.GetCardsDataBase().Count();
        if(countTest > 0)
        {
            foreach (var c in TransportData.GetCardsDataBase())
            {
                var card = Instantiate(cardModel, father);
                card.SetCardData(c.data);
                card.price = c.count;
            }
            foreach (var o in father.GetComponentsInChildren<OwnerCard>())
            {
                if (_cardsInventory.ContainsKey(o.cardInfo.title.text))
                {
                    Destroy(o.gameObject);
                    countTest--;
                }
                else
                {
                    _cardsInventory.Add(o.cardInfo.title.text, o);
                    o.cardInfo.count.text = o.price.ToString();
                    var btn = o.GetComponent<Button>();
                    btn.onClick.AddListener(() => leftMenuAnim.SetBool("show", true));
                    btn.onClick.AddListener(() => SetLeftMenu(btn.GetComponent<OwnerCard>()));
                }
            }
            _cardSelect = _cardsInventory.FirstOrDefault().Value;
            if (_cardSelect == null)
            {
                return;
            }
            //_cardSelect.SelectThisCard();
            //SetLeftMenu(_cardSelect);
            //SetPreview();
            float x = (float)countTest / 6f;

            int y = Mathf.CeilToInt(x) - 2;
            if (y > 0)
            {
                father.offsetMax = new Vector2(0, +48.45f + (+346.06f * y));
                father.localPosition = Vector3.zero;
            }
        }
        OpenSellingMenu(false);
    }

    public void BackLeftMenu()
    {
        leftMenuAnim.SetBool("show", false);
    }
    private void SetLeftMenu(OwnerCard oc)
    { 
        if(previewCardObj.activeSelf)
        {
            cardRightObj.SetActive(true);
            cardRight.title.text = cardPreview.title.text;
            cardRight.description.text = cardPreview.description.text;
            cardRight.artwork.sprite = cardPreview.artwork.sprite;
        }
        else
            cardRightObj.SetActive(false);
        if(_cardSelect != null)
            _cardSelect.DeselectThisCard();
        _cardSelect = oc;
        _cardSelect.SelectThisCard();
        CardData cd = oc.GetCardData();
        cardInfo.title.text = cd.title;
        cardInfo.description.text = cd.description;
        cardInfo.artwork.sprite = cd.artwork;
        cardInfo.count.text = oc.price.ToString();
    }
    public void SetPreview()
    {
        var oldCard = TransportData.piecesCard[_indexPiece].GetCard();
        bool deleteOld = false;
        if(!previewCardObj.activeSelf)
        {
            string nameCard = _cardSelect.cardInfo.title.text;
            TransportData.RemoveCardInDataBase(nameCard);
            if (_cardsInventory.ContainsKey(nameCard))
            {
                if (_cardsInventory[nameCard].price > 1)
                {
                    _cardsInventory[nameCard].price--;
                    _cardsInventory[nameCard].cardInfo.count.text = _cardsInventory[nameCard].price.ToString();
                }
                else
                {
                    Destroy(_cardsInventory[nameCard].gameObject);
                    _cardsInventory.Remove(nameCard);
                }
            }
        }else
        {
            deleteOld = true;
            string nameCard = oldCard.title;
            TransportData.AddCardInDatabase(nameCard, oldCard);
            if (_cardsInventory.ContainsKey(nameCard))
            {
                _cardsInventory[nameCard].price++;
                _cardsInventory[nameCard].cardInfo.count.text = _cardsInventory[nameCard].price.ToString();
            }
            else
            {
                var card = Instantiate(cardModel, father);
                card.SetCardData(oldCard);
                card.price = 1;

                _cardsInventory.Add(nameCard, card);
                card.cardInfo.count.text = card.price.ToString();
                var btn = card.GetComponent<Button>();
                btn.onClick.AddListener(() => leftMenuAnim.SetBool("show", true));
                btn.onClick.AddListener(() => SetLeftMenu(btn.GetComponent<OwnerCard>()));
            }
        }
        TransportData.piecesCard[_indexPiece].SetCard(_cardSelect.GetCardData());
        SelectPiece(_indexPiece);
        if(deleteOld)
        {
            string nameCard = _cardSelect.cardInfo.title.text;
            TransportData.RemoveCardInDataBase(nameCard);
            if (_cardsInventory[nameCard].price <= 1)
            {
                Destroy(_cardsInventory[nameCard].gameObject);
                _cardsInventory.Remove(nameCard);
            }else
            {
                _cardsInventory[nameCard].price--;
                _cardsInventory[nameCard].cardInfo.count.text = _cardsInventory[nameCard].price.ToString();
            }
        }

    }
    public void Search()
    {
        var list = _cardsInventory.Select(x => x.Value.GetComponent<IFiltrable>()).ToList();
        if(list.Count() > 0)
            SearchFilter.FilterCollection(searchCard.text, list);
    }
    public void SelectPiece(int index)
    {
        _indexPiece = index;
        selectParticle.position = piecesPos[index].position;
        namePiece.text = _namePieces[index];
        imagePiece.sprite = imagesPieces[index];
        foreach (var ap in animationPieces)
        {
            ap.SetActive(false);
        }
        animationPieces[index].SetActive(true);
        CardData cd = TransportData.piecesCard[index].GetCard();
        if(cd == null)
        {
            previewCardObj.SetActive(false);
        }
        else
        {
            previewCardObj.SetActive(true);
            cardPreview.title.text = cd.title;
            cardPreview.description.text = cd.description;
            cardPreview.artwork.sprite = cd.artwork;
        }
    }
    #region SellMenu
    public void SetPriceCard()
    {
        int.TryParse(priceInput.text, out int number);
        string result = Mathf.Clamp(number, 1, 99999).ToString();
        priceInput.text = result;
    }
    public void OpenSellingMenu(bool b)
    {
        sellMenu.SetActive(b);
        if(b)
        {
            cardSelling.artwork.sprite = cardInfo.artwork.sprite;
            cardSelling.count.text = cardInfo.count.text;
            cardSelling.title.text = cardInfo.title.text;
            cardSelling.description.text = cardInfo.description.text;
        }
    }
    public void SellCard()
    {
        BackLeftMenu();
        OpenSellingMenu(false);
        int.TryParse(priceInput.text, out int price);
        CardData auxData = new CardData();
        auxData.artwork = cardSelling.artwork.sprite;
        auxData.title = cardSelling.title.text;
        auxData.description = cardSelling.description.text;
        CardInStore card = new CardInStore(cardSelling.title.text, price, auxData);
        TransportData.cardInStore.Add(card);
        TransportData.RemoveCardInDataBase(cardSelling.title.text);
        moneyRain.Play();
        RemoveCardInventory(cardSelling.title.text);
    }
    private void RemoveCardInventory(string nameCardRemoved)
    {
        if(_cardsInventory.ContainsKey(nameCardRemoved))
        {
            if(_cardsInventory[nameCardRemoved].price > 1)
            {
                _cardsInventory[nameCardRemoved].price--;
                _cardsInventory[nameCardRemoved].cardInfo.count.text = _cardsInventory[nameCardRemoved].price.ToString();
            }else
            {
                Destroy(_cardsInventory[nameCardRemoved].gameObject);
                _cardsInventory.Remove(nameCardRemoved);
            }
        }
    }
    #endregion
}

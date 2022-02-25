using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class SellMenuManager : MonoBehaviour
{
    private OwnerCard _selectData = new OwnerCard();
    [Header("MyCards")]
    public Transform myCardPanel;
    public OwnerCard modelMyCard;
    private List<OwnerCard> _myCards = new List<OwnerCard>();
    public GameObject myCardSelling;
    public CardInformation myCardShow;
    public TMP_InputField priceInput;
    [Header("SellingCards")]
    public Transform sellingCardPanel;
    public OwnerCard modelSelling;
    public GameObject sellingCardMenu;
    public CardInformation sellingShow;
    public TMP_InputField sellingPrice;
    void Start()
    {
        CreateMyCards();
        CreateSellingCards();
        myCardSelling.SetActive(false);
        sellingCardMenu.SetActive(false);
    }

    private void CreateSellingCards()
    {
        foreach (var item in TransportData.cardInStore)
        {
            CreateSellingCard(item);
        }
    }
    private void CreateSellingCard(CardInStore storeCard)
    {
        OwnerCard card = Instantiate(modelSelling, sellingCardPanel);
        card.gameObject.SetActive(true);
        _myCards.Add(card);
        card.cardInfo.count.text = storeCard.price.ToString();
        card.SetCardData(storeCard.cardData);
        card.GetComponent<Button>().onClick.AddListener(() => _selectData = card);
        card.GetComponent<Button>().onClick.AddListener(() => OpenSellingMenu(true));
    }
    public void OpenSellingMenu(bool b)
    {
        sellingCardMenu.SetActive(b);
        sellingShow.CopyInformation(_selectData.cardInfo);
    }
    public void OutSaleCard()
    {
        Debug.Log("Intento quitar de la venta: " + _selectData.cardInfo.title.text);
        Debug.Log("SelectData: " + _selectData);
        sellingCardMenu.gameObject.SetActive(false);
        TransportData.AddCardInDatabase(_selectData.cardInfo.title.text, _selectData.GetCardData());
        TransportData.cardInStore.Remove(_selectData.myStoreReferent);
        Debug.Log("Busca a ver si encuentra: " + TransportData.GetCard(_selectData.cardInfo.title.text));
        if (TransportData.GetCard(_selectData.cardInfo.title.text) == null)
        {
            Debug.Log("No encontro nada y debe crearlo");
            CardDataBase cardDataBase = new CardDataBase();
            cardDataBase.count = 1;
            cardDataBase.title = _selectData.cardInfo.title.text;
            cardDataBase.data = _selectData.GetCardData();
            CreateMyCard(cardDataBase);
        } else
        {
            Debug.Log("Encontro algo y quiere modificarlo");
            OwnerCard result = _myCards.Where(x => x.cardInfo.title.text == _selectData.cardInfo.title.text).FirstOrDefault();
            int.TryParse(result.cardInfo.count.text, out int r);
            r++;
            result.cardInfo.count.text = r.ToString();
        }
        Destroy(_selectData.gameObject);
    }
    public void ChancePrice()
    {
        _selectData.cardInfo.count.text = sellingPrice.text;
        int.TryParse(sellingPrice.text, out int price);
        _selectData.price = price;
        sellingShow.count.text = price.ToString();

    }
    private void CreateMyCards()
    {
        foreach (var item in TransportData.GetCardsDataBase())
        {
            CreateMyCard(item);
        }
    }
    private void CreateMyCard(CardDataBase item)
    {
        OwnerCard card = Instantiate(modelMyCard, myCardPanel);
        card.gameObject.SetActive(true);
        _myCards.Add(card);
        card.cardInfo.count.text = item.count.ToString();
        card.SetCardData(item.data);
        card.GetComponent<Button>().onClick.AddListener(() => _selectData = card);
        card.GetComponent<Button>().onClick.AddListener(() => OpenMyCardSell(true));
    }
    public void OpenMyCardSell(bool b)
    {
        myCardSelling.SetActive(b);
        myCardShow.CopyInformation(_selectData.cardInfo);
    }
    public void PutCardUpForSale()
    {
        OpenMyCardSell(false);
        CardInStore sellingCard = new CardInStore();
        sellingCard.cardData = _selectData.GetCardData();
        int.TryParse(priceInput.text, out int p);
        sellingCard.price = p;
        sellingCard.ownerName = TransportData.namePlayer;
        TransportData.cardInStore.Add(sellingCard);
        TransportData.RemoveCardInDataBase(_selectData.GetCardData().title);
        _myCards.Remove(_selectData);
        Debug.Log("Price, que deberia ser la cantidad: " + _selectData.price);
        int.TryParse(_selectData.cardInfo.count.text, out int count);
        if(count <= 1)
            Destroy(_selectData.gameObject);
        else
        {
            count--;
            _selectData.cardInfo.count.text = count.ToString();
        }
        CreateSellingCard(sellingCard);
    }
    void Update()
    {
        
    }
}

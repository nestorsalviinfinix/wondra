using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System.IO;
using System.Linq;
using UnityEngine.Networking;

public static class TransportData
{
    public static readonly string webServer = "wondrachess.xanthops.com/api/v1";
    public static float currentBarInfo;
    public static string namePlayer = "Player" + Random.Range(1,1000000);
    public static bool isConnect = false;
    public static string access_token;
    public static int myMoney = 999;
    public static List<CardDataBase> _cardsDataBase = new List<CardDataBase>();
    public static List<HistoryCardDataBase> historyCards = new List<HistoryCardDataBase>();
    public static List<CardInStore> cardInStore = new List<CardInStore>();
    public static PiecesWithCard[] piecesCard = { 
                                                new PiecesWithCard(0),
                                                new PiecesWithCard(1),
                                                new PiecesWithCard(2),
                                                new PiecesWithCard(3),
                                                new PiecesWithCard(4),
                                                new PiecesWithCard(5)
                                                };

    public static void AddCardInDatabase(string cardName, CardData cd)
    {
        if (!ExistNameInList(cardName))
            return;

        if (_cardsDataBase.Exists(x => x.title == cardName))
        {
            _cardsDataBase.Find(x => x.title == cardName).count++;
        }else
        {
            CardDataBase cdb = new CardDataBase();
            cdb.title = cardName;
            cdb.data = cd;
            cdb.count = 1;
            _cardsDataBase.Add(cdb);
        }
    }
    public static void RemoveCardInDataBase(string cardName)
    {
        if (!ExistNameInList(cardName))
            return;
        if (_cardsDataBase.Exists(x => x.title == cardName))
        {
            var currentCard = _cardsDataBase.Find(x => x.title == cardName);
            currentCard.count--;
            if(currentCard.count <= 0)
            {
                _cardsDataBase.Remove(currentCard);
            }
        }
        else
        {
            Debug.LogError("***** Se intenta eliminar una carta que no esta en la lista de cartas de database.");
        }
    }
    public static List<CardDataBase> GetCardsDataBase()
    {
        return _cardsDataBase;
    }
    public static CardDataBase GetCard(string cardName)
    {
        var result = _cardsDataBase.Where(x => x.title == cardName).FirstOrDefault();
        return result;
    }

    public static bool ExistNameInList(string cardName)
    {
        var cards = Resources.LoadAll<CardData>("Cards");
        string[] possiblesCardNames = cards.Select(x => x.title).ToArray();
        foreach (var possible in possiblesCardNames)
        {
            if (possible == cardName)
            {
                return true;
            }
        }
        Debug.LogError("*****No se encuentra la carta en la lista de inscriptable objects.");
        return false;
    }
}
[System.Serializable]
public class CardDataBase
{
    public string title = "No name";
    public CardData data;
    public int count = 0;
}
[System.Serializable]
public class HistoryCardDataBase
{
    public string nameCard;
    public string date;
    public int dDay, dMonth, dYear;
    public int cost;
    public bool wasBuyed;

    public void SetDate(int day, int month, int year)
    {
        dDay = day;
        dMonth = month;
        dYear = year;
        date = day.ToString() + "/" + month.ToString() +"/" + year.ToString();
    }
}

[System.Serializable]
public class PiecesWithCard
{
    private string _namePiece;
    private CardData _cardAssociate;
    public PieceStats stats;
    
    public PiecesWithCard(int index)
    {
        PieceForIndex(index);
    }

    public void PieceForIndex(int index)
    {
        Piece.PieceType type = (Piece.PieceType)index;
        _namePiece = type.ToString().ToLower();

        stats = new PieceStats();

        switch (type)
        {
            case Piece.PieceType.PAWN:
                SetStats(3,1,1,1,.1f,.1f);
                break;
            case Piece.PieceType.TOWER:
                SetStats(1,8,3,.25f,0,.15f);
                break;
            case Piece.PieceType.BISHOP:
                SetStats(4,1,2,.65f,.25f,0);
                break;
            case Piece.PieceType.KNIGHT:
                SetStats(3, 4, 5, .33f, .15f, .35f);
                break;
            case Piece.PieceType.QUEEN:
                SetStats(7,0,1,1.6f,.35f,.35f);
                break;
            case Piece.PieceType.KING:
                SetStats(3, 3, 1, 1, .1f, .65f);
                break;
            default:
                break;
        }
    } 
    private void SetStats(int life, int defense, int atk, float atkSpeed,float critical,float block)
    {
        stats.life = life;
        stats.defense = defense;
        stats.attack = atk;
        stats.atkSpeed = atkSpeed;
        stats.critical = critical;
        stats.block = block;
    }
    public string GetPiece()
    {
        return _namePiece;
    }
    public void SetCard(CardData cd)
    {
        _cardAssociate = cd;
    }
    public CardData GetCard()
    {
        return _cardAssociate;
    }
}

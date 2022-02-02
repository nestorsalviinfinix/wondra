using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HistoryManager : MonoBehaviour
{
    public HistoryCard model;
    public RectTransform contentList;
    void Start()
    {
        //int r = Random.Range(22,444);
        //for (int i = 0; i < r; i++)
        //{
        //    HistoryCardDataBase hcdb = new HistoryCardDataBase();
        //    hcdb.nameCard = " No Name ";
        //    hcdb.SetDate(Random.Range(1, 31), Random.Range(1, 13), Random.Range(2015, 2023));
        //    hcdb.cost = Random.Range(1, 10000);
        //    hcdb.wasBuyed = Random.value > .5f;
        //    TransportData.historyCards.Add(hcdb);
        //}
        foreach (var hc in TransportData.historyCards)
        {
            var c = Instantiate(model, contentList);
            c.nameCard.text = hc.nameCard;
            c.cost.text = hc.cost.ToString();
            c.date.text = hc.date;
            SetState(c.state, hc.wasBuyed);
        }
        contentList.offsetMin = Vector2.zero;
        contentList.offsetMax = new Vector2(0, 150 * TransportData.historyCards.Count);
        contentList.localPosition = Vector3.zero;
    }

    private void SetState(TextMeshProUGUI t, bool b)
    {
        t.text = b ? "Buyed" : "Selled";
        t.color = b ? Color.green : Color.yellow;
    }
}

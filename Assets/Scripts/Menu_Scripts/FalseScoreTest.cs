using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FalseScoreTest : MonoBehaviour
{
    [Range(1,999)]
    public int min, max;
    public Transform parent;
    public TextMeshProUGUI model;
    void Start()
    {
        int testCount = Random.Range(min, max);
        for (int i = 0; i < testCount; i++)
        {
            var t = Instantiate(model);
            t.transform.parent = parent;
            t.transform.position = Vector3.zero;
            t.transform.localScale = Vector3.one;
            t.name = "model - " + (i+1);
            int number = (i + 1);
            string numberString = "";
            if (number <= 9)
                numberString = "00" + number;
            else
            if (number <= 99)
                numberString = "0" + number;
            else
                numberString =""+ number;
            t.GetComponent<TextMeshProUGUI>().text = "" + numberString + ": " + RandomMove() + " / " + RandomMove();
        }
    }

    private string RandomMove()
    {
        int r = Random.Range(0,8);
        string letter = "";
        switch (r)
        {
            case 0:
                letter = "A";
                break;
            case 1:
                letter = "B";
                break;
            case 2:
                letter = "C";
                break;
            case 3:
                letter = "D";
                break;
            case 4:
                letter = "E";
                break;
            case 5:
                letter = "F";
                break;
            case 6:
                letter = "G";
                break;
            case 7:
                letter = "H";
                break;
            default:
                break;
        }
        r = Random.Range(0, 8);
        letter += (r + 1);
        return letter;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UserName : MonoBehaviour
{
    void Start()
    {
        GetComponent<TextMeshProUGUI>().text = TransportData.namePlayer;
    }
}

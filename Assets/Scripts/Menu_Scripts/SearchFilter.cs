using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class SearchFilter
{
   public static IEnumerable FilterCollection(string filter, IEnumerable completeCollection)
    {
        List<string> aux = new List<string>();
        if(string.IsNullOrWhiteSpace(filter) || string.IsNullOrEmpty(filter))
        {

        }
        IEnumerable collection = aux;
        return aux;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class SearchFilter
{
   public static List<IFiltrable> FilterCollection(string filter, List<IFiltrable> completeCollection)
    {
        List<IFiltrable> findList = new List<IFiltrable>();
        if(string.IsNullOrWhiteSpace(filter) || string.IsNullOrEmpty(filter))
        {
            foreach (var item in completeCollection)
            {
                item.SetShowObject(true);
            }
            return completeCollection;
        }else
        {
            foreach (var item in completeCollection)
            {
                if(item.GetCodeName().ToLower().Contains(filter.ToLower()))
                {
                    item.SetShowObject(true);
                    findList.Add(item);
                }else
                {
                    item.SetShowObject(false);
                }
            }
        }
        return findList;
    }
}
public interface IFiltrable
{
    public string GetCodeName();
    public void SetShowObject(bool b);
}

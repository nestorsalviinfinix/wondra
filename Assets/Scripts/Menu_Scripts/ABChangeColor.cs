using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ABChangeColor : MonoBehaviour
{
    private enum PartColor { Skin, Clothe,Armor};
    public Image character;
    public Material material;
    public Button[] buttons;

    void Start()
    {
        material = character.material;
        buttons = GetComponentsInChildren<Button>();

        foreach (var b in buttons)
        {
            string partName = b.name;
            PartColor part = PartColor.Skin;
            if (partName == "Clothe")
                part = PartColor.Clothe;
            else if (partName == "Armor")
                part = PartColor.Armor;

            Color color = b.GetComponent<Image>().color;

            b.onClick.AddListener(()=> ChangeColor(part,color));
        }

    }

    private void ChangeColor(PartColor part,Color color)
    {
        print("xadsad");
        material.SetColor("_" + part.ToString(), color);
    }

}

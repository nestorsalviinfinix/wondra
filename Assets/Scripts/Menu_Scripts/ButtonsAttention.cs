using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class ButtonsAttention : MonoBehaviour
{
    public Button[] buttons;
    public bool searchAutomatically = true;
    private float _delayBeetweenPulse = 8;
    private float _difInButtons = .5f;
    private Color _colorButtonAttention;
    void Start()
    {
        DOTween.Init();

        if(searchAutomatically)
        {
            buttons = FindObjectsOfType<Button>();
        }
        _colorButtonAttention = new Color(1f,1f,.75f);
        StartCoroutine(LightButtons());
    }
    IEnumerator LightButtons()
    {
        while(true)
        {
            yield return new WaitForSeconds(_delayBeetweenPulse);

            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].GetComponent<Image>().DOBlendableColor(_colorButtonAttention, .5f);
                buttons[i].transform.DOShakePosition(.5f, new Vector3(3.5f, 0, 0), 15, 0, false, true);
                yield return new WaitForSeconds(_difInButtons);
                buttons[i].GetComponent<Image>().DOBlendableColor(Color.white, .5f);
            }
        }
    }
}

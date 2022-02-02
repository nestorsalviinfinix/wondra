using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonFeedBack : MonoBehaviour
{
    private bool _finishDelay = false;
    private Button _myBtn;
    private EventTrigger _myET;
    void Start()
    {
        DOTween.Init();
        _myBtn = GetComponent<Button>();
        if(_myBtn == null)
        {
            Debug.LogError(name + " no encontro su propio boton y se autodestruira.");
            DestroyImmediate(gameObject);
            return;
        }
        _myBtn.onClick.AddListener(() => ClickButton());
        _myET = gameObject.AddComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerEnter;
        entry.callback.AddListener((data) => { OnPointerDownDelegate((PointerEventData)data); });
        _myET.triggers.Add(entry);

    }
    public void OnPointerDownDelegate(PointerEventData data)
    {
        transform.DOShakePosition(.5f, new Vector3(10, 0, 0), 15, 0, false, true);
    }
    private void ClickButton()
    {
        transform.DOShakePosition(.5f, new Vector3(10, 0, 0), 15, 0, false, true);
        GetComponent<Image>()?.DOColor(new Color(0, 0, 0), .5f);
        GetComponentInChildren<ParticleSystem>()?.Play();
    }

}

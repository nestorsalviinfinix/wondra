using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectCircle : MonoBehaviour
{
    public GameObject sprite;
    public LiveBoardController board;

    public float glowSpeed = 1;

    void Start()
    {
    }

    void Update()
    {
        bool _pieceIsSelected = board.selectedBox?.piece != LivePiece.NullPiece;
        sprite.SetActive(_pieceIsSelected);

        if (!_pieceIsSelected) return;
        UpdateGlow();
    }

    public void UpdateGlow()
    {
        SpriteRenderer circleRenderer = sprite.GetComponent<SpriteRenderer>();
        float c = 0.5f * (Mathf.Sin(Time.time * glowSpeed) + 1);
        circleRenderer.color = new Color(c, c, c, 1);
    }

    public void SetSelectedCircle(LiveBox box)
    {
        gameObject.transform.position = box.transform.position;
    }

    private void OnEnable()
    {
        LiveBoardController.OnUpdateSelectedBox += SetSelectedCircle;
    }
    private void OnDisable()
    {
        LiveBoardController.OnUpdateSelectedBox -= SetSelectedCircle;
    }
}

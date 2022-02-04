using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePiece : MonoBehaviour
{
    // Start is called before the first frame update
    //public PieceType type;
    public CardData card;

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }
}
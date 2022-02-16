using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardNode : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    public void SetColorGround(Color c)
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.color = c;
    }
    void Start()
    {

    }

    void Update()
    {
        
    }
}

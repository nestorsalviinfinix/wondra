using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardNode : MonoBehaviour
{
    private SpriteRenderer _renderer;
    public void SetColorGround(Color c)
    {
        _renderer = GetComponent<SpriteRenderer>();
        _renderer.color = c;
    }
    void Start()
    {

    }

    void Update()
    {
        
    }
}

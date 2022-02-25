using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BoardNode : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    public BoardNode[] neighbors;
    public Vector2 radiusNeighbordsBox = new Vector2(2.3f,1.5f);
    public void SetColorGround(Color c)
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.color = c;
    }
    void Start()
    {
        var colls = Physics2D.OverlapBoxAll(transform.position, radiusNeighbordsBox,0);
        neighbors = colls
                        .Where(c => c.GetComponent<BoardNode>())
                        .Select(c => c.GetComponent<BoardNode>())
                        .Where(b=>b != this)
                        .ToArray();
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, radiusNeighbordsBox);
    }

    void Update()
    {
        
    }
}

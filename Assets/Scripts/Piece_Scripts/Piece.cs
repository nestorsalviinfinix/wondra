using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[RequireComponent(typeof(Collider2D))]
public abstract class Piece : MonoBehaviour
{
    private bool _pick = false;
    public BoardNode myNode;
    void Start()
    {
        var cols = Physics2D.OverlapCircleAll(transform.position, 1)
                        .Where(c => c != GetComponent<Collider2D>())
                        .Where(b => b.GetComponent<BoardNode>())
                        .ToArray();
        myNode = cols[0].GetComponent<BoardNode>();
    }

    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            _pick = false;
            var cols = Physics2D.OverlapCircleAll(transform.position, 1)
                        .Where(c=> c != GetComponent<Collider2D>())
                        .Where(b => b.GetComponent<BoardNode>())
                        .ToArray();
            if(cols.Length > 0)
            {
                Collider2D moreClose = cols[0];
                float distanceClose = Vector2.Distance(transform.position, moreClose.transform.position);
                foreach (var c in cols)
                {
                    float distance = Vector2.Distance(transform.position, c.transform.position);
                    if (distance < distanceClose)
                    {
                        distanceClose = distance;
                        moreClose = c;
                    }
                }
                myNode = moreClose.GetComponent<BoardNode>();
                transform.position = myNode.transform.position;
            }
        }

        if(_pick)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3( mousePos.x,mousePos.y,0);
        }
    }
    private void OnMouseDown()
    {
        _pick = true;
    }


}

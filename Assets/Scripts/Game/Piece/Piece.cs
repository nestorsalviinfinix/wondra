using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

//[RequireComponent(typeof(Collider2D))]
public class Piece : MonoBehaviour
{
    public EChessPieceType PieceType { get; private set; } 
    public ChessBoardBox Box { get; private set; }

    //public GameObject moveLeft;
    //public GameObject moveRight;

    void Start()
    {
        //transform.LookAt(Camera.main.transform.position, -Vector3.up);
        /*
        var cols = Physics2D.OverlapCircleAll(transform.position, 1)
                        .Where(c => c != GetComponent<Collider2D>())
                        .Where(b => b.GetComponent<BoardNode>())
                        .ToArray();
        currentNode = cols[0].GetComponent<BoardNode>();

        gameObject.name += "-" + pieceType.ToString();

        initialPosition = transform.position;
        isDragging = false;*/
        //moveLeft.SetActive(false);
        //moveRight.SetActive(false);
    }

    public void SetBox(ChessBoardBox newBox)
    {
        Box = newBox;
    }

    void Update()
    {
        transform.forward = Camera.main.transform.forward;
        /*if (isDragging)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            transform.Translate(mousePosition);
        }*/
        /*return;
        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
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
                currentNode = moreClose.GetComponent<BoardNode>();
                transform.position = currentNode.transform.position;
            }
        }

        if(isDragging)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3( mousePos.x,mousePos.y,0);
        }*/
    }
    public void OnMouseDown()
    {
        //isDragging = true;
        //moveLeft.SetActive(true);
        //moveRight.SetActive(true);
    }

    public void OnMouseUp()
    {
        //isDragging = false;
        //moveLeft.SetActive(false);
        //moveRight.SetActive(false);
    }
}
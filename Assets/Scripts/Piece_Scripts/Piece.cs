using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

//[RequireComponent(typeof(Collider2D))]
public class Piece : MonoBehaviour
{
    private bool isDragging;// = false;
    public string team;
    public PieceType pieceType;
    public BoardNode currentNode;

    private Animator _anim;

    private Vector2 initialPosition;
    //public GameObject moveLeft;
    //public GameObject moveRight;

    public enum PieceType  {PAWN,TOWER,KNIGHT,BISHOP,QUEEN,KING};

void Start()
    {
        var cols = Physics2D.OverlapCircleAll(transform.position, 1)
                        .Where(c => c != GetComponent<Collider2D>())
                        .Where(b => b.GetComponent<BoardNode>())
                        .OrderBy(b => Vector2.Distance(transform.position, b.transform.position))
                        .ToArray();
        currentNode = cols[0].GetComponent<BoardNode>();
        transform.position = currentNode.transform.position;

        gameObject.name += "-" + pieceType.ToString();

        initialPosition = transform.position;
        isDragging = false;
        //moveLeft.SetActive(false);
        //moveRight.SetActive(false);

        _anim = gameObject.AddComponent<Animator>();
        _anim.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Animation/PiecesAnims/" + dirForPiece());
    }

    private string dirForPiece()
    {
        switch (pieceType)
        {
            case PieceType.PAWN:
                return team == "black"? "BoardPawnBlack" : "BoardPiece";
            case PieceType.BISHOP:
                return team == "black" ? "BoardBishopBlack" : "BoardBishop";
            case PieceType.KING:
                return team == "black" ? "BoardKingBlack" : "BoardKing";
            case PieceType.QUEEN:
                return team == "black" ? "BoardQueenBlack" : "BoardQueen";
            case PieceType.TOWER:
                return team == "black" ? "BoardTowerBlack" : "BoardTower";
            case PieceType.KNIGHT:
                return team == "black" ? "BoardKnightBlack" : "BoardKnight";
            default:
                return "null";
        }
    }
    /*
    void Update()
    {
        if (isDragging)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            transform.Translate(mousePosition);
        }
        //return;
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
        }
    }

    public void OnMouseDown()
    {
        isDragging = true;
        _anim.SetTrigger("select");
        //moveLeft.SetActive(true);
        //moveRight.SetActive(true);
    }

    public void OnMouseUp()
    {
        isDragging = false;
        //moveLeft.SetActive(false);
        //moveRight.SetActive(false);
    }
    */
}
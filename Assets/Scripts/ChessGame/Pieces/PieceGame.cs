using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceGame : MonoBehaviour
{
    public EChessPieceType type;
    public EChessColor color;
    public Animator animator;
    public int moveCount = 0;
    private float speed = 1.5f;

    public IPieceMove canMoves;
    public IPieceMove canAttack;

    public Vector3 destination { get; private set; }

    public void Creation(EChessPieceType myType, EChessColor myColor)
    {
        type = myType;
        color = myColor;
        animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>(
                    "Animation/PiecesAnims/BoardAnims/Board" + (myType.ToString())
                    );
        GetComponent<SpriteRenderer>().color = myColor == EChessColor.White ? Color.white : Color.grey;
        if (myType == EChessPieceType.PAWN)
            transform.localScale *= .75f;

        name = myColor.ToString() + " - " + myType.ToString();
        destination = transform.position;

        FilterMove(myType);
    }
    //En el futuro esta funcion tendra como modificador las cartas.
    private void FilterMove(EChessPieceType myType)
    {
        switch (myType)
        {
            case EChessPieceType.PAWN:
                canMoves = new PawnMoves(color);
                canAttack = new PawnMovesAttack(color);
                speed = 1.35f;
                break;
            case EChessPieceType.BISHOP:
                canMoves = new BishopMoves();
                canAttack = new BishopMoves();
                speed = 2.5f;
                break;
            case EChessPieceType.KING:
                canMoves = new KingMoves();
                canAttack = new KingMoves();
                speed = 1.5f;
                break;
            case EChessPieceType.QUEEN:
                canMoves = new QueenMoves();
                canAttack = new QueenMoves();
                speed = 2.5f;
                break;
            case EChessPieceType.TOWER:
                canMoves = new TowerMoves();
                canAttack = new TowerMoves();
                speed = 3f;
                break;
            case EChessPieceType.KNIGHT:
                canMoves = new KnightMoves();
                canAttack = new KnightMoves();
                speed = 2.7f;
                break;
            case EChessPieceType.NULL:
                break;
            default:
                break;
        }
    }
    public void Select()
    {
        animator.SetTrigger("select");
    }

    public void Movement(Transform pieceSpot)
    {
        moveCount++;
        animator.SetBool("move", true);
        destination = pieceSpot.position;
    }
    private void FinishMove()
    {
        animator.SetBool("move", false);
        transform.position = destination;
        animator.Rebind();
    }

    private void Update()
    {
        if(Vector3.Distance(transform.position, destination) >.03f)
        {
            transform.position = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);
            if (Vector3.Distance(transform.position, destination) <= .03f)
                FinishMove();
        }
    }
}

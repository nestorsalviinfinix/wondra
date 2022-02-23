using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

//[RequireComponent(typeof(Collider2D))]
public class LivePiece : MonoBehaviour
{
    public virtual EChessColor PieceColor { get => Box.Piece.Color; }
    public virtual EChessPieceType PieceType { get => Box.Piece.Type; }
    public ChessBoardBox Box { get; protected set; }
    public static NullPiece NullPiece;

    void Start()
    {
    }

    public void SetBox(ChessBoardBox newBox)
    {
        Box = newBox;
    }

    void Update()
    {
        transform.forward = Camera.main.transform.forward;
    }

}
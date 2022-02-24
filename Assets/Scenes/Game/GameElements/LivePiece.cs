using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

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

    public List<ChessBoardBox> GetLivePossibleMoves()
    {
        if (this == LivePiece.NullPiece) return new List<ChessBoardBox>();
        List<ChessBoardBox> moveList = GetChessPiece().GetChessPossibleMoves();
        return moveList;
    }

    public ChessPiece GetChessPiece()
    {
        return Box.Board.boxes[Box.CoordX, Box.CoordY].Piece;
    }

    void Update()
    {
        transform.forward = Camera.main.transform.forward;
    }
}
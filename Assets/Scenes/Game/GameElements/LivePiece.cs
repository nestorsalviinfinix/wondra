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
    public LiveBoardController liveBoard;
    public static NullPiece NullPiece;



    public void SetBox(ChessBoardBox newBox)
    {
        Box = newBox;
    }

    public List<ChessBoardBox> GetLivePossibleMoves()
    {
        if (this == LivePiece.NullPiece)
            return new List<ChessBoardBox>();
        List<ChessBoardBox> moveList = GetChessPiece().GetChessPossibleMoves();
        return moveList;
    }

    public ChessPiece GetChessPiece()
    {
        return Box.Board.boxes[Box.CoordX, Box.CoordY].Piece;
    }

    internal void ExecuteActionTo(IAction action, LiveBox box)
    {
        int x = Box.CoordX;
        int y = Box.CoordY;

        action.ExecuteAction(Box, Box.Board.boxes[box.CoordX, box.CoordY]);
        box.piece = this;
        liveBoard.boxes[x, y].piece = NullPiece;
        Box = box.chessBox;
        transform.position = liveBoard.boxes[Box.CoordX, box.CoordY].gameObject.transform.position;
        Vector3 pos = transform.position;
        pos.y += .2f;
        pos.z -= .5f;
        transform.position = pos;
    }
    public void Capture()
    {
        //Box.SetPiece(null);
        Destroy(gameObject);
    }
}
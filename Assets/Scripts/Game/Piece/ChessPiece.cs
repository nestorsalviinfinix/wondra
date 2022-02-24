using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessPiece
{
    public ChessPlayer Owner { get; private set; }
    public EChessColor Color { get; private set; }
    public EChessPieceType Type { get; private set; }
    public ChessBoardBox Box { get; set; }

    public static Dictionary<EChessPieceType, IPieceMoveStrategy> moveStrategies;

    public int coordX;
    public int coordY;

    public ChessPiece(EChessPieceType type, EChessColor color)
    {
        Type = type;
        Color = color;
    }
    public ChessPiece(EChessPieceType type, ChessPlayer player)
    {
        Type = type;
        Owner = player;
        Color = player.Color;
    }

    public List<ChessBoardBox> GetChessPossibleMoves()
    {
        List<ChessBoardBox> moveList = (List<ChessBoardBox>)moveStrategies[Type].GetPossibleMoves(this);
        return moveList;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessPiece
{
    public EChessColor Color { get; private set; }
    public EChessPieceType Type { get; private set; }
    public ChessPiece(EChessPieceType type, EChessColor color)
    {
        Type = type;
        Color = color;
    }
}

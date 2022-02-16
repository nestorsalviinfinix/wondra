using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessBoardBox
{
    public ChessPiece Piece { get; private set; }

    public EChessColor Color { get; private set; }
    public char CoordX { get; private set; }
    public int CoordY { get; private set; }

    public ChessBoardBox(int coordX, int coordY, EChessColor color)
    {
        Color = color;
        CoordX = Convert.ToChar(coordY + 97);
        CoordY = coordY + 1;
    }

    public void SetPiece(ChessPiece piece)
    {
        this.Piece = piece;
    }
}

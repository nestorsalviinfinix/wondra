using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessBoardBox
{
    public ChessPiece Piece { get; private set; }

    public EChessColor Color { get; private set; }
    public int CoordX { get; private set; }
    public int CoordY { get; private set; }
    public char ACoordX { get; private set; }
    public int ACoordY { get; private set; }

    public ChessBoard Board { get; set; }

    public ChessBoardBox(int coordX, int coordY, EChessColor color)
    {
        Color = color;
        ACoordX = Convert.ToChar(coordX + 97);
        ACoordY = coordY + 1;

        CoordX = coordX;
        CoordY = coordY;
        //Debug.Log($"Created Box {CoordX}{CoordY}");
    }

    public void SetPiece(ChessPiece piece)
    {
        this.Piece = piece;
        piece.Box = this;
        piece.coordX = CoordX;
        piece.coordY = CoordY;
    }
}

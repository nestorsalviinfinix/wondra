using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessBoard
{
    public int sizeWidth = 8;
    public int sizeHeight = 8;
    public ChessBoardBox[,] boxes;
    public List<ChessPiece> pieces;

    public ChessBoard()
    {
        pieces = new List<ChessPiece>();
        boxes = new ChessBoardBox[sizeWidth, sizeHeight];
        EChessColor currentColor = EChessColor.White;

        for (int x = 0; x < sizeWidth; x++)
        {
            for (int y = 0; y < sizeHeight; y++)
            {
                boxes[x, y] = new ChessBoardBox(x, y, currentColor);
                boxes[x, y].Board = this;
                currentColor = currentColor == EChessColor.White ? EChessColor.Black : EChessColor.White;
            }
            currentColor = currentColor == EChessColor.White ? EChessColor.Black : EChessColor.White;
        }

        ChessPiece.moveStrategies = new Dictionary<EChessPieceType, IPieceMoveStrategy>()
        {
            { EChessPieceType.PAWN, new PawnPossibleMoves() },
            { EChessPieceType.KING, new KingPossibleMoves() },
            { EChessPieceType.QUEEN, new QueenPossibleMoves() },
            { EChessPieceType.KNIGHT, new KnightPossibleMoves() },
            { EChessPieceType.BISHOP, new BishopPossibleMoves() },
            { EChessPieceType.TOWER, new TowerPossibleMoves() },
            { EChessPieceType.NULL, new NullPiecePossibleMoves() },

        };
    }

    public void Fill(Dictionary<int[], ChessPiece> dict)
    {
        foreach(KeyValuePair<int[], ChessPiece> entry in dict)
        {
            int coordX = entry.Key[0];
            int coordY = entry.Key[1];
            ChessPiece piece = entry.Value;

            boxes[coordX, coordY].SetPiece(piece);

            if (piece != null) pieces.Add(piece);
        }
    }

    // method only made for debugging
    public void StandardFill(ChessPlayer[] players)
    {
        Dictionary<int[], ChessPiece> dict = new()
        {
            { new int[]{ 0, 0}, new ChessPiece(EChessPieceType.TOWER, players[1])},
            { new int[]{ 1, 0}, new ChessPiece(EChessPieceType.KNIGHT, players[1])},
            { new int[]{ 2, 0}, new ChessPiece(EChessPieceType.BISHOP, players[1])},
            { new int[]{ 3, 0}, new ChessPiece(EChessPieceType.QUEEN, players[1])},
            //{ new int[]{ 4, 0}, new ChessPiece(EChessPieceType.KING, players[1])},
            { new int[]{ 3, 3}, new ChessPiece(EChessPieceType.KING, players[1])},
            { new int[]{ 5, 0}, new ChessPiece(EChessPieceType.BISHOP, players[1])},
            { new int[]{ 6, 0}, new ChessPiece(EChessPieceType.KNIGHT, players[1])},
            { new int[]{ 7, 0}, new ChessPiece(EChessPieceType.TOWER, players[1])},

            { new int[]{ 0, 1}, new ChessPiece(EChessPieceType.PAWN, players[1])},
            { new int[]{ 1, 1}, new ChessPiece(EChessPieceType.PAWN, players[1])},
            { new int[]{ 2, 1}, new ChessPiece(EChessPieceType.PAWN, players[1])},
            { new int[]{ 3, 1}, new ChessPiece(EChessPieceType.PAWN, players[1])},
            { new int[]{ 4, 1}, new ChessPiece(EChessPieceType.PAWN, players[1])},
            { new int[]{ 5, 1}, new ChessPiece(EChessPieceType.PAWN, players[1])},
            { new int[]{ 6, 1}, new ChessPiece(EChessPieceType.PAWN, players[1])},
            { new int[]{ 7, 1}, new ChessPiece(EChessPieceType.PAWN, players[1])},

            { new int[]{ 0, 7}, new ChessPiece(EChessPieceType.TOWER, players[0])},
            { new int[]{ 1, 7}, new ChessPiece(EChessPieceType.KNIGHT, players[0])},
            { new int[]{ 2, 7}, new ChessPiece(EChessPieceType.BISHOP, players[0])},
            { new int[]{ 3, 7}, new ChessPiece(EChessPieceType.QUEEN, players[0])},
            //{ new int[]{ 4, 7}, new ChessPiece(EChessPieceType.KING, players[0])},
            { new int[]{ 4, 2}, new ChessPiece(EChessPieceType.KING, players[0])},
            { new int[]{ 5, 7}, new ChessPiece(EChessPieceType.BISHOP, players[0])},
            { new int[]{ 6, 7}, new ChessPiece(EChessPieceType.KNIGHT, players[0])},
            { new int[]{ 7, 7}, new ChessPiece(EChessPieceType.TOWER, players[0])},

            { new int[]{ 0, 6}, new ChessPiece(EChessPieceType.PAWN, players[0])},
            { new int[]{ 1, 6}, new ChessPiece(EChessPieceType.PAWN, players[0])},
            { new int[]{ 2, 6}, new ChessPiece(EChessPieceType.PAWN, players[0])},
            { new int[]{ 3, 6}, new ChessPiece(EChessPieceType.PAWN, players[0])},
            { new int[]{ 4, 6}, new ChessPiece(EChessPieceType.PAWN, players[0])},
            { new int[]{ 5, 6}, new ChessPiece(EChessPieceType.PAWN, players[0])},
            { new int[]{ 6, 6}, new ChessPiece(EChessPieceType.PAWN, players[0])},
            { new int[]{ 7, 6}, new ChessPiece(EChessPieceType.PAWN, players[0])},
        };

        Fill(dict);
    }

    public static string ToAlgebraic(Vector2 coords)
    {
        string ret = "";
        ret += (char)(coords.x + 97);
        ret += coords.y + 1;

        return ret;
    }

    public static Vector2 ToVector(string algebraicCoords)
    {
        Vector2 ret = new Vector2();

        ret.x = (int)Char.GetNumericValue(algebraicCoords[0]);
        ret.y = algebraicCoords[1] - 1;

        return ret;
    }

    public string ConvertToString()
    {
        string ret = "";

        for (int x = 0; x < sizeWidth; x++)
        {
            for (int y = 0; y < sizeHeight; y++)
            {
                if (boxes[y, x].Piece is null)
                {
                    ret += "   ;";
                    continue;
                }
                ret += boxes[y, x].Piece.Type.ToString() + "; ";
            }
            ret += "\n";
        }

        return ret;
    }
}

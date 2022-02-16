using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessBoard
{
    public int sizeWidth = 8;
    public int sizeHeight = 8;
    public ChessBoardBox[,] boxes;

    public ChessBoard()
    {
        boxes = new ChessBoardBox[sizeWidth, sizeHeight];
        EChessColor currentColor = EChessColor.White;

        for (int x = 0; x < sizeWidth; x++)
        {
            for (int y = 0; y < sizeHeight; y++)
            {
                boxes[x, y] = new ChessBoardBox(x, y, currentColor);
                currentColor = currentColor == EChessColor.White ? EChessColor.Black : EChessColor.White;
            }
            currentColor = currentColor == EChessColor.White ? EChessColor.Black : EChessColor.White;
        }
    }

    public void Fill(Dictionary<int[], ChessPiece> dict)
    {
        foreach(KeyValuePair<int[], ChessPiece> entry in dict)
        {
            boxes[entry.Key[0], entry.Key[1]].SetPiece(entry.Value);
        }
    }

    // method only made for debugging
    public void StandardFill()
    {
        Dictionary<int[], ChessPiece> dict = new Dictionary<int[], ChessPiece>()
        {
            { new int[]{ 0, 0}, new ChessPiece(EChessPieceType.TOWER, EChessColor.Black)},
            { new int[]{ 1, 0}, new ChessPiece(EChessPieceType.KNIGHT, EChessColor.Black)},
            { new int[]{ 2, 0}, new ChessPiece(EChessPieceType.BISHOP, EChessColor.Black)},
            { new int[]{ 3, 0}, new ChessPiece(EChessPieceType.QUEEN, EChessColor.Black)},
            { new int[]{ 4, 0}, new ChessPiece(EChessPieceType.KING, EChessColor.Black)},
            { new int[]{ 5, 0}, new ChessPiece(EChessPieceType.BISHOP, EChessColor.Black)},
            { new int[]{ 6, 0}, new ChessPiece(EChessPieceType.KNIGHT, EChessColor.Black)},
            { new int[]{ 7, 0}, new ChessPiece(EChessPieceType.TOWER, EChessColor.Black)},

            { new int[]{ 0, 1}, new ChessPiece(EChessPieceType.PAWN, EChessColor.Black)},
            { new int[]{ 1, 1}, new ChessPiece(EChessPieceType.PAWN, EChessColor.Black)},
            { new int[]{ 2, 1}, new ChessPiece(EChessPieceType.PAWN, EChessColor.Black)},
            { new int[]{ 3, 1}, new ChessPiece(EChessPieceType.PAWN, EChessColor.Black)},
            { new int[]{ 4, 1}, new ChessPiece(EChessPieceType.PAWN, EChessColor.Black)},
            { new int[]{ 5, 1}, new ChessPiece(EChessPieceType.PAWN, EChessColor.Black)},
            { new int[]{ 6, 1}, new ChessPiece(EChessPieceType.PAWN, EChessColor.Black)},
            { new int[]{ 7, 1}, new ChessPiece(EChessPieceType.PAWN, EChessColor.Black)},

            { new int[]{ 0, 7}, new ChessPiece(EChessPieceType.TOWER, EChessColor.White)},
            { new int[]{ 1, 7}, new ChessPiece(EChessPieceType.KNIGHT, EChessColor.White)},
            { new int[]{ 2, 7}, new ChessPiece(EChessPieceType.BISHOP, EChessColor.White)},
            { new int[]{ 3, 7}, new ChessPiece(EChessPieceType.QUEEN, EChessColor.White)},
            { new int[]{ 4, 7}, new ChessPiece(EChessPieceType.KING, EChessColor.White)},
            { new int[]{ 5, 7}, new ChessPiece(EChessPieceType.BISHOP, EChessColor.White)},
            { new int[]{ 6, 7}, new ChessPiece(EChessPieceType.KNIGHT, EChessColor.White)},
            { new int[]{ 7, 7}, new ChessPiece(EChessPieceType.TOWER, EChessColor.White)},

            { new int[]{ 0, 6}, new ChessPiece(EChessPieceType.PAWN, EChessColor.White)},
            { new int[]{ 1, 6}, new ChessPiece(EChessPieceType.PAWN, EChessColor.White)},
            { new int[]{ 2, 6}, new ChessPiece(EChessPieceType.PAWN, EChessColor.White)},
            { new int[]{ 3, 6}, new ChessPiece(EChessPieceType.PAWN, EChessColor.White)},
            { new int[]{ 4, 6}, new ChessPiece(EChessPieceType.PAWN, EChessColor.White)},
            { new int[]{ 5, 6}, new ChessPiece(EChessPieceType.PAWN, EChessColor.White)},
            { new int[]{ 6, 6}, new ChessPiece(EChessPieceType.PAWN, EChessColor.White)},
            { new int[]{ 7, 6}, new ChessPiece(EChessPieceType.PAWN, EChessColor.White)},
        };

        Fill(dict);
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingPossibleMoves : IPieceMoveStrategy
{
    public IEnumerable<ChessBoardBox> GetPossibleMoves(ChessPiece piece)
    {
        List<ChessBoardBox> list = new();
        int x = piece.coordX;
        int y = piece.coordY;
        ChessBoard board = piece.Box.Board;
        int maxX = board.sizeWidth - 1;
        int maxY = board.sizeHeight - 1;

        Debug.Log($"coords: ({x}, {y}) | max: ({maxX}, {maxY})");

        List<ChessBoardBox> candidates = new List<ChessBoardBox>();
        if (x - 1 >= 0)                         candidates.Add(board.boxes[x - 1, y]);
        if (x + 1 <= maxX)                      candidates.Add(board.boxes[x + 1, y]);
        if (x - 1 >= 0 && y - 1 >= 0)           candidates.Add(board.boxes[x - 1, y - 1]);
        if (x - 1 >= 0 && y + 1 <= maxY)        candidates.Add(board.boxes[x - 1, y + 1]);
        if (x + 1 <= maxX && y - 1 >= 0)        candidates.Add(board.boxes[x + 1, y - 1]);
        if (x + 1 <= maxX && y + 1 <= maxY)     candidates.Add(board.boxes[x + 1, y + 1]);
        if (y - 1 >= 0)                         candidates.Add(board.boxes[x, y - 1]);
        if (y + 1 <= maxY)                      candidates.Add(board.boxes[x, y + 1]);

        //Debug.Log($"Candidates ({candidates.Count})");

        foreach (ChessBoardBox candidate in candidates)
        {
            Debug.Log($"-- piece: {candidate.CoordX}|{candidate.CoordY}");
            if (candidate.Piece != null) continue;

            list.Add(candidate);
        }
        //Debug.Log($"but list only has {list.Count} elements");

        return list;
    }
}

public class QueenPossibleMoves : IPieceMoveStrategy
{
    public IEnumerable<ChessBoardBox> GetPossibleMoves(ChessPiece piece)
    {
        List<ChessBoardBox> list = new List<ChessBoardBox>();
        return list;
    }
}
public class PawnPossibleMoves : IPieceMoveStrategy
{
    public IEnumerable<ChessBoardBox> GetPossibleMoves(ChessPiece piece)
    {
        List<ChessBoardBox> list = new List<ChessBoardBox>();
        return list;
    }
}
public class TowerPossibleMoves : IPieceMoveStrategy
{
    public IEnumerable<ChessBoardBox> GetPossibleMoves(ChessPiece piece)
    {
        List<ChessBoardBox> list = new List<ChessBoardBox>();
        return list;
    }
}
public class KnightPossibleMoves : IPieceMoveStrategy
{
    public IEnumerable<ChessBoardBox> GetPossibleMoves(ChessPiece piece)
    {
        List<ChessBoardBox> list = new List<ChessBoardBox>();
        return list;
    }
}
public class BishopPossibleMoves : IPieceMoveStrategy
{
    public IEnumerable<ChessBoardBox> GetPossibleMoves(ChessPiece piece)
    {
        List<ChessBoardBox> list = new List<ChessBoardBox>();
        return list;
    }
}

public class NullPiecePossibleMoves : IPieceMoveStrategy
{
    public IEnumerable<ChessBoardBox> GetPossibleMoves(ChessPiece piece)
    {
        List<ChessBoardBox> list = new List<ChessBoardBox>();
        return list;
    }
}
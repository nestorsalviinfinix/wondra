using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
/*
public class KingPossibleMoves : IPieceMoveStrategy
{
    public IEnumerable<ChessBoardBox> GetPossibleMoves(ChessPiece piece)
    {
        List<ChessBoardBox> list = new List<ChessBoardBox>();
        int x = piece.coordX;
        int y = piece.coordY;
        ChessBoard board = piece.Box.Board;
        int maxX = board.sizeWidth - 1;
        int maxY = board.sizeHeight - 1;

        //Debug.Log($"coords: ({x}, {y}) | max: ({maxX}, {maxY})");

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
            //Debug.Log($"-- piece: {candidate.CoordX}|{candidate.CoordY}");
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

        List<ChessBoardBox> towerList = ChessPiece.moveStrategies[EChessPieceType.TOWER]
            .GetPossibleMoves(piece).ToList();

        List<ChessBoardBox> bishopList = ChessPiece.moveStrategies[EChessPieceType.BISHOP]
            .GetPossibleMoves(piece).ToList();

        List<ChessBoardBox> union = towerList.Union(bishopList).ToList();
        return union;
    }
}
public class PawnPossibleMoves : IPieceMoveStrategy
{
    public IEnumerable<ChessBoardBox> GetPossibleMoves(ChessPiece piece)
    {
        List<ChessBoardBox> list = new List<ChessBoardBox>();

        int direction = piece.Color == EChessColor.Black ? -1 : 1;

        int nextY = piece.coordY + direction;
        ChessBoard board = piece.Box.Board;
        int maxY = board.sizeWidth - 1;

        if (nextY < 0 || nextY > maxY) return list;

        ChessBoardBox candidate = board.boxes[piece.coordX, nextY];

        if (candidate.Piece == null)
            list.Add(candidate);

        return list;
    }
}
public class TowerPossibleMoves : IPieceMoveStrategy
{
    public IEnumerable<ChessBoardBox> GetPossibleMoves(ChessPiece piece)
    {
        List<ChessBoardBox> list = new List<ChessBoardBox>();
        int x = piece.coordX;
        int y = piece.coordY;
        ChessBoard board = piece.Box.Board;
        int maxX = board.sizeWidth - 1;
        int maxY = board.sizeHeight - 1;

        ChessBoardBox currentBox;
        int i;
            
        i = x + 1;
        while (i <= maxX)
        {
            currentBox = board.boxes[i, y];
            if (currentBox.Piece != null) break;
            list.Add(currentBox);
            i++;
        }

        i = x - 1;
        while (i >= 0)
        {
            currentBox = board.boxes[i, y];
            if (currentBox.Piece != null) break;
            list.Add(currentBox);
            i--;
        }

        i = y + 1;
        while (i <= maxY)
        {
            currentBox = board.boxes[x, i];
            if (currentBox.Piece != null) break;
            list.Add(currentBox);
            i++;
        }

        i = y - 1;
        while (i >= 0)
        {
            currentBox = board.boxes[x, i];
            if (currentBox.Piece != null) break;
            list.Add(currentBox);
            i--;
        }

        return list;
    }
}
public class KnightPossibleMoves : IPieceMoveStrategy
{
    private int maxX;
    private int maxY;
    private int x;
    private int y;
    private ChessBoard board;

    public IEnumerable<ChessBoardBox> GetPossibleMoves(ChessPiece piece)
    {
        List<ChessBoardBox> list = new List<ChessBoardBox>();

        board = piece.Box.Board;
        x = piece.coordX;
        y = piece.coordY;
        maxX = board.sizeWidth - 1;
        maxY = board.sizeHeight - 1;

        TryAdding(list, -1, -2);
        TryAdding(list, -2, -1);
        TryAdding(list, 1, -2);
        TryAdding(list, 2, -1);
        TryAdding(list, 2, 1);
        TryAdding(list, 1, 2);
        TryAdding(list, -1, 2);
        TryAdding(list, -2, 1);

        return list;
    }

    public void TryAdding(List<ChessBoardBox> _list, int dx, int dy)
    {
        int newX = x + dx;
        int newY = y + dy;
        if (newX < 0 || newX > maxX || newY < 0 || newY > maxY) return;
        ChessBoardBox candidate = board.boxes[newX, newY];
        if (candidate.Piece == null) _list.Add(candidate);
    }
}
public class BishopPossibleMoves : IPieceMoveStrategy
{
    public IEnumerable<ChessBoardBox> GetPossibleMoves(ChessPiece piece)
    {
        List<ChessBoardBox> list = new List<ChessBoardBox>();
        int x = piece.coordX;
        int y = piece.coordY;
        ChessBoard board = piece.Box.Board;
        int maxX = board.sizeWidth - 1;
        int maxY = board.sizeHeight - 1;

        ChessBoardBox currentBox;
        int i;
        int j;

        i = x + 1;
        j = y + 1;
        while (i <= maxX && j <= maxY)
        {
            currentBox = board.boxes[i, j];
            if (currentBox.Piece != null) break;
            list.Add(currentBox);
            i++;
            j++;
        }

        i = x - 1;
        j = y + 1;
        while (i >= 0 && j <= maxY)
        {
            currentBox = board.boxes[i, j];
            if (currentBox.Piece != null) break;
            list.Add(currentBox);
            i--;
            j++;
        }

        i = x - 1;
        j = y - 1;
        while (i >= 0 && j >= 0)
        {
            currentBox = board.boxes[i, j];
            if (currentBox.Piece != null) break;
            list.Add(currentBox);
            i--;
            j--;
        }

        i = x + 1;
        j = y - 1;
        while (i <= maxX && j >= 0)
        {
            currentBox = board.boxes[i, j];
            if (currentBox.Piece != null) break;
            list.Add(currentBox);
            i++;
            j--;
        }

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
*/ // Funciona pero no pueden comer.

public class KingPossibleMoves : IPieceMoveStrategy
{
    public IEnumerable<ChessBoardBox> GetPossibleMoves(ChessPiece piece)
    {
        List<ChessBoardBox> list = new List<ChessBoardBox>();
        int x = piece.coordX;
        int y = piece.coordY;
        ChessBoard board = piece.Box.Board;
        int maxX = board.sizeWidth - 1;
        int maxY = board.sizeHeight - 1;

        //Debug.Log($"coords: ({x}, {y}) | max: ({maxX}, {maxY})");

        List<ChessBoardBox> candidates = new List<ChessBoardBox>();
        if (x - 1 >= 0) candidates.Add(board.boxes[x - 1, y]);
        if (x + 1 <= maxX) candidates.Add(board.boxes[x + 1, y]);
        if (x - 1 >= 0 && y - 1 >= 0) candidates.Add(board.boxes[x - 1, y - 1]);
        if (x - 1 >= 0 && y + 1 <= maxY) candidates.Add(board.boxes[x - 1, y + 1]);
        if (x + 1 <= maxX && y - 1 >= 0) candidates.Add(board.boxes[x + 1, y - 1]);
        if (x + 1 <= maxX && y + 1 <= maxY) candidates.Add(board.boxes[x + 1, y + 1]);
        if (y - 1 >= 0) candidates.Add(board.boxes[x, y - 1]);
        if (y + 1 <= maxY) candidates.Add(board.boxes[x, y + 1]);

        //Debug.Log($"Candidates ({candidates.Count})");

        foreach (ChessBoardBox candidate in candidates)
        {
            //Debug.Log($"-- piece: {candidate.CoordX}|{candidate.CoordY}");
            //if (candidate.Piece != null) continue;

            if (candidate.Piece != null && candidate.Piece.Color == piece.Color) continue;

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

        List<ChessBoardBox> towerList = ChessPiece.moveStrategies[EChessPieceType.TOWER]
            .GetPossibleMoves(piece).ToList();

        List<ChessBoardBox> bishopList = ChessPiece.moveStrategies[EChessPieceType.BISHOP]
            .GetPossibleMoves(piece).ToList();

        List<ChessBoardBox> union = towerList.Union(bishopList).ToList();
        return union;
    }
}
public class PawnPossibleMoves : IPieceMoveStrategy
{
    public IEnumerable<ChessBoardBox> GetPossibleMoves(ChessPiece piece)
    {
        List<ChessBoardBox> list = new List<ChessBoardBox>();

        int direction = piece.Color == EChessColor.Black ? -1 : 1;

        int nextY = piece.coordY + direction;
        ChessBoard board = piece.Box.Board;
        int maxY = board.sizeWidth - 1;

        if (nextY < 0 || nextY > maxY) return list;

        ChessBoardBox candidate = board.boxes[piece.coordX, nextY];

        if (candidate.Piece == null)
            list.Add(candidate);

        return list;
    }
}
public class TowerPossibleMoves : IPieceMoveStrategy
{
    public IEnumerable<ChessBoardBox> GetPossibleMoves(ChessPiece piece)
    {
        List<ChessBoardBox> list = new List<ChessBoardBox>();
        int x = piece.coordX;
        int y = piece.coordY;
        ChessBoard board = piece.Box.Board;
        int maxX = board.sizeWidth - 1;
        int maxY = board.sizeHeight - 1;

        ChessBoardBox currentBox;
        int i;

        i = x + 1;
        while (i <= maxX)
        {
            currentBox = board.boxes[i, y];
            if (currentBox.Piece != null && currentBox.Piece.Color == piece.Color) break;
            list.Add(currentBox);
            i++;
            if (currentBox.Piece != null) break;
        }

        i = x - 1;
        while (i >= 0)
        {
            currentBox = board.boxes[i, y];
            if (currentBox.Piece != null && currentBox.Piece.Color == piece.Color) break;
            list.Add(currentBox);
            i--;
            if (currentBox.Piece != null) break;
        }

        i = y + 1;
        while (i <= maxY)
        {
            currentBox = board.boxes[x, i];
            if (currentBox.Piece != null && currentBox.Piece.Color == piece.Color) break;
            list.Add(currentBox);
            i++;
            if (currentBox.Piece != null) break;
        }

        i = y - 1;
        while (i >= 0)
        {
            currentBox = board.boxes[x, i];
            if (currentBox.Piece != null && currentBox.Piece.Color == piece.Color) break;
            list.Add(currentBox);
            i--;
            if (currentBox.Piece != null) break;
        }

        return list;
    }
}
public class KnightPossibleMoves : IPieceMoveStrategy
{
    private int maxX;
    private int maxY;
    private int x;
    private int y;
    private ChessBoard board;

    public IEnumerable<ChessBoardBox> GetPossibleMoves(ChessPiece piece)
    {
        List<ChessBoardBox> list = new List<ChessBoardBox>();

        board = piece.Box.Board;
        x = piece.coordX;
        y = piece.coordY;
        maxX = board.sizeWidth - 1;
        maxY = board.sizeHeight - 1;

        EChessColor pieceColor = piece.Color;

        TryAdding(list, -1, -2,pieceColor);
        TryAdding(list, -2, -1, pieceColor);
        TryAdding(list, 1, -2, pieceColor);
        TryAdding(list, 2, -1, pieceColor);
        TryAdding(list, 2, 1, pieceColor);
        TryAdding(list, 1, 2, pieceColor);
        TryAdding(list, -1, 2, pieceColor);
        TryAdding(list, -2, 1, pieceColor);

        return list;
    }

    public void TryAdding(List<ChessBoardBox> _list, int dx, int dy, EChessColor color)
    {
        int newX = x + dx;
        int newY = y + dy;
        if (newX < 0 || newX > maxX || newY < 0 || newY > maxY) return;
        ChessBoardBox candidate = board.boxes[newX, newY];
        if (candidate.Piece != null && candidate.Piece.Color == color)
        {

        }else
            _list.Add(candidate);
    }
}
public class BishopPossibleMoves : IPieceMoveStrategy
{
    public IEnumerable<ChessBoardBox> GetPossibleMoves(ChessPiece piece)
    {
        List<ChessBoardBox> list = new List<ChessBoardBox>();
        int x = piece.coordX;
        int y = piece.coordY;
        ChessBoard board = piece.Box.Board;
        int maxX = board.sizeWidth - 1;
        int maxY = board.sizeHeight - 1;

        ChessBoardBox currentBox;
        int i;
        int j;

        i = x + 1;
        j = y + 1;
        while (i <= maxX && j <= maxY)
        {
            currentBox = board.boxes[i, j];
            if (currentBox.Piece != null && currentBox.Piece.Color == piece.Color) break;
            list.Add(currentBox);
            i++;
            j++;
            if (currentBox.Piece != null) break;
        }

        i = x - 1;
        j = y + 1;
        while (i >= 0 && j <= maxY)
        {
            currentBox = board.boxes[i, j];
            if (currentBox.Piece != null && currentBox.Piece.Color == piece.Color) break;
            list.Add(currentBox);
            i--;
            j++;
            if (currentBox.Piece != null) break;
        }

        i = x - 1;
        j = y - 1;
        while (i >= 0 && j >= 0)
        {
            currentBox = board.boxes[i, j];
            if (currentBox.Piece != null && currentBox.Piece.Color == piece.Color) break;
            list.Add(currentBox);
            i--;
            j--;
            if (currentBox.Piece != null) break;
        }

        i = x + 1;
        j = y - 1;
        while (i <= maxX && j >= 0)
        {
            currentBox = board.boxes[i, j];
            if (currentBox.Piece != null && currentBox.Piece.Color == piece.Color) break;
            list.Add(currentBox);
            i++;
            j--;
            if (currentBox.Piece != null) break;
        }

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
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPieceMoveStrategy
{
    public IEnumerable<ChessBoardBox> GetPossibleMoves(ChessPiece piece);
}

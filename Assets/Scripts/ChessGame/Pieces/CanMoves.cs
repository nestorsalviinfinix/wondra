using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//  ( 1,-1) ( 1, 0) ( 1, 1)
//
//  ( 0,-1) ( POS ) ( 0, 1)
//
//  (-1,-1) (-1, 0) (-1, 1)

public class KingMoves : IPieceMove
{
    public MoveRule[] PossibleMovements()
    {
        MoveRule[] result = new MoveRule[8];

        result[0] = new MoveRule(new Vector2Int(1,-1),true);
        result[1] = new MoveRule(new Vector2Int(1,0),true);
        result[2] = new MoveRule(new Vector2Int(1,1),true);
        result[3] = new MoveRule(new Vector2Int(0,-1),true);
        result[4] = new MoveRule(new Vector2Int(0,1),true);
        result[5] = new MoveRule(new Vector2Int(-1,-1),true);
        result[6] = new MoveRule(new Vector2Int(-1,0),true);
        result[7] = new MoveRule(new Vector2Int(-1,1),true);

        return result;
    }
}
public class TowerMoves : IPieceMove
{
    public MoveRule[] PossibleMovements()
    {
        MoveRule[] result = new MoveRule[4];

        result[0] = new MoveRule(new Vector2Int(1, 0), false);
        result[1] = new MoveRule(new Vector2Int(0, -1), false);
        result[2] = new MoveRule(new Vector2Int(0, 1), false);
        result[3] = new MoveRule(new Vector2Int(-1, 0), false);

        return result;
    }
}
public class BishopMoves : IPieceMove
{
    public MoveRule[] PossibleMovements()
    {
        MoveRule[] result = new MoveRule[4];

        result[0] = new MoveRule(new Vector2Int(1, -1), false);
        result[1] = new MoveRule(new Vector2Int(1, 1), false);
        result[2] = new MoveRule(new Vector2Int(-1, -1), false);
        result[3] = new MoveRule(new Vector2Int(-1, 1), false);

        return result;
    }
}
public class KnightMoves : IPieceMove
{
    public MoveRule[] PossibleMovements()
    {
        MoveRule[] result = new MoveRule[8];

        result[0] = new MoveRule(new Vector2Int(-1, -2), true);
        result[1] = new MoveRule(new Vector2Int(-2, -1), true);
        result[2] = new MoveRule(new Vector2Int(1, -2), true);
        result[3] = new MoveRule(new Vector2Int(2, -1), true);
        result[4] = new MoveRule(new Vector2Int(2, 1), true);
        result[5] = new MoveRule(new Vector2Int(1, 2), true);
        result[6] = new MoveRule(new Vector2Int(-1, 2), true);
        result[7] = new MoveRule(new Vector2Int(-2, 1), true);

        return result;
    }
}
public class PawnMoves : IPieceMove
{
    private bool moveDown = false;
    public PawnMoves(EChessColor c)
    {
        moveDown = c == EChessColor.Black;
    }

    public MoveRule[] PossibleMovements()
    {
        MoveRule[] result = new MoveRule[2];

        result[0] = new MoveRule(new Vector2Int(moveDown? -1:1, 0), true);
        result[1] = new MoveRule(new Vector2Int(moveDown? -2:2, 0), true,0, true);
        result[1].needEmptySlots =  new Vector2Int(moveDown ? -1 : 1,0);

        return result;
    }
}
public class PawnMovesAttack : IPieceMove
{
    private bool moveDown = false;
    public PawnMovesAttack(EChessColor c)
    {
        moveDown = c == EChessColor.Black;
    }

    public MoveRule[] PossibleMovements()
    {
        MoveRule[] result = new MoveRule[2];

        result[0] = new MoveRule(new Vector2Int(moveDown ? -1 : 1, 1), true);
        result[1] = new MoveRule(new Vector2Int(moveDown ? -1 : 1, -1), true);

        return result;
    }
}
public class QueenMoves : IPieceMove
{
    public MoveRule[] PossibleMovements()
    {
        MoveRule[] result = new MoveRule[8];

        result[0] = new MoveRule(new Vector2Int(1, -1), false);
        result[1] = new MoveRule(new Vector2Int(1, 1), false);
        result[2] = new MoveRule(new Vector2Int(-1, -1), false);
        result[3] = new MoveRule(new Vector2Int(-1, 1), false);

        result[4] = new MoveRule(new Vector2Int(1, 0), false);
        result[5] = new MoveRule(new Vector2Int(0, -1), false);
        result[6] = new MoveRule(new Vector2Int(0, 1), false);
        result[7] = new MoveRule(new Vector2Int(-1, 0), false);

        return result;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPieceMove
{
    public MoveRule[] PossibleMovements();
}
public class MoveRule
{
    public Vector2Int move;
    public bool oneTime = false;
    public int useInTurn = -1;
    public bool jump = false;
    public bool especialCondition = false; //Extender a una serie de bools que todos deben estar en true para pasar. Sino se cancela el movimiento.
    public Vector2Int needEmptySlots;

    public MoveRule(Vector2Int _move, bool _oneTime = false, int _useInTurn = -1, bool conditions = false)
    {
        move = _move;
        oneTime = _oneTime;
        useInTurn = _useInTurn;
        especialCondition = conditions;
    }
}


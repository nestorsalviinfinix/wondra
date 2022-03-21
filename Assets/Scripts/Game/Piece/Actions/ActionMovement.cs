using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionMovement : Action
{
    public override void ExecuteAction(ChessBoardBox originBox, ChessBoardBox destinyBox)
    {
        destinyBox.SetPiece(originBox.Piece);
        originBox.SetPiece(null);
    }
}
public class ActionAttack : Action
{
    public override void ExecuteAction(ChessBoardBox originBox, ChessBoardBox destinyBox)
    {
        destinyBox.SetPiece(originBox.Piece);
        if (destinyBox != originBox)
            originBox.SetPiece(null);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Action : IAction
{
    public static Dictionary<EActionType, Action> actions;
    public static void Init()
    {
        actions = new Dictionary<EActionType, Action>();
        actions.Add(EActionType.Move, new ActionMovement());
    }

    public abstract void ExecuteAction(ChessBoardBox originBox, ChessBoardBox destinyBox);
}

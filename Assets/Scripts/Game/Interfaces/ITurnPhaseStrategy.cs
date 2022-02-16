using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITurnPhaseStrategy
{
    public void Execute(TurnController turnManager);
}

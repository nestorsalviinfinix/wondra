using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialPhaseStrategy : ITurnPhaseStrategy
{
    public void Execute(TurnController turnManager)
    {
        // run Initial stuff
        Debug.Log("START / END initial phase");

        turnManager.AdvanceNextPhase();
    }
}

public class PlanningPhaseStrategy : ITurnPhaseStrategy
{
    public void Execute(TurnController turnManager)
    {
        // run planning stuff

        Debug.Log("Starting plan Phase");
        System.Threading.Thread.Sleep(1000);
        Debug.Log("Ended plan Phase");

        turnManager.AdvanceNextPhase();
    }
}

public class MainPhaseStrategy : ITurnPhaseStrategy
{
    public void Execute(TurnController turnManager)
    {
        // UI stuff
        Debug.LogWarning("WAITING FOR MOVEMENT");
    }
}

public class EndPhaseStrategy : ITurnPhaseStrategy
{
    public void Execute(TurnController turnManager)
    {
        // after battle stuff
        Debug.Log("End phase ended");

        turnManager.AdvanceNextPhase();
    }
}

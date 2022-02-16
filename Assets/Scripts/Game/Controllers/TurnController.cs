using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnController : MonoBehaviour
{
    public ChessPlayerController playerManager;

    public ChessPlayer currentPlayer { private set; get; }
    public EChessColor currentColor { private set; get; }
    public int currentTurn { private set; get; }

    public TurnPhase currentPhase { private set; get; }
    private TurnPhase[] phaseOrder = new TurnPhase[]
    {
        TurnPhase.InitialPhase,
        TurnPhase.PlanningPhase,
        TurnPhase.MainPhase,
        TurnPhase.EndPhase
    };
    Dictionary<TurnPhase, ITurnPhaseStrategy> turnPhaseStrategy;
    private int currentPhaseIndex = 0;

    void Start()
    {
        playerManager = new ChessPlayerController();

        turnPhaseStrategy = new Dictionary<TurnPhase, ITurnPhaseStrategy>();
        turnPhaseStrategy.Add(TurnPhase.InitialPhase, new InitialPhaseStrategy());
        turnPhaseStrategy.Add(TurnPhase.PlanningPhase, new PlanningPhaseStrategy());
        turnPhaseStrategy.Add(TurnPhase.MainPhase, new MainPhaseStrategy());
        turnPhaseStrategy.Add(TurnPhase.EndPhase, new EndPhaseStrategy());

        currentTurn = 0;
    }

    public void NextTurn()
    {
        currentTurn++;

        currentPlayer = playerManager.GetNextPlayer();
        currentColor = currentPlayer.Color;

        StartTurn();
    }

    public void FirstTurn()
    {
        currentPlayer = playerManager.GetFirstPlayer();
        currentColor = currentPlayer.Color;

        StartTurn();
    }

    private void StartTurn()
    {
        currentPhase = TurnPhase.InitialPhase;
        currentPhaseIndex = 0;

        turnPhaseStrategy[currentPhase].Execute(this);
    }

    public void AdvanceNextPhase()
    {
        currentPhaseIndex++;
        if (currentPhaseIndex >= phaseOrder.Length) currentPhaseIndex = 0;

        turnPhaseStrategy[currentPhase].Execute(this);
    }
}

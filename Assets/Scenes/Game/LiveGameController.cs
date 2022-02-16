using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiveGameController : MonoBehaviour
{
    public ChessGameController gameController;
    public LiveBoardController boardController;

    void Start()
    {
        gameController = new ChessGameController();
        Debug.Log(boardController);
        boardController.SetData(gameController.Board);
        boardController.CreateBoxMatrix();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

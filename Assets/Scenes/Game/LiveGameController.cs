using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiveGameController : MonoBehaviour
{
    public ChessGameController gameController;
    public LiveBoardController boardController;
    public LivePieceController pieceController;

    void Start()
    {
        // creates game
        gameController = new ChessGameController();

        // set board data on live board
        boardController.SetData(gameController.Board);
        Debug.Log(boardController);

        // create live board boxes
        boardController.CreateBoxMatrix();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

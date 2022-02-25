using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiveBox : MonoBehaviour
{
    // Start is called before the first frame update
    public LivePiece piece;
    public GameObject possibleMoveIndicator;

    public ELiveBoardBoxStatus Status;
    public int CoordX { get; set; }
    public int CoordY { get; set; }
    public string ACoords { get; set; }
    public ChessBoardBox chessBox;

    public EChessPieceType PieceType { get => piece.PieceType; }
    public EChessColor PieceColor { get => piece.PieceColor; }
    public static NullBox NullBox;

    protected LiveBoardController _boardController;

    void Start()
    {
        
    }

    public void Init(LiveBoardController boardController)
    {
        Status = ELiveBoardBoxStatus.None;
        _boardController = boardController;
        ACoords = ChessBoard.ToAlgebraic(new Vector2(CoordX, CoordY));
        piece = LivePiece.NullPiece;
        chessBox = boardController.chessBoard.boxes[CoordX, CoordY];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnMouseDown()
    {
        _boardController.SelectBox(this);

        //piece.GetAvailableActions();
    }
}

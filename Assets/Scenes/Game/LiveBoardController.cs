using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiveBoardController : MonoBehaviour
{
    private int boardWidth;
    private int boardHeight;
    public float boxWidth;
    public float boxHeight;
    public LiveBox[,] boxes;
    public ChessBoard chessBoard;

    public Material[] boxesMaterials;
    public LiveBox boxPrefab;
    public NullBox NullBox;
    public NullPiece NullPiece;

    // text reference
    //public event Action<LiveBox> OnLiveBoxSelected;
    public delegate void UpdateSelectedBox(LiveBox box);
    public static event UpdateSelectedBox OnUpdateSelectedBox;

    public delegate void UpdatePossibleMoves(List<ChessBoardBox> movesList);
    public static event UpdatePossibleMoves OnUpdatePossibleMoves;

    public LiveBox selectedBox { get; private set; }
    public LivePiece SelectedPiece { get; set; }

    public void CreateBoxMatrix()
    {
        boxes = new LiveBox[boardWidth, boardHeight];

        for (int x = 0; x < boardWidth; x++)
            for (int y = 0; y < boardHeight; y++)
            {
                float posX = boxWidth * (2 * x - boardWidth) / 4;
                float posY = boxHeight * (boardHeight - 2 * y) / 4;
                LiveBox box = Instantiate(boxPrefab, gameObject.transform);
                box.transform.position = new Vector3(posX, 0.01f, posY);
                box.transform.parent = gameObject.transform;

                MeshRenderer render = box.GetComponentInChildren<MeshRenderer>();
                EChessColor color = chessBoard.boxes[x, y].Color;

                render.material = color == EChessColor.White ? boxesMaterials[0] : boxesMaterials[1];

                box.name = $"{ChessBoard.ToAlgebraic(new Vector2(x, y))} - {chessBoard.boxes[x, y].Color}";

                boxes[x, y] = box;
                box.CoordX = x;
                box.CoordY = y;

                box.Init(this);
            }

        CleanPossibleMovesIndicators();
    }

    public void CleanPossibleMovesIndicators()
    {
        for (int x = 0; x < boardWidth; x++)
            for (int y = 0; y < boardHeight; y++)
            {
                boxes[x, y].possibleMoveIndicator.SetActive(false);
                boxes[x, y].Status = ELiveBoardBoxStatus.None;
            }
    }

    public void Init(ChessBoard data)
    {
        LiveBox.NullBox = this.NullBox;
        this.NullBox.Init(this);
        LivePiece.NullPiece = this.NullPiece;

        selectedBox = NullBox;
        SelectedPiece = NullPiece;

        chessBoard = data;

        boardWidth = chessBoard.sizeWidth;
        boardHeight = chessBoard.sizeHeight;

        boxWidth = boxPrefab.GetComponent<BoxCollider>().size.x * 2;
        boxHeight = boxPrefab.GetComponent<BoxCollider>().size.z * 2;
    }

    public void SelectBox(LiveBox box)
    {
        if (selectedBox == box) return;
        // select
        if (box.Status == ELiveBoardBoxStatus.None)
        {
            CleanPossibleMovesIndicators();
            OnUpdateSelectedBox?.Invoke(box);
            selectedBox = box;
            box.Status = ELiveBoardBoxStatus.Selected;

            List<ChessBoardBox> possibleMoves = box.piece.GetLivePossibleMoves();
            OnUpdatePossibleMoves.Invoke(possibleMoves);

            // set possible moves
            foreach (ChessBoardBox m in possibleMoves)
            {
                LiveBox possibleBox = boxes[m.CoordX, m.CoordY];
                possibleBox.possibleMoveIndicator.SetActive(true);
                possibleBox.Status = ELiveBoardBoxStatus.WaitingForAction;
            }
        }

        // action
        if (box.Status == ELiveBoardBoxStatus.WaitingForAction)
        {
            // move piece
            //Debug.Log($"Will move to: {box.ACoords}");

            if(box.piece != LivePiece.NullPiece)
            {
                Destroy(box.piece.gameObject);
                box.piece = LivePiece.NullPiece;
                Debug.Log("ATTAAAACK!!!!");
            }


            EActionType currentActionType = EActionType.Move;
            IAction action = Action.actions[currentActionType];
            selectedBox.piece.ExecuteActionTo(action, box);

            // #TODO: move this to an event listener after implementing turns
            selectedBox = LiveBox.NullBox;
            CleanPossibleMovesIndicators();
        }
    }
}

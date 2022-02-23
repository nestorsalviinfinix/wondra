using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class KeyValuePair
{
    public string key;
    public Sprite value;
}

public class LivePieceController : MonoBehaviour
{
    public List<LivePiece> pieces;
    public ChessBoard boardController;
    public LiveBoardController liveBoard;
    public ChessPlayerController playerController;
    public ChessGameController gameController;
    public LivePiece piecePrefab;
    public List<KeyValuePair> pieceSpritesList;
    private Dictionary<string, Sprite> _pieceSprites;

    public void Init(ChessGameController chessGameController)
    {
        pieces = new List<LivePiece>();
        gameController = chessGameController;
        playerController = chessGameController.playerController;
        boardController = chessGameController.Board;

        _pieceSprites = new Dictionary<string, Sprite>();
        foreach (var kvp in pieceSpritesList) _pieceSprites[kvp.key.ToUpper()] = kvp.value;
        //pieceSpritesList = new List<KeyValuePair>();
    }

    public void CreatePieces()
    {
        foreach (ChessPiece chessPiece in boardController.pieces)
        {
            //Debug.Log($"({chessPiece.coordX}, {chessPiece.coordY}) - {chessPiece.Color}-{chessPiece.Type} from {chessPiece.Owner.PlayerName}");
            LivePiece newPiece = CreatePiece(chessPiece);
            pieces.Add(newPiece);
        }
    }

    public LivePiece CreatePiece(ChessPiece chessPiece)
    {
        //Debug.Log(chessPiece.coordX);
        //Debug.Log(chessPiece.coordY);
        //Debug.Log(ChessBoard.ToVector(chessPiece.coordX + chessPiece.coordY.ToString()));
        LiveBox liveBox = liveBoard.boxes[chessPiece.coordX, chessPiece.coordY];
        LivePiece newPiece = Instantiate(piecePrefab, liveBox.transform);
        liveBox.piece = newPiece;

        newPiece.transform.parent = gameObject.transform;

        newPiece.SetBox(boardController.boxes[chessPiece.coordX, chessPiece.coordY]);

        string color = chessPiece.Color.ToString().ToUpper();
        string type = chessPiece.Type.ToString().ToUpper();

        // #TODO: add later black assets
        // Sprite pieceSprite = _pieceSprites[color + "-" + type];
        Sprite pieceSprite = _pieceSprites["WHITE" + "-" + type];

        SpriteRenderer spriteRenderer = newPiece.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = pieceSprite;
        
        if (chessPiece.Color == EChessColor.Black)
        {
            spriteRenderer.flipX = true;
            Color newColor = new Color32(84, 23, 150, 255);
            newPiece.GetComponent<SpriteRenderer>().color = newColor;
        }

        newPiece.name = $"{chessPiece.Color} {chessPiece.Type} in " +
            $"{ChessBoard.ToAlgebraic(new Vector2(chessPiece.coordX, chessPiece.coordY))}";

        return newPiece;
    }
}

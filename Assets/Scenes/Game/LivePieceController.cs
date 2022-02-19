using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivePieceController : MonoBehaviour
{
    public Piece[] pieces;
    public ChessBoard data;

    void Start()
    {
        CreatePieces();
    }

    public void CreatePieces()
    {
        Debug.Log(data);
    }
}

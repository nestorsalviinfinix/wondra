using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BoardBox : MonoBehaviour
{
    public Tuple<int, char> coordinates;
    public EChessColor color = EChessColor.White;
    private char[] alphabetic = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H' };
    public Transform pieceSpot;
    public GameObject highlighter;
    public PieceGame pieceContaining { get; private set;}
    public Action<BoardBox> amSelect;
    public Vector2Int positionInMatrix;


    void Start()
    {
        MoveIndicator(false);
        amSelect = FindObjectOfType<SelectBoard>().Select;
    }
    void Update()
    {
        
    }
    public void MoveIndicator(bool b, Color c)
    {
        MoveIndicator(b);
        highlighter.GetComponent<SpriteRenderer>().color = c;
    }
    public void MoveIndicator(bool b)
    {
        highlighter.SetActive(b);
    }

    internal void SetPosition(int x, int y)
    {
        color = (x+y) % 2 == 0 ? EChessColor.Black : EChessColor.White;
        positionInMatrix = new Vector2Int(x,y);

        coordinates = new Tuple<int, char>((x+1), alphabetic[y]);

        name = "BoardBox - (" + coordinates.Item1 + "," + coordinates.Item2 + ")";
    }
    public void SetPiece(PieceGame piece)
    {
        pieceContaining = piece;
    }
    public void EliminitePiece()
    {
        Destroy(pieceContaining.gameObject);
        pieceContaining = null;
    }
    public void OnMouseDown()
    {
        amSelect(this);
        if(pieceContaining != null)
        {
            pieceContaining.Select();
        }
    }

    public void Capture(BoardBox attacker)
    {
        EliminitePiece();
        SetPiece(attacker.pieceContaining);
        attacker.pieceContaining.Movement(pieceSpot);
        attacker.SetPiece(null);
    }
}

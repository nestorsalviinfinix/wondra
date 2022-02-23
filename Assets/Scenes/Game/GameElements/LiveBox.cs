using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiveBox : MonoBehaviour
{
    // Start is called before the first frame update
    public Piece piece;
    public int CoordX { get; set; }
    public int CoordY { get; set; }
    public string ACoords { get; set; }

    public List<IObserver> observers;

    void Start()
    {
        
    }

    public void Init()
    {
        ACoords = ChessBoard.ToAlgebraic(new Vector2(CoordX, CoordY));
        observers = new List<IObserver>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnMouseDown()
    {
        Debug.Log($"MOUSE DOWN at {ACoords}");
        foreach (IObserver observer in observers)
        {
            observer.UpdateObserver();
        }
    }
}

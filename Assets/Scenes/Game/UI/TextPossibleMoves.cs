using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextPossibleMoves : MonoBehaviour
{
    private TextMeshProUGUI _text;

    // Start is called before the first frame update
    void Start()
    {
        _text = gameObject.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetPossibleMoves(List<ChessBoardBox> moveList)
    {
        string finalText = $"Possible Moves ({moveList.Count}): ";
        foreach (ChessBoardBox move in moveList)
        {
            //Debug.Log($"{move.CoordX}|{move.CoordY} - {move.ACoordX}|{move.ACoordY}");
            finalText += $"{move.ACoordX}{move.ACoordY}";
            if (move != moveList[moveList.Count - 1]) finalText += ", ";
        }

        _text.text = finalText;
    }
    private void OnEnable()
    {
        LiveBoardController.OnUpdatePossibleMoves += SetPossibleMoves;
    }
    private void OnDisable()
    {
        LiveBoardController.OnUpdatePossibleMoves -= SetPossibleMoves;
    }
}

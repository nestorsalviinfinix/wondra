using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TextSelectedPiece : MonoBehaviour
{
    // Start is called before the first frame update
    private TextMeshProUGUI _text;

    void Start()
    {
        _text = gameObject.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetSelectedBox(LiveBox box)
    {
        string name = $"{ box.PieceColor.ToString().ToLower() } {box.PieceType.ToString().ToLower()}";
        if (box.piece.name == "NULL_PIECE") name = "None";

        _text.text = $"Selected Piece: {name} at {box.ACoords.ToUpper()}";
    }

    private void OnEnable()
    {
        LiveBoardController.OnUpdateSelectedBox += SetSelectedBox;
    }
    private void OnDisable()
    {
        LiveBoardController.OnUpdateSelectedBox -= SetSelectedBox;
    }
}

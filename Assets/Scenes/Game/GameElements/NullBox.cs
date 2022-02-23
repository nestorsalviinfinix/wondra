using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NullBox : LiveBox
{
    // Start is called before the first frame update
    void Start()
    {
    }

    public new void Init(LiveBoardController boardController)
    {
        _boardController = boardController;
        ACoords = "--";
    }

    public new void OnMouseDown()
    {
        return;
    }
}

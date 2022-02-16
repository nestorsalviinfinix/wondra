using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardCreate : MonoBehaviour
{
    public BoardNode modelNode;
    public int height = 4, width= 4;
    public Color blackColor;

    public Transform rightUp, rightDown, leftUp, leftDown;

    void Start()
    {
        if (height < 4)
            height = 4;
        if (width < 4)
            width = 4;

        Create();
    }
    private void Create()
    {
        float Ymove = (rightUp.position.y - rightDown.position.y) / (height-1);
        float Xmove = (rightDown.position.x - leftDown.position.x) / (width-1);

        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                var boardNode = Instantiate(modelNode);
                boardNode.transform.parent = this.transform;
                boardNode.name = "Board node - H: " + i + " / W: " + j;
                boardNode.transform.position = new Vector3(
                                                                leftDown.position.x +Xmove*j,
                                                                leftDown.position.y +Ymove*i,
                                                                0);
                if(i%2 != 0)
                {
                    if (j % 2 != 0)
                        boardNode.SetColorGround(blackColor);
                }else
                {
                    if ((j+1) % 2 != 0)
                        boardNode.SetColorGround(blackColor);
                }

            }
        }
    }

}

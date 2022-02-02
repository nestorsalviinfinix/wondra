using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menus : MonoBehaviour
{
    public void ChangeScene(string s)
    {
        TransitionScenes.Instance.ChangeScene(s);
    }
}

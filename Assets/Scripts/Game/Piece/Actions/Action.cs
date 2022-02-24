using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Action : IAction
{
    public void ExecuteAction()
    {
        Debug.Log("ACTION EXECUTED!");
    }
}

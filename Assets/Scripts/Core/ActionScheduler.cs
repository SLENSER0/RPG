using System.Collections;
using System.Collections.Generic;
using RPG.Core;
using RPG.Movement;
using UnityEngine;

public class ActionScheduler : MonoBehaviour
{
    private IAction _currentAction;
    
    // Start is called before the first frame update
    public void StartAction(IAction action)
    {
        if (_currentAction == action) return;
        if (_currentAction != null)
        {
            _currentAction.Cancel();

        }
        _currentAction = action;

    }
}

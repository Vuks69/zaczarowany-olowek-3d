using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;

public abstract class IAction
{
    private ParametersMenu parametersMenu;

    public void HandleLeftButton()
    {
        // left button is always Undo so no need to override this
    }

    public abstract void HandleTriggerDown();
    public abstract void HandleTriggerUp();
    public abstract void Update();
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IAction
{
    public void HandleLeftButton()
    {
        // left button is always Undo so no need to override this
    }

    public virtual void HandleTrigger()
    {

    }
}
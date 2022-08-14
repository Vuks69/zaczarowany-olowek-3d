using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorPicking : IAction
{
	public override void HandleTrigger()
	{
		// perform action associated with trigger
		// when trigger is pressed AND flystick points at ColorPalette OR PredefinedColorIcon
		// then set CurrentColor
		GameManager.Instance.CurrentColor = Color.red;
	}
}

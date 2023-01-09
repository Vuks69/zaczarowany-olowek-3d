using Assets.Scripts.Managers;
using Assets.Scripts.Menus.Icons;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Menus
{
    public class ClearSceneParametersMenu : ParametersMenu
    {
        private readonly ClearSceneConfirmMenuIcon clearSceneConfirm = new ClearSceneConfirmMenuIcon(GameObject.Find("Clear Scene Confirm"), GameManager.Instance.ActionsData.Selecting);

        public ClearSceneParametersMenu(GameObject gameObject) : base(gameObject)
        {
            icons = new List<MenuIcon> { clearSceneConfirm };
        }
    }
}
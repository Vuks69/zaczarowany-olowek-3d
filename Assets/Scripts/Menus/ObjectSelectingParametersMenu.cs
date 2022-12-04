using Assets.Scripts.Managers;
using Assets.Scripts.Menus.Icons;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Menus
{
    public class ObjectSelectingParametersMenu : ParametersMenu
    {
        private readonly ObjectSelectingMenuIcon removeSelectionIcon = new ObjectSelectingMenuIcon(GameObject.Find("Remove Selection"), GameManager.Instance.ActionsData.ObjectSelecting, GameManager.Instance.ActionsData.ObjectSelecting.RemoveSelection);
        private readonly ObjectSelectingMenuIcon copySelectionIcon = new ObjectSelectingMenuIcon(GameObject.Find("Copy Selection"), GameManager.Instance.ActionsData.ObjectSelecting, GameManager.Instance.ActionsData.ObjectSelecting.CopySelection);
        public ObjectSelectingParametersMenu(GameObject gameObject) : base(gameObject)
        {
            icons = new List<MenuIcon> { removeSelectionIcon, copySelectionIcon };
        }
    }
}
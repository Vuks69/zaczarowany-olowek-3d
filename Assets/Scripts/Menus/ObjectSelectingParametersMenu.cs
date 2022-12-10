using Assets.Scripts.Managers;
using Assets.Scripts.Menus.Icons;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Menus
{
    public class ObjectSelectingParametersMenu : ParametersMenu
    {
        private readonly ObjectSelectingMenuIcon removeSelectionIcon = new ObjectSelectingMenuIcon(GameObject.Find("Remove Selection"), GameManager.Instance.ActionsData.ObjectSelecting, GameManager.Instance.ActionsData.ObjectSelecting.DeleteSelection);
        private readonly ObjectSelectingMenuIcon copySelectionIcon = new ObjectSelectingMenuIcon(GameObject.Find("Copy Selection"), GameManager.Instance.ActionsData.ObjectSelecting, GameManager.Instance.ActionsData.ObjectSelecting.SetStateCopying);
        private readonly ObjectSelectingMenuIcon moveSelectionIcon = new ObjectSelectingMenuIcon(GameObject.Find("Move Selection"), GameManager.Instance.ActionsData.ObjectSelecting, GameManager.Instance.ActionsData.ObjectSelecting.SetStateMoving);
        private readonly ObjectSelectingMenuIcon changeSelectionColorIcon = new ObjectSelectingMenuIcon(GameObject.Find("Change Selection Color"), GameManager.Instance.ActionsData.Selecting, GameManager.Instance.ActionsData.ObjectSelecting.ChangeSelectionColor);
        public ObjectSelectingParametersMenu(GameObject gameObject) : base(gameObject)
        {
            icons = new List<MenuIcon> { removeSelectionIcon, copySelectionIcon, moveSelectionIcon, changeSelectionColorIcon };
        }
    }
}
using UnityEngine;
using Assets.Scripts.Menus.Icons;
using Assets.Scripts.Managers;
using System.Collections.Generic;

namespace Assets.Scripts.Menus
{
    public class SavingLoadingParametersMenu : ParametersMenu
    {
        private readonly SavingMenuIcon savingIcon = new SavingMenuIcon(GameObject.Find("Saving"), GameManager.Instance.ActionsData.Selecting);
        private readonly LoadingMenuIcon loadingIcon = new LoadingMenuIcon(GameObject.Find("Loading"), GameManager.Instance.ActionsData.ObjectSelecting);
        public SavingLoadingParametersMenu(GameObject gameObject) : base(gameObject)
        {
            icons = new List<MenuIcon> { savingIcon, loadingIcon };
        }
    }
}
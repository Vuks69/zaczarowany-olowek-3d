using Assets.Scripts.Managers;
using Assets.Scripts.Menus.Icons;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Menus
{
    public class SavingLoadingParametersMenu : ParametersMenu
    {
        private readonly SavingMenuIcon savingIcon1 = new SavingMenuIcon(GameObject.Find("Saving 1"), GameManager.Instance.ActionsData.Selecting, 1);
        private readonly SavingMenuIcon savingIcon2 = new SavingMenuIcon(GameObject.Find("Saving 2"), GameManager.Instance.ActionsData.Selecting, 2);
        private readonly SavingMenuIcon savingIcon3 = new SavingMenuIcon(GameObject.Find("Saving 3"), GameManager.Instance.ActionsData.Selecting, 3);
        private readonly LoadingMenuIcon loadingIcon1 = new LoadingMenuIcon(GameObject.Find("Loading 1"), GameManager.Instance.ActionsData.ObjectSelecting, 1);
        private readonly LoadingMenuIcon loadingIcon2 = new LoadingMenuIcon(GameObject.Find("Loading 2"), GameManager.Instance.ActionsData.ObjectSelecting, 2);
        private readonly LoadingMenuIcon loadingIcon3 = new LoadingMenuIcon(GameObject.Find("Loading 3"), GameManager.Instance.ActionsData.ObjectSelecting, 3);
        public SavingLoadingParametersMenu(GameObject gameObject) : base(gameObject)
        {
            icons = new List<MenuIcon> { savingIcon1, savingIcon2, savingIcon3, loadingIcon1, loadingIcon2, loadingIcon3 };
        }
    }
}
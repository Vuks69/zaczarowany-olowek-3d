using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Actions;

namespace Assets.Scripts.Menus
{
    public abstract class Menu
    {
        public List<MenuIcon> icons = new List<MenuIcon>();
        public GameObject MenuObject { get; set; }
        public void Init()
        {
            foreach (Transform child in MenuObject.transform)
            {
                icons.Add(new MenuIcon(child.gameObject, new Selecting()));
            }
        }
    }
}
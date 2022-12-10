using Assets.Scripts.Menus.Icons;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Menus
{
    public abstract class Menu
    {
        public List<MenuIcon> icons;
        public GameObject MenuObject { get; set; }
        public MenuIcon SelectedIcon { get; set; }
        public MenuIcon PreviouslySelectedIcon { get; set; }
        public bool IsSelectedIcon { get; set; } = false;
    }
}
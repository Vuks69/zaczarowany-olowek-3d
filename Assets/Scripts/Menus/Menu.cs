using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Menus.Icons;

namespace Assets.Scripts.Menus
{
    public abstract class Menu
    {
        public List<MenuIcon> icons;
        public GameObject MenuObject { get; set; }
        public MenuIcon SelectedIcon { get; set; }
        public bool IsSelectedIcon { get; set; } = true;
    }
}
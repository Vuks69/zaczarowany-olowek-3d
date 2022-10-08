using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Actions;

namespace Assets.Scripts.Menus
{
    public abstract class Menu
    {
        public List<MenuIcon> icons = new List<MenuIcon>();
        public GameObject MenuObject { get; set; }
    }
}
using UnityEngine;

namespace Assets.Scripts.Menus
{
    public class ParametersMenu : Menu
    {
        public ParametersMenu(GameObject gameObject)
        {
            MenuObject = gameObject;
            MenuObject.SetActive(false);
        }
    }
}
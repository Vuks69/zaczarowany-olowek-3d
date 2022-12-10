using Assets.Scripts.Actions;
using UnityEngine;

namespace Assets.Scripts.Menus.Icons
{
    public class SkyboxSelectingMenuIcon : MenuIcon
    {
        private readonly Material skybox;
        public SkyboxSelectingMenuIcon(GameObject icon, Action action, Material skybox) : base(icon, action)
        {
            this.skybox = skybox;
        }

        public override void Select()
        {
            SetSelectedColor();
            getIconsMenu().SelectedIcon = this;
            RenderSettings.skybox = skybox;
        }
    }
}
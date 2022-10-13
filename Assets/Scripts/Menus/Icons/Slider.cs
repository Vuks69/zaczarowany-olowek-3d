using UnityEngine;
using Assets.Scripts.Actions;

namespace Assets.Scripts.Menus.Icons
{
    public class Slider : MenuIcon
    {
        private GameObject sphere;
        private GameObject capsule;
        public Slider(GameObject icon, Action action) : base(icon, action)
        {
            capsule = icon.transform.GetChild(0).gameObject;
            sphere = capsule.transform.GetChild(0).gameObject;
        }

        public override bool IsGameObjectInIcon(GameObject gameObject)
        {
            return this.gameObject == gameObject || sphere == gameObject || capsule == gameObject;
        }

        public override void UpdateColor()
        {
            var renderer = sphere.GetComponent<Renderer>();
            renderer.material.SetColor("_Color", currentColor);
        }
    }
}
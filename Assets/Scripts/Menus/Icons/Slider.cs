using UnityEngine;
using Assets.Scripts.Actions;
using Assets.Scripts.Managers;

namespace Assets.Scripts.Menus.Icons
{
    public class Slider : MenuIcon
    {
        private GameObject sphere;
        private GameObject capsule;
        public Vector3 PreviousFlystickForward { get; set; } = new Vector3(0, 0, 0);
        private Vector3 initialSphereCoord;
        private readonly float sensitivity = 3f;

        public Slider(GameObject icon, Action action) : base(icon, action)
        {
            capsule = icon.transform.GetChild(0).gameObject;
            sphere = capsule.transform.GetChild(0).gameObject;
            initialSphereCoord = sphere.transform.position;
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

        public void Move()
        {
            var newPosition = new Vector3(sphere.transform.position.x - (PreviousFlystickForward.x - FlystickManager.Instance.MultiTool.transform.forward.x), sphere.transform.position.y, initialSphereCoord.z);
            var newPositionLocal = capsule.transform.InverseTransformPoint(newPosition);
            if (newPositionLocal.y >= -1 && newPositionLocal.y <= 1)
            {
                var flystickForward = FlystickManager.Instance.MultiTool.transform.forward;
                sphere.transform.position = new Vector3(sphere.transform.position.x - sensitivity * (PreviousFlystickForward.x - flystickForward.x), sphere.transform.position.y, sphere.transform.position.z);
                PreviousFlystickForward = flystickForward;
                GameManager.Instance.CurrentLineThickness = (newPositionLocal.y + 2) / 2;
            }
        }
    }
}
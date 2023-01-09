using Assets.Scripts.Actions;
using Assets.Scripts.Managers;
using UnityEngine;

namespace Assets.Scripts.Menus.Icons
{
    public class Slider : MenuIcon
    {
        private GameObject sphere;
        private GameObject capsule;
        public Vector3 PreviousFlystickForward { get; set; } = new Vector3(0, 0, 0);
        private Vector3 initialSphereCoord;
        private readonly float sensitivity = 1f;
        protected float value;
        protected float initialValue;

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

        public override void Select()
        {
            SetSelectedColor();
            getIconsMenu().SelectedIcon = this;
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
            if (newPositionLocal.y >= -1.0f && newPositionLocal.y <= 1.0f)
            {
                var flystickForward = FlystickManager.Instance.MultiTool.transform.forward;
                sphere.transform.position = new Vector3(sphere.transform.position.x - sensitivity * (PreviousFlystickForward.x - flystickForward.x), sphere.transform.position.y, sphere.transform.position.z);
                PreviousFlystickForward = flystickForward;
                value = normalized(newPositionLocal.y);
                OnMove();
            }
        }

        public void SetValueToInitial()
        {
            value = initialValue;
            sphere.transform.localPosition = new Vector3(sphere.transform.localPosition.x, unnormalized(value), sphere.transform.localPosition.z);
        }

        protected float normalized(float val)
        {
            return (val + 1.0f) / 2.0f;
        }

        protected float unnormalized(float val)
        {
            return (val * 2.0f) - 1.0f;
        }

        protected virtual void OnMove() { }
    }
}
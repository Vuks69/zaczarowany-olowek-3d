using Assets.Scripts.Actions;
using Assets.Scripts.Managers;
using UnityEngine;

namespace Assets.Scripts.Menus.Icons
{
    public class SelectionScaleSlider : Slider
    {
        private readonly float maxScale = 2.0f;
        private readonly float minScale = 0.1f;

        public SelectionScaleSlider(GameObject icon, Action action) : base(icon, action)
        {
            initialValue = 0.5f;
            SetValueToInitial();
        }

        protected override void OnMove()
        {
            var scale = (value * (maxScale - minScale)) + minScale;
            GameManager.Instance.ActionsData.ObjectSelecting.ChangeSelectionScale(new Vector3(scale, scale, scale));
        }
    }
}
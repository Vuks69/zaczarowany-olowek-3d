using UnityEngine;
using Assets.Scripts.Actions;
using Assets.Scripts.Managers;

namespace Assets.Scripts.Menus.Icons
{
    public class ObjectSizeSlider : Slider
    {

        public ObjectSizeSlider(GameObject icon, Action action) : base(icon, action)
        {
            value = 0.0f;
            value = initialValue;
        }

        protected override void OnMove()
        {
            var objectSize = (value * (GameManager.Instance.MaxObjectSize - GameManager.Instance.MinObjectSize)) + GameManager.Instance.MinObjectSize;
			GameManager.Instance.ActionsData.ObjectAdding.objectSize = objectSize;
        }

    }
}
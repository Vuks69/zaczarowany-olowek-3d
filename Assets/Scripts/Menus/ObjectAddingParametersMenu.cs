using Assets.Scripts.Managers;
using Assets.Scripts.Menus.Icons;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Menus
{
    public class ObjectAddingParametersMenu : ParametersMenu
    {
        private readonly ObjectSizeSlider objectSizeSlider = new ObjectSizeSlider(GameObject.Find("Object Size Slider"), GameManager.Instance.ActionsData.Selecting);
        private readonly ObjectTypeMenuIcon cubeIcon = new ObjectTypeMenuIcon(GameObject.Find("Cube Icon"), GameManager.Instance.ActionsData.Selecting, PrimitiveType.Cube);
        private readonly ObjectTypeMenuIcon cylinderIcon = new ObjectTypeMenuIcon(GameObject.Find("Cylinder Icon"), GameManager.Instance.ActionsData.Selecting, PrimitiveType.Cylinder);
        private readonly ObjectTypeMenuIcon sphereIcon = new ObjectTypeMenuIcon(GameObject.Find("Sphere Icon"), GameManager.Instance.ActionsData.Selecting, PrimitiveType.Sphere);
        private readonly ObjectTypeMenuIcon capsuleIcon = new ObjectTypeMenuIcon(GameObject.Find("Capsule Icon"), GameManager.Instance.ActionsData.Selecting, PrimitiveType.Capsule);


        public ObjectAddingParametersMenu(GameObject gameObject) : base(gameObject)
        {
            icons = new List<MenuIcon> { objectSizeSlider, sphereIcon, cylinderIcon, cubeIcon, capsuleIcon };
        }

    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Menus
{
    public class SliderBehaviour : MenuIcon
    {

        public GameObject sphere;
        public GameObject capsule;
        public float value;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        public override void Update()
        {
            ChangeColor();
            sphere.transform.localPosition = new Vector3(
                sphere.transform.localPosition.x,
                sphere.transform.localPosition.y + 0.01f,
                sphere.transform.localPosition.z
                );
        }

        void WhenMoved()
        {
            value = (sphere.transform.localPosition.y + 1) / 2;
        }

        public override void ChangeColor()
        {
            zmienna++;
            if (zmienna == 100)
            {
                zmienna = 0;
                ToggleColor();
            }
            if (updateColor)
            {
                updateColor = false;
                sphere.GetComponent<Renderer>().material.SetColor("_Color", currentColor);
                capsule.GetComponent<Renderer>().material.SetColor("_Color", currentColor);
            }
        }
    }
}
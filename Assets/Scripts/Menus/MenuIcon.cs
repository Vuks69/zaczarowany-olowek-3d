using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Actions;
using Assets.Scripts.Managers;

namespace Assets.Scripts.Menus
{
    public class MenuIcon : MonoBehaviour
    {
        private static Color DEFAULT_COLOR = Color.white;
        private static Color SELECTED_COLOR = Color.green;
        protected Color currentColor = DEFAULT_COLOR;
        protected bool updateColor = false;
        protected long zmienna = 0;
        public Action Action;

        // Use this for initialization
        void Start()
        {
            Select();
        }

        public void Select()
        {
            GameManager.Instance.CurrentAction = Action;
        }

        // Update is called once per frame
        public virtual void Update()
        {
            ChangeColor();
        }

        public void ToggleColor()
        {
            updateColor = true;
            if (currentColor == DEFAULT_COLOR)
            {
                currentColor = SELECTED_COLOR;
                return;
            }
            currentColor = DEFAULT_COLOR;
        }

        public virtual void ChangeColor()
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
                var renderer = gameObject.GetComponent<Renderer>();
                renderer.material.SetColor("_Color", currentColor);
            }
        }
    }
}
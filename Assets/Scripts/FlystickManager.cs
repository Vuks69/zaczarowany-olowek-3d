using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class FlystickManager : MonoBehaviour
    {
        public static FlystickManager Instance;

        public GameObject Flystick;
        public GameObject MultiTool;
        // przykladowe
        void HandleInput(string input)
        {
            if (input == "trigger_down")
            {
                GameManager.Instance.CurrentAction.HandleTriggerDown();
            }
            else if (input == "trigger_up")
            {
                GameManager.Instance.CurrentAction.HandleTriggerUp();
            }
        }

        void Update()
        {
            GameManager.Instance.CurrentAction.Update();
        }

        void Awake()
        {
            Instance = this;
        }

        //test
        void Start()
        {
            GameManager.Instance.CurrentAction = new LineDrawBehaviour();
            GameManager.Instance.CurrentAction.HandleTriggerDown();
        }
    }
}
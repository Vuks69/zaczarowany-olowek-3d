using Assets.Scripts.Actions;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;
        public Color CurrentColor { get; set; } = Color.magenta;
        public Action CurrentAction { get; set; }
        public ActionsData ActionsData { get; set; }
        public float CurrentLineThickness { get; set; } = 0.0f;
        public float MinStrokeWidth { get; set; } = 0.01f;
        public float MaxStrokeWidth { get; set; } = 0.1f;
        public float MinObjectSize { get; set; } = 0.2f;
        public float MaxObjectSize { get; set; } = 1f;
        public string PathToSaveFile { get; set; } = "save.json";

        void Awake()
        {
            Instance = this;
        }

        void Start()
        {
            ActionsData = new ActionsData();
            CurrentAction = ActionsData.Selecting;
            CurrentAction.Init();
        }

        public void changeCurrentAction(Action action)
        {
            CurrentAction.Finish();
            CurrentAction = action;
            CurrentAction.Init();
        }
    }
}
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
        public float MinStrokeWidth { get; set; } = 0.5f;
        public float MaxStrokeWidth { get; set; } = 5f;
        public float MinObjectSize { get; set; } = 0.2f;
        public float MaxObjectSize { get; set; } = 2f;
        public string PathToSaveFile { get; set; }
        public float LineSize { get; set; } = 0.5f;

        void Awake()
        {
            Instance = this;
            PathToSaveFile = Application.persistentDataPath + "/save.json";
        }

        void Start()
        {
            ActionsData = new ActionsData();
            CurrentAction = ActionsData.Selecting;
            CurrentAction.Init();
            ActionsData.LineDrawing.SetLineType(LineDrawing.LineType.Circle);
        }

        public void changeCurrentAction(Action action)
        {
            CurrentAction.Finish();
            CurrentAction = action;
            CurrentAction.Init();
        }
    }
}
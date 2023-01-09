using Assets.Scripts.Actions;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
        public float MaxStrokeWidth { get; set; } = 0.5f;
        public float MinObjectSize { get; set; } = 0.2f;
        public float MaxObjectSize { get; set; } = 2f;
        public string PathToSaveFile { get; set; }
        public List<List<GameObject>> DeletedObjects { get; set; } = new List<List<GameObject>>();

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
        }

        public void changeCurrentAction(Action action)
        {
            CurrentAction.Finish();
            CurrentAction = action;
            CurrentAction.Init();
        }
    }
}
using UnityEngine;
using Assets.Scripts.Actions;

namespace Assets.Scripts.Managers
{
	public class GameManager : MonoBehaviour
	{
		public static GameManager Instance;

		public Color CurrentColor { get; set; } = Color.magenta;
		public Action CurrentAction { get; set; }
		public ActionsData ActionsData { get; set; }
		// public GameObject LeftFlystick
		// public GameObject CurrentSelection

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
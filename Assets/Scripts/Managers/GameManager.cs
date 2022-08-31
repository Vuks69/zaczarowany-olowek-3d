using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Actions;

namespace Assets.Scripts.Managers
{
	public class GameManager : MonoBehaviour
	{
		public static GameManager Instance;

		public Color CurrentColor { get; set; }
		public Action CurrentAction { get; set; }
		// public GameObject LeftFlystick
		// public GameObject CurrentSelection

		void Awake()
		{
			Instance = this;
		}
	}
}
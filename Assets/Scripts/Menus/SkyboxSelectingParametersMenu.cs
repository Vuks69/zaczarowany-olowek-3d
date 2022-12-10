using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Menus.Icons;
using Assets.Scripts.Managers;

namespace Assets.Scripts.Menus
{
    public class SkyboxSelectingParametersMenu : ParametersMenu
    {
        private readonly SkyboxSelectingMenuIcon skyboxSelectingMenuIcon1 = new SkyboxSelectingMenuIcon(GameObject.Find("Skybox 1"), GameManager.Instance.ActionsData.Selecting, Resources.Load<Material>("Skybox/DiverseSpaceMaterial"));
        private readonly SkyboxSelectingMenuIcon skyboxSelectingMenuIcon2 = new SkyboxSelectingMenuIcon(GameObject.Find("Skybox 2"), GameManager.Instance.ActionsData.Selecting, Resources.Load<Material>("Skybox/GalacticGreenMaterial"));
        private readonly SkyboxSelectingMenuIcon skyboxSelectingMenuIcon3 = new SkyboxSelectingMenuIcon(GameObject.Find("Skybox 3"), GameManager.Instance.ActionsData.Selecting, Resources.Load<Material>("Skybox/GalaxyFireMaterial"));
        private readonly SkyboxSelectingMenuIcon skyboxSelectingMenuIcon4 = new SkyboxSelectingMenuIcon(GameObject.Find("Skybox 4"), GameManager.Instance.ActionsData.Selecting, Resources.Load<Material>("Skybox/sky-1"));
        private readonly SkyboxSelectingMenuIcon skyboxSelectingMenuIcon5 = new SkyboxSelectingMenuIcon(GameObject.Find("Skybox 5"), GameManager.Instance.ActionsData.Selecting, Resources.Load<Material>("Skybox/sky-2"));
        private readonly SkyboxSelectingMenuIcon skyboxSelectingMenuIcon6 = new SkyboxSelectingMenuIcon(GameObject.Find("Skybox 6"), GameManager.Instance.ActionsData.Selecting, Resources.Load<Material>("Skybox/sky-3"));
        private readonly SkyboxSelectingMenuIcon skyboxSelectingMenuIcon7 = new SkyboxSelectingMenuIcon(GameObject.Find("Skybox 7"), GameManager.Instance.ActionsData.Selecting, Resources.Load<Material>("Skybox/sky-5"));
        private readonly SkyboxSelectingMenuIcon skyboxSelectingMenuIcon8 = new SkyboxSelectingMenuIcon(GameObject.Find("Skybox 8"), GameManager.Instance.ActionsData.Selecting, Resources.Load<Material>("Skybox/sky-10"));
        private readonly SkyboxSelectingMenuIcon skyboxSelectingMenuIcon9 = new SkyboxSelectingMenuIcon(GameObject.Find("Skybox 9"), GameManager.Instance.ActionsData.Selecting, Resources.Load<Material>("Skybox/sky-12"));
        public SkyboxSelectingParametersMenu(GameObject gameObject) : base(gameObject)
        {
            icons = new List<MenuIcon> {
                skyboxSelectingMenuIcon1,
                skyboxSelectingMenuIcon2,
                skyboxSelectingMenuIcon3,
                skyboxSelectingMenuIcon4,
                skyboxSelectingMenuIcon5,
                skyboxSelectingMenuIcon6,
                skyboxSelectingMenuIcon7,
                skyboxSelectingMenuIcon8,
                skyboxSelectingMenuIcon9
            };
        }
    }
}
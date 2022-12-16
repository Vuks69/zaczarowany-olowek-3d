using UnityEngine;

namespace Assets.Scripts.Menus
{
    public class ParametersMenusData
    {
        public ColorPickingParametersMenu ColorPickingParametersMenu { get; set; } = new ColorPickingParametersMenu(GameObject.Find("Color Picking Parameters Menu"));
        public LineDrawingParametersMenu LineDrawingParametersMenu { get; set; } = new LineDrawingParametersMenu(GameObject.Find("Line Drawing Parameters Menu"));
        public ObjectSelectingParametersMenu ObjectSelectingParametersMenu { get; set; } = new ObjectSelectingParametersMenu(GameObject.Find("Object Selecting Parameters Menu"));
        public SavingLoadingParametersMenu SavingLoadingParametersMenu { get; set; } = new SavingLoadingParametersMenu(GameObject.Find("Saving Loading Parameters Menu"));
        public ObjectAddingParametersMenu ObjectAddingParametersMenu { get; set; } = new ObjectAddingParametersMenu(GameObject.Find("Object Adding Parameters Menu"));
        public SkyboxSelectingParametersMenu SkyboxSelectingParametersMenu { get; set; } = new SkyboxSelectingParametersMenu(GameObject.Find("Skybox Selecting Parameters Menu"));
        public ClearSceneParametersMenu ClearSceneParametersMenu { get; set; } = new ClearSceneParametersMenu(GameObject.Find("Clear Scene Parameters Menu"));
    }
}
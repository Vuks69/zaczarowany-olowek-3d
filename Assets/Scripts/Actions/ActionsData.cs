using Assets.Scripts.Managers;

namespace Assets.Scripts.Actions
{
    public class ActionsData
    {
        public Selecting Selecting { get; } = new Selecting();
        public LineDrawing LineDrawing{ get; } = new LineDrawing();

        public void AssignParametersMenus()
        {
            Selecting.ParametersMenu = MenuManager.Instance.ParametersMenusData.ColorPickingParametersMenu;
            LineDrawing.ParametersMenu = MenuManager.Instance.ParametersMenusData.LineDrawingParametersMenu;
        }
    }
}
using Assets.Scripts.Managers;

namespace Assets.Scripts.Actions
{
    public class ActionsData
    {
        public Selecting Selecting { get; } = new Selecting();
        public LineDrawing LineDrawing{ get; } = new LineDrawing();

        public void AssignParametrsMenus()
        {
            Selecting.ParametersMenu = MenuManager.Instance.ParametersMenusData.ColorPicking;
            LineDrawing.ParametersMenu = MenuManager.Instance.ParametersMenusData.ColorPicking;
        }
    }
}
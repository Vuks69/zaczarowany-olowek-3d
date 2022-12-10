using Assets.Scripts.Actions;
using Assets.Scripts.Managers;
using UnityEngine;

namespace Assets.Scripts.Menus.Icons
{
    public class LineTypeMenuIcon : MenuIcon
    {
        private LineDrawing.LineType lineType;

        public LineTypeMenuIcon(GameObject icon, Action action, LineDrawing.LineType lineType) : base(icon, action)
        {
            this.lineType = lineType;
        }

        public override void Select()
        {
            GameManager.Instance.ActionsData.LineDrawing.SetLineType(lineType);
            SetSelectedColor();
            getIconsMenu().SelectedIcon = this;
        }
    }
}
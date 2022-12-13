using UnityEngine;
using Assets.Scripts.Actions;
using Assets.Scripts.Managers;

namespace Assets.Scripts.Menus.Icons
{
    public class LineThicknessSlider : Slider
    {

        public LineThicknessSlider(GameObject icon, Action action) : base(icon, action)
        {
            value = 0.0f;
            value = initialValue;
        }

        protected override void OnMove()
        {
            var strokeWidth = (value * (GameManager.Instance.MaxStrokeWidth - GameManager.Instance.MinStrokeWidth)) + GameManager.Instance.MinStrokeWidth;
            GameManager.Instance.CurrentLineThickness = strokeWidth;
            GameManager.Instance.ActionsData.LineDrawing.StrokeWidth = strokeWidth;

        }

    }
}
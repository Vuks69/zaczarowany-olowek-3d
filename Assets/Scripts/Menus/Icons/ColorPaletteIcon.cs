using Assets.Scripts.Actions;
using Assets.Scripts.Managers;
using UnityEngine;

namespace Assets.Scripts.Menus.Icons
{
    public class ColorPaletteIcon : MenuIcon
    {
        public ColorPaletteIcon(GameObject icon, Action action) : base(icon, action) { }

        public override void Select()
        {
            GameManager.Instance.CurrentColor = getColorFromPalette(GameManager.Instance.ActionsData.Selecting.PCoord);
        }

        public override void Highlight()
        {
            // Nothing happens
        }

        public override void Dehighlight()
        {
            // Nothing happens
        }

        private Color getColorFromPalette(Vector2 pCoord)
        {
            Renderer renderer = gameObject.GetComponent<MeshRenderer>();
            Texture2D texture2D = renderer.material.mainTexture as Texture2D;
            pCoord.x *= texture2D.width;
            pCoord.y *= texture2D.height;

            Vector2 tiling = renderer.material.mainTextureScale;
            return texture2D.GetPixel(Mathf.FloorToInt(pCoord.x * tiling.x), Mathf.FloorToInt(pCoord.y * tiling.y));
        }
    }
}
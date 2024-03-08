using UnityEngine;

namespace Player
{
    public class CursorChanger : MonoBehaviour
    {
        [SerializeField] private Texture2D texture2D;
        [Tooltip("If it's true, use hotSpot value")]
        [SerializeField] private bool hasCustomHotspot;
        [SerializeField] private Vector2 customHotSpot;
        [SerializeField] private CursorMode cursorMode;
        
        public void EnableCursorImage()
        {
           Cursor.visible = true;
        }
        
        public void DisableCursorImage()
        {
            Cursor.visible = false;
        }
        
        public void ChangeCursorImage()
        {
            Vector2 newHotspot;

            if (hasCustomHotspot)
            {
                newHotspot = customHotSpot;
            }
            else
            {
                newHotspot = new Vector2(texture2D.width / 2, texture2D.height / 2);
            }

            Cursor.SetCursor(texture2D, newHotspot, cursorMode);
        }
    }
}
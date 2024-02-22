using Player;
using UnityEngine;

namespace Settings
{
    public class GameSettings : MonoBehaviour
    {
        [SerializeField] private CursorChanger cursorChanger;

        private void Awake()
        {
            cursorChanger.DisableCursorImage();
            SetFrameRate(60);
        }
        public void SetFrameRate(int frameRate)
        {
            Application.targetFrameRate = frameRate;
        }
    }
}
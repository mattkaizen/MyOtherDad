using JetBrains.Annotations;
using TMPro;
using UnityEngine;

namespace Tasks
{
    public class TaskDisplay : MonoBehaviour
    {
        [SerializeField] private TMP_Text text;

        [UsedImplicitly]
        public void StrikeThroughText()
        {
            text.fontStyle = FontStyles.Strikethrough | FontStyles.Bold;
        }
    }
}
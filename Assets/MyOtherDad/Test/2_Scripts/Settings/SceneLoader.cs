using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Settings
{
    public class SceneLoader : MonoBehaviour
    {
        [UsedImplicitly]
        public void LoadScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }
        
        [UsedImplicitly]
        public void LoadScene(int sceneIndex)
        {
            SceneManager.LoadScene(sceneIndex);
        }

        [UsedImplicitly]
        public void ExitGame()
        {
            Application.Quit();
        }
    }
}
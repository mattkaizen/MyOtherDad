using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu_Manager : MonoBehaviour
{
    [SerializeField] int sceneIndex;
    [SerializeField] GameObject optionsMenu;

    public void LoadScene()
    {
        SceneManager.LoadScene(sceneIndex); 
    }

    public void QuitGame()
    {
        Debug.Log("I'm Quitting");
        Application.Quit();
    }

    public void OpenOptionsMenu()
    {
        optionsMenu.SetActive(true);
    }

    private void Start()
    {
        optionsMenu.SetActive(false);
    }
}

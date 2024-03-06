using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu_Manager : MonoBehaviour
{
    [SerializeField] int sceneIndex;
    [SerializeField] GameObject optionsMenu;

    [SerializeField] Animator myAnim; 

    public void LoadScene()
    {
        StartCoroutine(StartScene());
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

    IEnumerator StartScene()
    {
        myAnim.SetBool("Play", true);
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(sceneIndex);
    }
}

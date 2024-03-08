using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class MainMenu_Manager : MonoBehaviour
{
    [SerializeField] private UnityEvent awakeEvent; 
    [SerializeField] int sceneIndex;
    [SerializeField] GameObject optionsMenu;

    [SerializeField] Animator myAnim;

    private void Awake()
    {
        Application.targetFrameRate = 60;
        InitializeSettings();
    }

    private void InitializeSettings()
    {
        awakeEvent?.Invoke();
    }
    

    public void LoadScene()
    {
        SceneManager.LoadScene(sceneIndex);
    }
    public void PlayAnimation()
    {
        myAnim.SetBool("Play", true);
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

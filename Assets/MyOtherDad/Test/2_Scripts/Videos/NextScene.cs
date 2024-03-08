using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour
{
    [SerializeField] int sceneIndex;
    [SerializeField] float maxTime;

    private float timer;

    void Start()
    {
        timer = Time.realtimeSinceStartup;
    }

    void Update()
    {
        if (timer < maxTime)
        {
            timer += Time.deltaTime;
        }
        else if (timer > maxTime) 
        {
            SceneManager.LoadScene(sceneIndex);
            timer = 0;
        }
    }
}

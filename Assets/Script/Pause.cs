using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ClosePause();
        }
    }

    public void  ClosePause()
    {
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }

    public void exit()
    {   
        Time.timeScale = 1;
        SceneTransitionManager.instance.LoadSceneWithFade("Start Screen");
    }
}

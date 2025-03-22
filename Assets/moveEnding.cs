using UnityEngine;

public class moveEnding : MonoBehaviour
{
    private int hasChat=0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        hasChat=PlayerPrefs.GetInt("Farhan");
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player"&&hasChat==1)
        {
            SceneTransitionManager.instance.LoadSceneWithFade("Ending");
        }
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;

public class nextScScript : MonoBehaviour
{
    public GameObject Panel;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Panel.SetActive(true);
            //SceneManager.LoadScene("Main Scene");
        }
    }
    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Panel.SetActive(false);
        }
    }

    void OnCollisionStay2D(Collision2D other)
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            SceneManager.LoadScene("SC Farhan");
        }
    }

}

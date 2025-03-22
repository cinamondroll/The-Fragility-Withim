using UnityEngine;
using UnityEngine.SceneManagement;

public class nextScScript : MonoBehaviour
{
    public GameObject Panel;
    [SerializeField] private int hasChat;
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
        hasChat=PlayerPrefs.GetInt("Andre");
        if (other.gameObject.tag == "Player"&&hasChat==1)
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
        hasChat=PlayerPrefs.GetInt("Andre");
        if (Input.GetKeyDown(KeyCode.Return)&&hasChat==1)
        {
            SceneManager.LoadScene("SC Farhan");
        }
    }

}

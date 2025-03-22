using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NPC : MonoBehaviour
{
    [SerializeField] string nextDialogScene;
    public GameObject Chat;
    int isTalked = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isTalked = PlayerPrefs.GetInt(gameObject.name);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (isTalked == 0)
        {
        Chat.SetActive(true);
            
        }

    }

    public void LoadScene(string scene)
    {
        //Debug.Log("Tes");
        SceneManager.LoadScene(scene);
    }

    void OnCollisionStay2D(Collision2D other)
    {
        if (Input.GetKey(KeyCode.Return)&& isTalked == 0)
        {
            GameObject player = GameObject.Find("Player");
            float anxStat = player.GetComponent<Player>().getAnxSta();
            float x = player.transform.position.x;
            float y = player.transform.position.y;
            PlayerPrefs.SetFloat("x", x);
            PlayerPrefs.SetFloat("y", y);
            PlayerPrefs.SetFloat("anxStat", anxStat);
            PlayerPrefs.SetInt(gameObject.name, 1);
            LoadScene(nextDialogScene);
            //gameObject.SetActive(false);    
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        Chat.SetActive(false);
    }
}


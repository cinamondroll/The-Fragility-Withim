using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NPC : MonoBehaviour
{
    public GameObject Chat;
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
        Chat.SetActive(true);
          
        
        }
    
    public void LoadScene(string scene){
        //Debug.Log("Tes");
        SceneManager.LoadScene(scene);
    }

    void OnCollisionStay2D(Collision2D other)
    {
        if (Input.GetKey(KeyCode.Return))
        {
            LoadScene("chat");
            //gameObject.SetActive(false);    
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        Chat.SetActive(false);  
      
        }
    }


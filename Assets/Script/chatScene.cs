using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class chatScene : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    void FixedUpdate()
    {
         if(Input.GetKey(KeyCode.Escape)){
            SceneManager.LoadScene("SC Chapter 1");
        }
    }
}

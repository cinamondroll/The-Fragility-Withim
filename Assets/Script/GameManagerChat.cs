using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{   
    public GameObject card;
    public GameObject panel;
    void Start()
    {
        
    }
    void Update()
    {
     if (Input.GetMouseButtonDown(0))
     {
         card.SetActive(true);
        panel.SetActive(true);
     }   
    }

   
}

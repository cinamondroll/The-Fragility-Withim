using System.Diagnostics;
using UnityEngine;

public class CameraMoce : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public Transform player;
    public Camera mainCamera;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float y= transform.position.y;
        if (transform.position.x < 13&& transform.position.x > -13)
        {
        transform.position = new Vector3(player.position.x, y, transform.position.z);
        }
        if (player.position.x < 13 && player.position.x > -13)
        {
            transform.position = new Vector3(player.position.x, y, transform.position.z);
        }
        
    }

    void FixedUpdate()
    {   
        float y=0.1f;
        if (Input.mouseScrollDelta.y <0  && mainCamera.orthographicSize <= 5f && mainCamera.orthographicSize >= 2.5f)
        {   
            mainCamera.orthographicSize += y;
            Vector3 temp=transform.position;
              float eq= mainCamera.orthographicSize;
            mainCamera.transform.position = new Vector3(temp.x, 0.34f*eq, temp.z);
        }
        else if (Input.mouseScrollDelta.y > 0 && mainCamera.orthographicSize >= 2.5f  && mainCamera.orthographicSize <= 5f){
            
            mainCamera.orthographicSize -= y;
            Vector3 temp=mainCamera.transform.position;
            float eq= mainCamera.orthographicSize;
            mainCamera.transform.position = new Vector3(temp.x, 0.34f*eq, temp.z);
        }
       
        
    }
   

}

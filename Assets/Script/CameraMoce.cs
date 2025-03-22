using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;

public class CameraMoce : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public Transform player;
    private float zoom;
    private float zoomMultiplier = 4f;
    private float minZoom = 3f;
    private float maxZoom = 5f;
    private float velocity = 0f;
    private float smoothTime = 0.25f;
    [SerializeField] private float maxMoveCam;
    [SerializeField] private float minMoveCam;
    [SerializeField] private float MaxPlayerMove;
    [SerializeField] private float MinPlayerMove;

    [SerializeField] private Camera cam;

    void Start()
    {
        zoom = cam.orthographicSize;
    }

    // Update is called once per frame
    void Update()
    {

        if (transform.position.x < maxMoveCam && transform.position.x > minMoveCam)
        {
            transform.position = new Vector3(player.position.x, transform.position.y, transform.position.z);
        }
        else if (player.position.x < MaxPlayerMove && player.position.x > MinPlayerMove)
        {
            transform.position = new Vector3(player.position.x, transform.position.y, transform.position.z);
        }
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        zoom -= scroll * zoomMultiplier;
        zoom = Mathf.Clamp(zoom, minZoom, maxZoom);
        cam.orthographicSize = Mathf.SmoothDamp(cam.orthographicSize, zoom, ref velocity, smoothTime);
        transform.position = new Vector3(transform.position.x, cam.orthographicSize * 0.18f, transform.position.z);

    }

    void FixedUpdate()
    {
    }
}

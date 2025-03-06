using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;         // Transform pemain yang akan diikuti
    public Vector3 offset;          // Offset posisi kamera dari pemain
    public float smoothSpeed = 0.125f; // Kecepatan pergerakan kamera

    void LateUpdate()
    {
        // Hitung posisi kamera yang baru berdasarkan posisi pemain dan offset
        Vector3 desiredPosition = player.position + offset;

        // Perhalus pergerakan kamera dengan Lerp
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Atur posisi kamera
        transform.position = smoothedPosition;

        // Kamera juga bisa mengarah ke pemain (opsional)
        transform.LookAt(player);
    }
}

using UnityEngine;
using UnityEngine.Serialization;

public class CameraShake : MonoBehaviour
{
    [SerializeField] float shakeIntensity = 0.1f; 
    [SerializeField] float shakeSpeed = 1f;  
    
    private Vector3 initialPosition;

    private void Start()
    {
        initialPosition = transform.localPosition;
    }

    private void Update()
    {
        float offsetX = Mathf.PerlinNoise(Time.time * shakeSpeed, 0) * shakeIntensity;
        float offsetY = Mathf.PerlinNoise(0, Time.time * shakeSpeed) * shakeIntensity;
        
        transform.localPosition = initialPosition + new Vector3(offsetX, offsetY, 0);
    }
}

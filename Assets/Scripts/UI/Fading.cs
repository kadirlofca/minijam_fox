using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Fading : MonoBehaviour
{
    public static Func<IEnumerator> OnFadeIn;
    public static Func<IEnumerator> OnFadeOut;
    
    [SerializeField] private Image _fadeImage;
    [SerializeField] private float _fadeSpeed = 0.5f;

    private void Awake()
    {
        StartCoroutine(FadeIn());
    }

    private void OnEnable()
    {
        OnFadeIn += FadeIn;
        OnFadeOut += FadeOut;
    }

    private void OnDisable()
    {
        OnFadeIn -= FadeIn;
        OnFadeOut -= FadeOut;
    }
    
    
    private IEnumerator FadeIn()
    {
        Color color = _fadeImage.color;
        color.a = 1f;
        _fadeImage.color = color; 

        while (color.a > 0f)
        {
            color.a -= Time.deltaTime * _fadeSpeed; 
            _fadeImage.color = color; 
            yield return null; 
        }

        color.a = 0f; 
        _fadeImage.raycastTarget = false;
        _fadeImage.color = color;
    }

    private IEnumerator FadeOut()
    {
        _fadeImage.raycastTarget = true;
        
        Color color = _fadeImage.color; 
        color.a = 0f; 
        _fadeImage.color = color;

        while (color.a < 1f)
        {
            color.a += Time.deltaTime * _fadeSpeed;
            _fadeImage.color = color;
            yield return null; 
        }

        color.a = 1f; 

        _fadeImage.color = color;
    }
}

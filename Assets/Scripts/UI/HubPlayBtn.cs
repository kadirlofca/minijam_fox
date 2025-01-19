using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HubPlayBtn : MonoBehaviour
{
    public static Action OnEnableButton;
    
    [SerializeField] private int levelIndex = 1;

    private Button _button;
    
    private void Awake()
    {
        _button = GetComponent<Button>();   
    }

    private void OnEnable()
    {
        OnEnableButton += EnableButton;   
    }

    private void OnDisable()
    {
        OnEnableButton -= EnableButton;  
    }

    public void LoadLevel()
    {
        StartCoroutine(LoadLevelCoroutine());
    }

    private IEnumerator LoadLevelCoroutine()
    {
        yield return Fading.OnFadeOut?.Invoke();
        SceneManager.LoadScene(levelIndex);
    }
    
    private void EnableButton()
    {
        _button.interactable = true;
    }
}

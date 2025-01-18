using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void LoadLevel(int level)
    {
        StartCoroutine(LoadLevelCoroutine(level));
    }
    
    public void ExitGame() => Application.Quit();


    IEnumerator LoadLevelCoroutine(int level)
    {
        yield return Fading.OnFadeOut();
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(level);
        
    }
}

using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void LoadLevel(int level)
    {
        Application.LoadLevel(level);
    }
    
    public void ExitGame() => Application.Quit();
}

using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public void RestartButton(){
        SceneManager.LoadScene(0);
    }

    public void ExitButton(){
        SceneManager.LoadScene(1);
    }

}

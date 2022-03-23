using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectionMenu : MonoBehaviour {

    public void LoadDinoSkateboarding() {
        SceneManager.LoadScene(1);
    }

    public void LoadGame(string sceneName){
        SceneManager.LoadScene(sceneName);
    } // end method 

    public void QuitGame() {
        Application.Quit();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Roly_SceneManager : MonoBehaviour
{   
    public Animator transition;

    public float transitionTime;
    public static Roly_SceneManager instance {get; private set;}
    public string currentScene = "";
    private void Awake(){
        if(instance != null && instance != this)
            Destroy(this);
        else
            instance = this;
    } // end awake

    public string getCurrentScene(){
        return currentScene;
    } // end getter

    public void setCurrentScene(string newScene){
        currentScene = newScene;
    } // end setter
    public void loadScene(string newScene){

        StartCoroutine(Load(newScene));
    } // end method

    IEnumerator Load(string newScene){

        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(newScene);
        setCurrentScene(newScene);
    } // end method

    public void CloseMusic(){
        Destroy(GameObject.Find("--- Music ---"));
    } // end method
} // end method

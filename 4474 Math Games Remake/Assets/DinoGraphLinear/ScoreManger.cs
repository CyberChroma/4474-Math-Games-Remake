using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using TMPro;
using UnityEngine.SceneManagement;

public enum GraphType
{
    Linear,
    NonLinear,
}
public class ScoreManger : MonoBehaviour
{
    public GameObject graph;
    public List<Sprite> linear;
    public List<Sprite> nonLinear;
    public Transform dragableParent;
    public Animator dinoAnimator;

    public TextMeshProUGUI scoreUI;

    [Header("Menu Stuff")]
    public GameObject startMenu;
    public GameObject endMenu;
    public GameObject pauseMenu;

    private static int score = 0;
    private void Start()
    {
        startMenu.SetActive(true);
        List<KeyValuePair<Sprite, GraphType>> newList = new List<KeyValuePair<Sprite, GraphType>>();
        newList.AddRange(ToPair(linear, GraphType.Linear));
        newList.AddRange(ToPair(nonLinear, GraphType.NonLinear));
        var rnd = new System.Random();
        var randomized = newList.OrderBy(item => rnd.Next());
        foreach (KeyValuePair<Sprite, GraphType> valuePair in randomized)
        {
            GameObject current = Instantiate(graph, dragableParent);
            current.GetComponent<Image>().sprite = valuePair.Key;
            current.GetComponent<DragSnap>().type = valuePair.Value;
        }
    }

    private List<KeyValuePair<Sprite, GraphType>> ToPair(List<Sprite> list, GraphType type)
    {
        List<KeyValuePair<Sprite, GraphType>> newList = new List<KeyValuePair<Sprite, GraphType>>();
        foreach (Sprite sprite in list)
        {
            newList.Add(new KeyValuePair<Sprite, GraphType>(sprite, type));
        }
        return newList;
    }

    private void OnEnable()
    {
        score = 0;
    }

    public void UpdateScore()
    {
        dinoAnimator.SetTrigger("right");
        score++;
    }

    public void Wrong()
    {
        dinoAnimator.SetTrigger("wrong");
    }

    public void StartGame()
    {
        startMenu.SetActive(false);
    }

    public void Pause()
    {
        pauseMenu.SetActive(!pauseMenu.activeSelf);
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }

    private void Update()
    {
        scoreUI.text = $"Score: {score}";
        if (score == linear.Count + nonLinear.Count)
        {
            endMenu.SetActive(true);
        }
    }
}

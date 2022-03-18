using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DinoSkateCharacterPick : MonoBehaviour
{
    public float imageSlideSmoothing;
    public float imageCropSmoothing;

    public Image ripper;
    public Image slash;
    public Image skitch;
    public Image burley;
    public GameObject chooseCharacterText;

    public DinoSkateRaceManager raceManager;
    public DinoSkatePlayerMove playerMove;
    public DinoSkateAIMove AIMove1;
    public DinoSkateAIMove AIMove2;

    public AudioClip ripperVoiceLine;
    public AudioClip slashVoiceLine;
    public AudioClip skitchVoiceLine;
    public AudioClip burleyVoiceLine;

    private int activeImage; // -1 = none active
    private bool canSelect;
    private RectTransform slashTransform;
    private RectTransform skitchTransform;
    private DinoSkateVoiceManager voiceManager;

    // Start is called before the first frame update
    void Start()
    {
        ripper.gameObject.SetActive(true);
        slash.gameObject.SetActive(true);
        skitch.gameObject.SetActive(true);
        burley.gameObject.SetActive(true);
        chooseCharacterText.SetActive(true);
        slashTransform = slash.GetComponent<RectTransform>();
        skitchTransform = skitch.GetComponent<RectTransform>();
        slashTransform.anchoredPosition = Vector3.right * -130;
        skitchTransform.anchoredPosition = Vector3.right * 130;
        slash.fillAmount = 0.715f;
        skitch.fillAmount = 0.715f;
        activeImage = -1;
        canSelect = false;
        voiceManager = FindObjectOfType<DinoSkateVoiceManager>();
        StartCoroutine(WaitToEnableSelect());
    }

    IEnumerator WaitToEnableSelect()
    {
        yield return new WaitForSeconds(4f);
        canSelect = true;
        switch (activeImage) {
            case 0:
                voiceManager.PlayVoiceLine(ripperVoiceLine);
                break;
            case 1:
                voiceManager.PlayVoiceLine(slashVoiceLine);
                break;
            case 2:
                voiceManager.PlayVoiceLine(skitchVoiceLine);
                break;
            case 3:
                voiceManager.PlayVoiceLine(burleyVoiceLine);
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (canSelect) {
            switch (activeImage) {
                case -1:
                    // None active
                    slashTransform.anchoredPosition = Vector3.right * Mathf.Lerp(slashTransform.anchoredPosition.x, -130, imageSlideSmoothing * Time.deltaTime);
                    skitchTransform.anchoredPosition = Vector3.right * Mathf.Lerp(skitchTransform.anchoredPosition.x, 130, imageSlideSmoothing * Time.deltaTime);
                    slash.fillAmount = Mathf.Lerp(slash.fillAmount, 0.715f, imageCropSmoothing * Time.deltaTime);
                    skitch.fillAmount = Mathf.Lerp(skitch.fillAmount, 0.715f, imageCropSmoothing * Time.deltaTime);
                    break;
                case 0:
                    // Ripper active
                    slashTransform.anchoredPosition = Vector3.right * Mathf.Lerp(slashTransform.anchoredPosition.x, 105, imageSlideSmoothing * Time.deltaTime);
                    skitchTransform.anchoredPosition = Vector3.right * Mathf.Lerp(skitchTransform.anchoredPosition.x, 200, imageSlideSmoothing * Time.deltaTime);
                    slash.fillAmount = Mathf.Lerp(slash.fillAmount, 0.58f, imageCropSmoothing * Time.deltaTime);
                    skitch.fillAmount = Mathf.Lerp(skitch.fillAmount, 0.58f, imageCropSmoothing * Time.deltaTime);
                    break;
                case 1:
                    // Slash active
                    slashTransform.anchoredPosition = Vector3.right * Mathf.Lerp(slashTransform.anchoredPosition.x, -150, imageSlideSmoothing * Time.deltaTime);
                    skitchTransform.anchoredPosition = Vector3.right * Mathf.Lerp(skitchTransform.anchoredPosition.x, 200, imageSlideSmoothing * Time.deltaTime);
                    slash.fillAmount = Mathf.Lerp(slash.fillAmount, 1, imageCropSmoothing * Time.deltaTime);
                    skitch.fillAmount = Mathf.Lerp(skitch.fillAmount, 0.58f, imageCropSmoothing * Time.deltaTime);
                    break;
                case 2:
                    // Skitch active
                    slashTransform.anchoredPosition = Vector3.right * Mathf.Lerp(slashTransform.anchoredPosition.x, -150, imageSlideSmoothing * Time.deltaTime);
                    skitchTransform.anchoredPosition = Vector3.right * Mathf.Lerp(skitchTransform.anchoredPosition.x, 200, imageSlideSmoothing * Time.deltaTime);
                    slash.fillAmount = Mathf.Lerp(slash.fillAmount, 0.58f, imageCropSmoothing * Time.deltaTime);
                    skitch.fillAmount = Mathf.Lerp(skitch.fillAmount, 1, imageCropSmoothing * Time.deltaTime);
                    break;
                case 3:
                    // Burley active
                    slashTransform.anchoredPosition = Vector3.right * Mathf.Lerp(slashTransform.anchoredPosition.x, -150, imageSlideSmoothing * Time.deltaTime);
                    skitchTransform.anchoredPosition = Vector3.right * Mathf.Lerp(skitchTransform.anchoredPosition.x, -105, imageSlideSmoothing * Time.deltaTime);
                    slash.fillAmount = Mathf.Lerp(slash.fillAmount, 0.58f, imageCropSmoothing * Time.deltaTime);
                    skitch.fillAmount = Mathf.Lerp(skitch.fillAmount, 0.58f, imageCropSmoothing * Time.deltaTime);
                    break;
            }
            skitch.raycastPadding = new Vector4(skitchTransform.sizeDelta.x - skitchTransform.sizeDelta.x * skitch.fillAmount, 0, 0, 0);
        }
    }

    public void SelectCharacter(int characterSelected)
    {
        if (canSelect) {
            switch (characterSelected) {
                case 0:
                    playerMove.transform.Find("Isometric Lean").Find("Ripper").gameObject.SetActive(true);
                    playerMove.transform.Find("Isometric Lean").Find("Slash").gameObject.SetActive(false);
                    playerMove.transform.Find("Isometric Lean").Find("Skitch").gameObject.SetActive(false);
                    playerMove.transform.Find("Isometric Lean").Find("Burley").gameObject.SetActive(false);
                    playerMove.anim = playerMove.transform.Find("Isometric Lean").Find("Ripper").GetComponent<Animator>();
                    break;
                case 1:
                    playerMove.transform.Find("Isometric Lean").Find("Ripper").gameObject.SetActive(false);
                    playerMove.transform.Find("Isometric Lean").Find("Slash").gameObject.SetActive(true);
                    playerMove.transform.Find("Isometric Lean").Find("Skitch").gameObject.SetActive(false);
                    playerMove.transform.Find("Isometric Lean").Find("Burley").gameObject.SetActive(false);
                    playerMove.anim = playerMove.transform.Find("Isometric Lean").Find("Slash").GetComponent<Animator>();
                    break;
                case 2:
                    playerMove.transform.Find("Isometric Lean").Find("Ripper").gameObject.SetActive(false);
                    playerMove.transform.Find("Isometric Lean").Find("Slash").gameObject.SetActive(false);
                    playerMove.transform.Find("Isometric Lean").Find("Skitch").gameObject.SetActive(true);
                    playerMove.transform.Find("Isometric Lean").Find("Burley").gameObject.SetActive(false);
                    playerMove.anim = playerMove.transform.Find("Isometric Lean").Find("Skitch").GetComponent<Animator>();
                    break;
                case 3:
                    playerMove.transform.Find("Isometric Lean").Find("Ripper").gameObject.SetActive(false);
                    playerMove.transform.Find("Isometric Lean").Find("Slash").gameObject.SetActive(false);
                    playerMove.transform.Find("Isometric Lean").Find("Skitch").gameObject.SetActive(false);
                    playerMove.transform.Find("Isometric Lean").Find("Burley").gameObject.SetActive(true);
                    playerMove.anim = playerMove.transform.Find("Isometric Lean").Find("Burley").GetComponent<Animator>();
                    break;
            }
            playerMove.anim.SetFloat("RandomStart", Random.Range(0f, 1f));
            playerMove.GetComponent<DinoSkateProgressionMeter>().SetupIcon(characterSelected);

            int ai1Character = Random.Range(0, 4);
            while (ai1Character == characterSelected) {
                ai1Character = Random.Range(0, 4);
            }

            switch (ai1Character) {
                case 0:
                    AIMove1.transform.Find("Isometric Lean").Find("Ripper").gameObject.SetActive(true);
                    AIMove1.transform.Find("Isometric Lean").Find("Slash").gameObject.SetActive(false);
                    AIMove1.transform.Find("Isometric Lean").Find("Skitch").gameObject.SetActive(false);
                    AIMove1.transform.Find("Isometric Lean").Find("Burley").gameObject.SetActive(false);
                    AIMove1.anim = AIMove1.transform.Find("Isometric Lean").Find("Ripper").GetComponent<Animator>();
                    break;
                case 1:
                    AIMove1.transform.Find("Isometric Lean").Find("Ripper").gameObject.SetActive(false);
                    AIMove1.transform.Find("Isometric Lean").Find("Slash").gameObject.SetActive(true);
                    AIMove1.transform.Find("Isometric Lean").Find("Skitch").gameObject.SetActive(false);
                    AIMove1.transform.Find("Isometric Lean").Find("Burley").gameObject.SetActive(false);
                    AIMove1.anim = AIMove1.transform.Find("Isometric Lean").Find("Slash").GetComponent<Animator>();
                    break;
                case 2:
                    AIMove1.transform.Find("Isometric Lean").Find("Ripper").gameObject.SetActive(false);
                    AIMove1.transform.Find("Isometric Lean").Find("Slash").gameObject.SetActive(false);
                    AIMove1.transform.Find("Isometric Lean").Find("Skitch").gameObject.SetActive(true);
                    AIMove1.transform.Find("Isometric Lean").Find("Burley").gameObject.SetActive(false);
                    AIMove1.anim = AIMove1.transform.Find("Isometric Lean").Find("Skitch").GetComponent<Animator>();
                    break;
                case 3:
                    AIMove1.transform.Find("Isometric Lean").Find("Ripper").gameObject.SetActive(false);
                    AIMove1.transform.Find("Isometric Lean").Find("Slash").gameObject.SetActive(false);
                    AIMove1.transform.Find("Isometric Lean").Find("Skitch").gameObject.SetActive(false);
                    AIMove1.transform.Find("Isometric Lean").Find("Burley").gameObject.SetActive(true);
                    AIMove1.anim = AIMove1.transform.Find("Isometric Lean").Find("Burley").GetComponent<Animator>();
                    break;
            }
            AIMove1.anim.SetFloat("RandomStart", Random.Range(0f, 1f));
            AIMove1.GetComponent<DinoSkateProgressionMeter>().SetupIcon(ai1Character);


            int ai2Character = Random.Range(0, 4);
            while (ai2Character == characterSelected || ai2Character == ai1Character) {
                ai2Character = Random.Range(0, 4);
            }

            switch (ai2Character) {
                case 0:
                    AIMove2.transform.Find("Isometric Lean").Find("Ripper").gameObject.SetActive(true);
                    AIMove2.transform.Find("Isometric Lean").Find("Slash").gameObject.SetActive(false);
                    AIMove2.transform.Find("Isometric Lean").Find("Skitch").gameObject.SetActive(false);
                    AIMove2.transform.Find("Isometric Lean").Find("Burley").gameObject.SetActive(false);
                    AIMove2.anim = AIMove2.transform.Find("Isometric Lean").Find("Ripper").GetComponent<Animator>();
                    break;
                case 1:
                    AIMove2.transform.Find("Isometric Lean").Find("Ripper").gameObject.SetActive(false);
                    AIMove2.transform.Find("Isometric Lean").Find("Slash").gameObject.SetActive(true);
                    AIMove2.transform.Find("Isometric Lean").Find("Skitch").gameObject.SetActive(false);
                    AIMove2.transform.Find("Isometric Lean").Find("Burley").gameObject.SetActive(false);
                    AIMove2.anim = AIMove2.transform.Find("Isometric Lean").Find("Slash").GetComponent<Animator>();
                    break;
                case 2:
                    AIMove2.transform.Find("Isometric Lean").Find("Ripper").gameObject.SetActive(false);
                    AIMove2.transform.Find("Isometric Lean").Find("Slash").gameObject.SetActive(false);
                    AIMove2.transform.Find("Isometric Lean").Find("Skitch").gameObject.SetActive(true);
                    AIMove2.transform.Find("Isometric Lean").Find("Burley").gameObject.SetActive(false);
                    AIMove2.anim = AIMove2.transform.Find("Isometric Lean").Find("Skitch").GetComponent<Animator>();
                    break;
                case 3:
                    AIMove2.transform.Find("Isometric Lean").Find("Ripper").gameObject.SetActive(false);
                    AIMove2.transform.Find("Isometric Lean").Find("Slash").gameObject.SetActive(false);
                    AIMove2.transform.Find("Isometric Lean").Find("Skitch").gameObject.SetActive(false);
                    AIMove2.transform.Find("Isometric Lean").Find("Burley").gameObject.SetActive(true);
                    AIMove2.anim = AIMove2.transform.Find("Isometric Lean").Find("Burley").GetComponent<Animator>();
                    break;
            }
            AIMove2.anim.SetFloat("RandomStart", Random.Range(0f, 1f));
            AIMove2.GetComponent<DinoSkateProgressionMeter>().SetupIcon(ai2Character);

            raceManager.StartRace();
            gameObject.SetActive(false);
        }
    }

    public void HoverOverCharacter(int characterSelected)
    {
        activeImage = characterSelected;
        if (canSelect) {
            switch (characterSelected) {
                case 0:
                    voiceManager.PlayVoiceLine(ripperVoiceLine);
                    break;
                case 1:
                    voiceManager.PlayVoiceLine(slashVoiceLine);
                    break;
                case 2:
                    voiceManager.PlayVoiceLine(skitchVoiceLine);
                    break;
                case 3:
                    voiceManager.PlayVoiceLine(burleyVoiceLine);
                    break;
            }
        }
    }

    public void StopHoverOverCharacter(int characterSelected)
    {
        activeImage = -1;
    }
}

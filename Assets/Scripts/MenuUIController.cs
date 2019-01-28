using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuUIController : MonoBehaviour
{

    public Animator animator;

    public GameObject RegistrationPanel;
    public GameObject memberPanel;

    public InputField NameInput;



    public Button soundButton;
    public Sprite soundOnSprite;
    public Sprite soundOffSprite;

    private void Awake()
    {
        GuideManager.Instance.firstTimeEvent.AddListener(OpenRegistrationPanel);
        GuideManager.Instance.memberPanelEvent.AddListener(OpenMemberPanel);
    }


    // Use this for initialization
    void Start()
    {

    }

    private IEnumerator ShowPanelAFterTransition(int panelIndex)
    {
        if (panelIndex == 0)
        {
            RegistrationPanel.SetActive(true);
        }

        if (panelIndex == 1)
        {
            animator.SetBool("fadeOut", true);
            yield return new WaitForSeconds(1f);
            animator.SetBool("fadeOut", false);
            animator.SetBool("fadeIn", true);
            yield return new WaitForSeconds(0.1f);
            animator.SetBool("fadeIn", false);
            RegistrationPanel.SetActive(false);
            memberPanel.SetActive(true);
        }
    }

    private IEnumerator ChaneSceneAfterTransition(int sceneIndex)
    {
        Debug.Log("Scene Index:" + sceneIndex);
        if (sceneIndex == 1)
        {
            animator.SetBool("fadeOut", true);
            yield return new WaitForSeconds(1.1f);
            animator.SetBool("fadeOut", false);
            //animator.SetBool("fadeIn", true);
            SceneManager.LoadScene(1);
        }

    }


    void OpenRegistrationPanel()
    {
        StartCoroutine(ShowPanelAFterTransition(0));
    }

    public void Register()
    {
        string name = NameInput.text;
        Debug.Log("Name:" + name);
        GuideManager.Instance.registerEvent.Invoke(name);
    }

    void OpenMemberPanel(bool isFirst)
    {
        StartCoroutine(ShowPanelAFterTransition(1));
    }

    public void loadScene(int sceneIndex)
    {
        SoundManager.Instance.Play("Button");
        StartCoroutine(ChaneSceneAfterTransition(sceneIndex));

    }

    public void muteSound()
    {
        if (!SoundManager.Instance.isMuted)
        {
            SoundManager.Instance.isMuted = !SoundManager.Instance.isMuted;
            soundButton.GetComponent<Image>().sprite = soundOffSprite;
            //SoundManager.instance.EffectsSource.mute = true;
            SoundManager.Instance.MusicSource.mute = true;
        }
        else
        {
            //soundManager.SetActive(true);
            //SoundManager.instance.EffectsSource.mute = false;
            SoundManager.Instance.MusicSource.mute = false;
            SoundManager.Instance.isMuted = !SoundManager.Instance.isMuted;
            soundButton.GetComponent<Image>().sprite = soundOnSprite;
            //SoundManager.instance.PlayMusic("GameSound");
        }
    }
}

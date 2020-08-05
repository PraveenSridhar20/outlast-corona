using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelChanger : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator animator;
    private int levelToLoad;
    public AudioSource audioSource;
    public AudioSource audioSource1;
    public Text text;
    public GameObject loadingScreen;
    public Slider slider;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FadeToLevel(int levelIndex){
        if (audioSource!=null){
                StartCoroutine(FadeAudioSource.StartFade(audioSource,audioSource1,1f,0f));
               // audioSource=null;
        }
        levelToLoad=levelIndex;
        animator.SetTrigger("FadeOut");
        
    }
    public void OnFadeComplete(){
      //  AsyncOperation operation=SceneManager.LoadSceneAsync(levelToLoad);
      StartCoroutine(LoadAsynchro(levelToLoad));
    }

    IEnumerator LoadAsynchro(int sceneIndex){
        
        AsyncOperation operation=SceneManager.LoadSceneAsync(levelToLoad);
        loadingScreen.SetActive(true);
        while (!operation.isDone){
            float progress=Mathf.Clamp01(operation.progress/.9f);
            slider.value=progress;
            text.text="Loading...";
            yield return null;
        }

    }
}

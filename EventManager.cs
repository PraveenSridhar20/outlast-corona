using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject gameLoseUI;
    public GameObject gameLoseCrowdUI;
    public GameObject gameWinUI;
    static bool gameOver=false;
    static bool gameWon=false;
    static bool level2=false;
    static bool level0=true;
    public LevelChanger levelChanger;
    void Start()
    {
       Cursor.lockState=CursorLockMode.Locked;
       Cursor.visible=false;
       VirusPath.onPlayerCollision+=showGameLoseUI;
       LevelComplete.levelWon+=showGameWinUI;
       CrowdHitBox.onPlayerCollisionCrowdDeath+=showGameLoseCrowdUI;
    }

    // Update is called once per frame
    void Update()
    {
         if (level0){
            if (Input.GetKeyDown(KeyCode.F)){
                level0=false;
                levelChanger.FadeToLevel(1);
               // SceneManager.LoadScene(1);
            }
        }
        if (level2){
            if (Input.GetKeyDown(KeyCode.F)){
                levelChanger.FadeToLevel(3);
                level2=false;
            }
        }
         if (gameOver){
            if(Input.GetKeyDown(KeyCode.Space)){
                gameOver=false;
                SceneManager.LoadScene(1);
                PlayerController.lives=3;
                PlayerController.pills=0;
                PlayerController.disabled=false;
            }
            
        }
        else if (gameWon){
            if (Input.GetKeyDown(KeyCode.Space)){
                levelChanger.FadeToLevel(2);
                gameWon=false;
                level2=true;
            }
        }
        
    }

    void showGameLoseUI(){
        gameLoseUI.SetActive(true);
        gameOver=true;
    }
    
    void showGameLoseCrowdUI(){
        gameLoseCrowdUI.SetActive(true);
        gameOver=true;
    }

    void showGameWinUI(){
        gameWinUI.SetActive(true);
        gameWon=true;
    }

    void OnDestroy(){
        VirusPath.onPlayerCollision-=showGameLoseUI;
        CrowdHitBox.onPlayerCollisionCrowdDeath-=showGameLoseCrowdUI;
        LevelComplete.levelWon-=showGameWinUI;
    }
}

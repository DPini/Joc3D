using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameMenuController : MonoBehaviour {

    private GameObject deadPanel;
    private GameObject creditsPanel;
    private GameController gameController;
    private AudioController audioController;
    private bool isMenuShown;
    private bool creditsShown;
    

	// Use this for initialization
	public void Init() {
        gameController = GameObject.Find("Controllers").GetComponent<GameController>();
        deadPanel = GameObject.Find("DeadPanel");
        creditsPanel = GameObject.Find("CreditsPanel");
        isMenuShown = false;
        deadPanel.SetActive(isMenuShown);
        creditsShown = false;
        creditsPanel.SetActive(creditsShown);
        audioController = GameObject.Find("Music").GetComponent<AudioController>();

    }

    public void showMenu( bool b, bool endGame = false )
    {
        if (isMenuShown != b)
        {
            deadPanel.SetActive(b);
            isMenuShown = b;
        }
        if (b) 
	{
            EventSystem.current.SetSelectedGameObject(GameObject.Find("RestartGameBtn"));
            if(endGame) audioController.EnterMenu();
	}
        else
            EventSystem.current.SetSelectedGameObject(null);
    }

    public void showCredits( bool b ){
        if (creditsShown != b)
        {
            creditsPanel.SetActive(b);
            creditsShown = b;
        }
        if (b){
            EventSystem.current.SetSelectedGameObject(null);
            showMenu(false);
        }
        else{
            showMenu(true);
        }

    }

    public bool IsMenuActive()
    {
        return isMenuShown;
    }

    public bool IsCreditsActive(){
        return creditsShown;
    }

    public void RestartGameHandler()
    {
        gameController.restartGame();
    }

    public void CreditsMenuHandler()
    {
        creditsPanel.SetActive(true);
    }

    public void ExitToDesktopHandler()
    {
        gameController.exitGame();
    }

}

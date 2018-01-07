using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameMenuController : MonoBehaviour {

    private GameObject deadPanel;
    private GameController gameController;
    private AudioController audioController;
    private bool isMenuShown;
    

	// Use this for initialization
	public void Init() {
        gameController = GameObject.Find("Controllers").GetComponent<GameController>();
        deadPanel = GameObject.Find("DeadPanel");
        isMenuShown = false;
        deadPanel.SetActive(isMenuShown);
        audioController = GameObject.Find("Music").GetComponent<AudioController>();

    }

    public void showMenu( bool b )
    {
        if (isMenuShown != b)
        {
            deadPanel.SetActive(b);
            isMenuShown = b;
        }
        if (b) 
	{
            EventSystem.current.SetSelectedGameObject(GameObject.Find("RestartGameBtn"));
            audioController.EnterMenu();
	}
        else
            EventSystem.current.SetSelectedGameObject(null);
    }

    public bool IsMenuActive()
    {
        return isMenuShown;
    }

    public void RestartGameHandler()
    {
        gameController.restartGame();
    }

    public void ExitToGameMenuHandler()
    {
        Debug.Log("Main menu not implemented yet!");
    }

    public void ExitToDesktopHandler()
    {
        gameController.exitGame();
    }

}

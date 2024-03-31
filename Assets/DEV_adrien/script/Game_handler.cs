using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game_handler : MonoBehaviour
{
    [Header("References")]
    private GameObject[] buttonLevel;
    public GameObject endGamePannel;
    public GameObject cam;
    public GameObject transitionScreen;
    public Texture2D normalCursor;
    public Texture2D hoverCursor;
    private bool isOverCursor;

    void Start(){
        Cursor.SetCursor(normalCursor, new Vector2(5,5), CursorMode.Auto);
        isOverCursor = false;
    }

    public void updateLevelScreen() { //called when loading the level selection screen

        //reset the playerpref if it have a problem
        if(PlayerPrefs.GetFloat("unlockedLevel") == 0) PlayerPrefs.SetFloat("unlockedLevel", 1);

        //Load every level that are unlocked (could have used directly the playerPref, yeah)
        buttonLevel = GameObject.FindGameObjectsWithTag("LevelButton");
        for(int i = 0; i < buttonLevel.Length; i++) {
            if(i < PlayerPrefs.GetFloat("unlockedLevel")) buttonLevel[i].SetActive(true);
            else buttonLevel[i].SetActive(false);
        } 
    }

    public void loadLevel(int i) { //called by the level selection menu

        //smooth transition, so wait a litle
        StartCoroutine(transition());
        StartCoroutine(loadLevelRoutine(0.55f, i));

        //reset the time, in case we where in pause
        Time.timeScale = 1;
    }

    public void loadNextLevel() { //same, but with a different index (called at the end of a level)

        StartCoroutine(transition());
        StartCoroutine(loadLevelRoutine(0.55f, SceneManager.GetActiveScene().buildIndex + 1));
    }

    public void pauseGame() {
        Time.timeScale = 0;
    }

    public void unPauseGame() {
        Time.timeScale = 1;
    }

    public void resetLevel() { //reload safely the scene
        StartCoroutine(transition());
        StartCoroutine(loadLevelRoutine(0.55f, SceneManager.GetActiveScene().buildIndex));
        Time.timeScale = 1;
    }

    public void returnToMenu() { //called by menu buttons of levels

        //smooth transition
        StartCoroutine(transition());
        StartCoroutine(loadLevelRoutine(0.535f, 0));

        //usual safety resets
        Time.timeScale = 1;
    }

    public void signalLevelFinished() { //called when a level is finished

        //unlock a new level when we are playing the lastest level
        if(SceneManager.GetActiveScene().buildIndex == PlayerPrefs.GetFloat("unlockedLevel"))
            PlayerPrefs.SetFloat("unlockedLevel", SceneManager.GetActiveScene().buildIndex + 1);

        //Update the ui
        endGamePannel.SetActive(true);

        //Pause the game
        Time.timeScale = 0;
    }

    IEnumerator loadLevelRoutine(float timer, int index) {
        yield return new WaitForSecondsRealtime(timer);
        Time.timeScale = 1;
        SceneManager.LoadScene(index);
    }

    IEnumerator transition(float delay = 0) { //create a big black transition screen effect
        Debug.Log("reached");
        yield return new WaitForSeconds(delay);
        Instantiate(transitionScreen, cam.transform.position + new Vector3(22,0,10), Quaternion.Euler(new Vector3(0,0,180)));
    }

    public void OpenLink(string url) { //used by credits and about section for url
        Application.OpenURL(url);
    }

    public void closeGame() {
        Application.Quit();
    }

    public void swapCursor() { // Swap the state of the cursor
        if(isOverCursor) {
            Cursor.SetCursor(normalCursor, new Vector2(5,5), CursorMode.Auto);
            isOverCursor = false;
        } 
        else {
            Cursor.SetCursor(hoverCursor, new Vector2(5,5), CursorMode.Auto);
            isOverCursor = true;
        }
    }

    public void swapCursor(bool newState) { // Force the cursor into the desired state
        isOverCursor = !newState;
        swapCursor();
    }

    //Some editor commands for testing
    
    [ContextMenu("Add Unlocked Level")]
    public void editorAddUnlockedLevel() {
        PlayerPrefs.SetFloat("unlockedLevel", PlayerPrefs.GetFloat("unlockedLevel") + 1);
        Debug.Log(PlayerPrefs.GetFloat("unlockedLevel"));
    }

    [ContextMenu("Remove Unlocked Level")]
    public void editorRemoveUnlockedLevel() {
        PlayerPrefs.SetFloat("unlockedLevel", PlayerPrefs.GetFloat("unlockedLevel") - 1);
        Debug.Log(PlayerPrefs.GetFloat("unlockedLevel"));
    }

    [ContextMenu("Reset Unlocked Level")]
    public void editorResetUnlockedLevel() {
        PlayerPrefs.DeleteKey("unlockedLevel");
        Debug.Log("unlocked level reset");
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour
{
    public string Intro = "TestScene1";
    public string FirstGameScene = "TestScene1";

    // Method to be called when the 'Play' button is clicked
    public void OnPlayButtonClick()
    {
        // Load the cutscene scene
        SceneManager.LoadScene(Intro);
    }

    public void OnExitButtonClick()
    {
        // Quit the game
        Application.Quit();
    }

    // Method to be called when the cutscene finishes (you might call this from an event, animation event, etc.)
    public void OnCutsceneFinished()
    {
        // Load the next scene after the cutscene finishes
        SceneManager.LoadScene(FirstGameScene);
    }

}

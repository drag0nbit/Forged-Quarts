using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuButtonHandler : MonoBehaviour
{
    // Variables for the buttons
    public Button playButton;
    public Button optionsButton;
    public Button researchButton;

    void Start()
    {
        optionsButton.interactable = false;
        researchButton.interactable = false;
    }

    // Method called when the Play button is pressed
    public void PlayButtonPressed()
    {
        SceneManager.LoadScene("testPlay");
    }
}

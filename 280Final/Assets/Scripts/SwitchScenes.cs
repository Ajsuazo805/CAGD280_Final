using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * Author: [Suazo, Angel]
 * Last Updated: [05/09/2024]
 * [Script to switch scenes and quit application]
 */
public class SwitchScenes : MonoBehaviour
{
    //button for quiting game
    public void QuitGame()
    {
        Application.Quit();
    }

    //function for canvas button to switch scenes
    public void SwitchScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    // Update is called once per frame
    void Update()
    {

    }
}

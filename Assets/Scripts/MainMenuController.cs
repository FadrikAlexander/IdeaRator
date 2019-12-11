using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//This Class will be called from buttons and keys
public class MainMenuController : MonoBehaviour
{
	//Change the Keys in the inspector 
    [SerializeField]
    KeyCode QuitKey = KeyCode.Escape;
    void Update()
    {
        if (Input.GetKeyDown(QuitKey))
            Quit();
    }

	//use this to quit the game called from Buttons
    public void Quit()
    {
        Application.Quit();
    }

	//use this to surf the net
	//Add the URL as a string in the Button Call
    public void GoToUrl(string URL)
    {
		//when opening while playing WebGl we have to open a new tab in the browser
        if (Application.platform == RuntimePlatform.WebGLPlayer)
            Application.ExternalEval("window.open(\"" + URL + " \",\"_blank\")");
        else
            Application.OpenURL(URL);
    }
}

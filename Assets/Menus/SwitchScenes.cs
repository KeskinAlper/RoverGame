using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScenes : MonoBehaviour
{
    public GameStateObject gamestate;
    public string scenename;
    public void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    public void SwitchScene()
    {
        SceneManager.LoadScene(scenename);
        gamestate.makingnewgame = false;
    }
    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.J))
        {
            SwitchScene();
        }

    }
}

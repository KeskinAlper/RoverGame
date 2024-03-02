using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewGameLoadGame : MonoBehaviour
{
    public GameStateObject gameState;
    public void NewGame()
    {
        gameState.makingnewgame = true;
        SceneManager.LoadScene("Map");
    }
    public void LoadGame()
    {
        gameState.makingnewgame = false;
        SceneManager.LoadScene("Map");
    }
}

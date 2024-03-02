using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using UnityEngine.SceneManagement;

public class PlayerSaverLoader : MonoBehaviour
{
    public GameStateObject gameState;
    public float[] pos = new float[3];
    private IDataService DataService = new JsonDataService();
    private void Start()
    {

        if (gameState.makingnewgame == false)
        {
            pos = DataService.LoadData<float[]>("/playerpos.json", false);
            gameObject.transform.SetPositionAndRotation(new Vector3(pos[0], pos[1], pos[2]), Quaternion.identity);
            Debug.Log("Loaded pos");
        }
    }
    
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            pos[0] = gameObject.transform.position.x;
            pos[1] = gameObject.transform.position.y;
            pos[2] = gameObject.transform.position.z;
            DataService.SaveData<float[]>("/playerpos.json", pos , false);
            SceneManager.LoadScene("Map");
        }
    }
}

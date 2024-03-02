using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextDestination : MonoBehaviour
{
    public string[] chosenmaps;
    public SwitchScenes deploy;
    public int mapindex;
    public GameStateObject gameState;
    public GameStateObject sceneState;
    private IDataService dataService = new JsonDataService();
    void Awake()
    {
        if (gameState.makingnewgame == true)
        {
            deploy.scenename = chosenmaps[0];
            mapindex = 0;
            sceneState.makingnewgame = true;
        }
        else
        {
            chosenmaps = dataService.LoadData<string[]>("/chosenmaps", false);
            deploy.scenename = dataService.LoadData<string>("/currentdeploymap", false);
            mapindex = dataService.LoadData<int>("/mapindex", false);
            sceneState.makingnewgame = false;
        }
    }


    private void OnDisable()
    {
        dataService.SaveData<string[]>("/chosenmaps", chosenmaps, false);
        dataService.SaveData<string>("/currentdeploymap", deploy.scenename, false);
        dataService.SaveData<int>("/mapindex", mapindex, false);
    }
    public void GoNext()
    {
        mapindex++;
        deploy.scenename = chosenmaps[mapindex];
        sceneState.makingnewgame = true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuTrigger : MonoBehaviour
{
    GameStatus gameStatus;
    ScenePersistance scenePersistance;
    SceneLoader sceneLoader;

    // Start is called before the first frame update
    void Start()
    {
        sceneLoader = FindObjectOfType<SceneLoader>();
        gameStatus = FindObjectOfType<GameStatus>();
        scenePersistance = FindObjectOfType<ScenePersistance>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        scenePersistance.ResetMemory();
        gameStatus.Restart();
        sceneLoader.LoadMenu();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    GameStatus gameStatus;
    ScenePersistance scenePersistance;

    // Start is called before the first frame update
    void Start()
    {
        gameStatus = FindObjectOfType<GameStatus>();
        scenePersistance = FindObjectOfType<ScenePersistance>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        gameStatus.IncKeys();
        scenePersistance.MemorizeItem(gameObject);
        Destroy(gameObject);
    }
}

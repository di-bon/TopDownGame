using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    Animator myAnimator;
    GameStatus gameStatus;
    ScenePersistance scenePersistance;

    // Start is called before the first frame update
    void Start()
    {
        myAnimator = GetComponent<Animator>();
        gameStatus = FindObjectOfType<GameStatus>();
        scenePersistance = FindObjectOfType<ScenePersistance>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        bool openDoor = gameStatus.keys > 0;
        if (openDoor)
        {
            myAnimator.SetBool("openDoor", openDoor);
        }
    }

    public void DoorDestroyer()
    {
        gameStatus.DecKeys();
        scenePersistance.MemorizeItem(gameObject);
        Destroy(gameObject);
    }
}

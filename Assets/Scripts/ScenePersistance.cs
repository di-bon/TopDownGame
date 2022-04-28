using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenePersistance : MonoBehaviour
{

    private int startingSceneIndex;
    public List<string> objectsToDestroy = new List<string>();

    private void Awake()
    {
        //Singleton
        int scenePersistanceCount = FindObjectsOfType<ScenePersistance>().Length;
        if (scenePersistanceCount > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
            DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        startingSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    // Update is called once per frame
    void Update()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (currentSceneIndex != startingSceneIndex)
            DontDestroyOnLoad(gameObject);
    }

    public void MemorizeItem(GameObject objectToRemember)
    {
        objectsToDestroy.Add(objectToRemember.name);
    }

    public void DeleteObjects()
    {
        for (int i = 0; i < objectsToDestroy.Count; i++)
        {
            Destroy(GameObject.Find(objectsToDestroy[i]));
        }
    }

    public void ResetMemory()
    {
        objectsToDestroy = new List<string>();
    }
}

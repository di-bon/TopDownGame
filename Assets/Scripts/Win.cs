using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Win : MonoBehaviour
{
    [SerializeField] float timeToWait = 1.5f;
    [SerializeField] float levelExitSlowMotionFactor = 0.4f;

    SceneLoader sceneLoader;
    ScenePersistance scenePersistance;

    // Start is called before the first frame update
    void Start()
    {
        sceneLoader = FindObjectOfType<SceneLoader>();
        scenePersistance = FindObjectOfType<ScenePersistance>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine(LoadNextLevel());
    }

    IEnumerator LoadNextLevel()
    {
        Time.timeScale = levelExitSlowMotionFactor;
        yield return new WaitForSecondsRealtime(timeToWait);
        Time.timeScale = 1F;
        scenePersistance.ResetMemory();
        sceneLoader.LoadNextScene();
    }
}

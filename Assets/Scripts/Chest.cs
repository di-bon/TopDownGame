using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Chest : MonoBehaviour
{
    ScenePersistance scenePersistance;
    Animator animator;

    [SerializeField] List<GameObject> istantiableObjects = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        scenePersistance = FindObjectOfType<ScenePersistance>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (CrossPlatformInputManager.GetButtonDown("Interaction"))
        {
            animator.SetBool("wantToOpen", true);
        }
    }

    public void ChestDestroyer()
    {
        Instantiate(istantiableObjects[Random.Range(0, istantiableObjects.Count)], transform.position, transform.rotation);
        scenePersistance.MemorizeItem(gameObject);
        Destroy(gameObject);
    }
}
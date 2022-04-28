using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] GameObject explosionToInstantiate;

    ScenePersistance scenePersistance;
    CircleCollider2D circleCollider2D;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        circleCollider2D = GetComponent<CircleCollider2D>();
        animator = GetComponent<Animator>();
        scenePersistance = FindObjectOfType<ScenePersistance>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (circleCollider2D.IsTouchingLayers(LayerMask.GetMask("Player")))
        {
            animator.SetBool("isTriggered", true);
        }
    }

    public void BombDestroyer()
    {
        Instantiate(explosionToInstantiate, transform.position, transform.rotation);
        scenePersistance.MemorizeItem(gameObject);
        Destroy(gameObject);
    }
}

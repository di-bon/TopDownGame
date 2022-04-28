using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPotion : MonoBehaviour
{
    [SerializeField] float effectTime = 7.5f;
    [SerializeField] float newRunSpeed = 8f;

    Player player;
    BoxCollider2D potionCollider;
    SpriteRenderer potionSpriteRenderer;

    float previousRunSpeed;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        potionCollider = GetComponent<BoxCollider2D>();
        potionSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DisablePotion();
        ChangeSpeed();
    }

    private void DisablePotion()
    {
        potionCollider.enabled = !potionCollider.enabled;
        potionSpriteRenderer.sprite = null;
    }

    private void ChangeSpeed()
    {
        StartCoroutine(WaitingCoroutine());
    }

    IEnumerator WaitingCoroutine()
    {
        previousRunSpeed = player.runSpeed;
        player.runSpeed = newRunSpeed;
        yield return new WaitForSeconds(effectTime);
        player.runSpeed = previousRunSpeed;
        Destroy(gameObject);
    }
}

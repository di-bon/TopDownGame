using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisibilityPotion : MonoBehaviour
{
    [SerializeField] float effectTime = 3f;

    Player player;
    SpriteRenderer playerSpriteRenderer;
    BoxCollider2D potionCollider;
    SpriteRenderer potionSpriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        playerSpriteRenderer = player.GetComponent<SpriteRenderer>();
        potionCollider = GetComponent<BoxCollider2D>();
        potionSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DisablePotion();
        SetInvisible();
    }

    private void DisablePotion()
    {
        potionCollider.enabled = !potionCollider.enabled;
        potionSpriteRenderer.sprite = null;
    }

    private void SetInvisible()
    {
        StartCoroutine(InvisibleCoroutine());
    }

    IEnumerator InvisibleCoroutine()
    {
        player.isInvisible = true;
        playerSpriteRenderer.color = new Color(playerSpriteRenderer.color.r, playerSpriteRenderer.color.g, playerSpriteRenderer.color.b, playerSpriteRenderer.color.a / 2);
        yield return new WaitForSeconds(effectTime);
        player.isInvisible = false;
        playerSpriteRenderer.color = new Color(playerSpriteRenderer.color.r, playerSpriteRenderer.color.g, playerSpriteRenderer.color.b, playerSpriteRenderer.color.a * 2);
        Destroy(gameObject);
    }
}

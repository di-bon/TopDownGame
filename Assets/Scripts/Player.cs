using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    public bool isInvisible = false;
    private bool isAlive = true;
    private Vector2 death = new Vector2(0f, 0f);

    [SerializeField] public float runSpeed = 5f;

    Rigidbody2D myRigidbody2D;
    Animator myAnimator;
    Collider2D myCollider2D;
    SpriteRenderer mySpriteRenderer;
    Spikes spikes;
    GameStatus gameStatus;

    // Start is called before the first frame update
    void Start()
    {
        gameStatus = FindObjectOfType<GameStatus>();
        myRigidbody2D = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myCollider2D = GetComponent<Collider2D>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        spikes = FindObjectOfType<Spikes>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isAlive) return;

        Run();
        FlipSprite();
        Die();
    }

    private void FlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody2D.velocity.x) > Mathf.Epsilon;
        if (playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(myRigidbody2D.velocity.x), 1f);
        }
    }

    private void Run()
    {
        float xAxis = CrossPlatformInputManager.GetAxis("Horizontal");
        float yAxis = CrossPlatformInputManager.GetAxis("Vertical");
        Vector2 playerVelocity = new Vector2(xAxis * runSpeed, yAxis * runSpeed);
        myRigidbody2D.velocity = playerVelocity;

        bool playerHasSpeed = Mathf.Abs(myRigidbody2D.velocity.x) > Mathf.Epsilon || Mathf.Abs(myRigidbody2D.velocity.y) > Mathf.Epsilon;
        myAnimator.SetBool("isRunning", playerHasSpeed);
    }

    public void Die()
    {
        if (((myCollider2D.IsTouchingLayers(LayerMask.GetMask("Enemy")) || myCollider2D.IsTouchingLayers(LayerMask.GetMask("Spikes")) && spikes.IsEnabled()) && !isInvisible) || myCollider2D.IsTouchingLayers(LayerMask.GetMask("InstantKill")))
        {
            isAlive = false;
            myCollider2D.enabled = false;
            myRigidbody2D.velocity = death;
            myAnimator.SetBool("isRunning", false);
            StartCoroutine(DyingSpriteChange());
            StartCoroutine(WaitBeforeDie());
        }
    }

    IEnumerator WaitBeforeDie()
    {
        yield return new WaitForSeconds(1.5f);
        gameStatus.ProcessPlayerDeath();
    }

    IEnumerator DyingSpriteChange()
    {
        for (int i = 0; i < 10; i++)
        {
            mySpriteRenderer.color = new Color(mySpriteRenderer.color.r, mySpriteRenderer.color.g, mySpriteRenderer.color.b, mySpriteRenderer.color.a / 2);
            yield return new WaitForSeconds(0.15f);
        }
    }
}

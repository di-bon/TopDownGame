using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 2.5f;
    [SerializeField] bool isMoving = true;

    Rigidbody2D myRigidbody2D;
    Animator myAnimator;

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myAnimator.SetBool("isRunning", isMoving);
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving)
            myRigidbody2D.velocity = new Vector2(IsFacingRight() ? moveSpeed : -moveSpeed, 0);
        else
            myRigidbody2D.velocity = new Vector2(Mathf.Epsilon, Mathf.Epsilon);
        myAnimator.SetBool("isRunning", isMoving);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        transform.localScale = new Vector2(-Mathf.Sign(myRigidbody2D.velocity.x), 1f);
    }

    private bool IsFacingRight()
    {
        return transform.localScale.x > 0;
    }
}

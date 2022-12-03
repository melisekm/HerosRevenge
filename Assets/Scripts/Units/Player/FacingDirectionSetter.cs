using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class FacingDirectionSetter : MonoBehaviour
{
    public bool isFacingRight;
    private Rigidbody2D rb;
    private SpriteRenderer sprite;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    private void SetFacingDirection()
    {
        if (rb.velocity.x >= 0.01f)
        {
            if (!isFacingRight)
            {
                Flip();
            }
        }
        else if (rb.velocity.x <= -0.01f)
        {
            if (isFacingRight)
            {
                Flip();
            }
        }
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        sprite.flipX = !sprite.flipX;
        // this only flips sprite, not the collider etc
    }

    protected void Update()
    {
        SetFacingDirection();
    }
}
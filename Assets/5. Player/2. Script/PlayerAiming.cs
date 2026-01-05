using UnityEngine;

public class PlayerAiming : MonoBehaviour
{
    [SerializeField] private Transform LeftArm;
    [SerializeField] private Transform RightArm;
    [SerializeField] private SpriteRenderer DotLine;
    private bool IsFacingRight = true;

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            StartAiming();
        }

        if (Input.GetMouseButtonUp(0))
        {
            DotLine.enabled = false;
        }
    }

    private void StartAiming()
    {
        DotLine.enabled = true;
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePosition - (Vector2)transform.position).normalized;
        float angle;

        if (IsFacingRight)
        {
            angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        }
        else
        {
            angle = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;
        }

        LeftArm.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        RightArm.rotation = Quaternion.Euler(new Vector3(0, 0, angle / 2));

        if (mousePosition.x < transform.position.x && IsFacingRight ||
            mousePosition.x > transform.position.x && !IsFacingRight)
        {
            FlipPlayer();
        }
    }

    private void FlipPlayer()
    {
        IsFacingRight = !IsFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }
}

using UnityEngine;

public class PlayerAiming : MonoBehaviour
{
    [SerializeField] private Transform LeftArm;
    [SerializeField] private Transform RightArm;
    [SerializeField] private LineRenderer DotLine;
    private float DotLineLength = 10f;
    private bool IsFacingRight = true;

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            StartAiming();
            ShowDotLine();
        }
    }

    private void StartAiming()
    {
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

    private void ShowDotLine()
    {
        DotLine.enabled = true;
        DotLine.positionCount = 2;
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePosition - (Vector2)LeftArm.position).normalized;
        DotLine.SetPosition(0, LeftArm.position);
        DotLine.SetPosition(1, mousePosition + direction * DotLineLength);
    }
}

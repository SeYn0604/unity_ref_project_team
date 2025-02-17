using UnityEngine;

public class Runner : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;
    public float speed = 5f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        // 시작 시 강제로 run 애니메이션 재생
        animator.Play("run");
    }

    void Update()
    {
        float move = Input.GetAxis("Horizontal");

        // 좌우 이동
        rb.velocity = new Vector2(move * speed, rb.velocity.y);

        // 방향 전환
        if (move > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (move < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }
}

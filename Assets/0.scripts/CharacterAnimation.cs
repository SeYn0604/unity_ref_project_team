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

        // 시작 시 run 애니메이션 재생
        //animator.Play("run", 0);
    }

    void Update()
    {
        /*      
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
        */ //캐릭터 수동이동, 생각해보니 단방향 자동이동으로 짜면 더 편할듯
        // 무조건 오른쪽으로 자동 이동
        rb.velocity = new Vector2(speed, rb.velocity.y);
    }
}

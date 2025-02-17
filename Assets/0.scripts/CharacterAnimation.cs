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

        // ���� �� run �ִϸ��̼� ���
        //animator.Play("run", 0);
    }

    void Update()
    {
        /*      
        float move = Input.GetAxis("Horizontal");

        // �¿� �̵�
        rb.velocity = new Vector2(move * speed, rb.velocity.y);

        // ���� ��ȯ
        if (move > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (move < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        */ //ĳ���� �����̵�, �����غ��� �ܹ��� �ڵ��̵����� ¥�� �� ���ҵ�
        // ������ ���������� �ڵ� �̵�
        rb.velocity = new Vector2(speed, rb.velocity.y);
    }
}

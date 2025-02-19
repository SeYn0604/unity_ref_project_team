using UnityEngine;

public class AICharacter : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;

    [Header("�̵� ����")]
    public float speed = 5f;                             // AI�� �׻� �� �ӵ��� �̵� 

    [Header("�浹 �ִϸ��̼� ����")]
    public string loseAnimationName = "lose";            // AI �й� �ִϸ��̼�

    // �浹 �߻� �� �̵� ������ ���� �÷���
    private bool collisionOccurred = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        rb.velocity = new Vector2(speed,rb.velocity.y);

        // �浹 �߻� �Ŀ��� �̵� ������Ʈ�� �ߴ�
        if (collisionOccurred)
        {
            rb.velocity = Vector2.zero;
            return;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // �÷��̾�� �浹 �� AI �й� ó��
        if (collision.gameObject.CompareTag("player") && !collisionOccurred)
        {
            collisionOccurred = true;
            rb.velocity = Vector2.zero;
            animator.Play(loseAnimationName);
        }

        // ��ֹ����� �浹 ó�� (�ʿ� ��)
        if (collision.gameObject.CompareTag("Obstacles"))
        {
            collisionOccurred = true;
            rb.velocity = Vector2.zero;
        }
    }
    /*
    // �ܺ� ȣ��� AI�� �̵��� ���߱� ���� �޼���
    public void StopMovement()
    {
        collisionOccurred = true;
        speed = 0f;
        if (rb != null)
            rb.velocity = Vector2.zero;
    }*/
}

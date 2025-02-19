using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;

    [Header("�̵�/���� ����")]
    public float speed = 5f;                            // �ڵ� �̵� �ӵ�
    public float jumpForce = 10f;                       // ���� �� ���� ������ ��
    private bool isGrounded = false;                    // ���� ����ִ��� üũ
    [Header("���� ���� ����")]
    public int maxJumpCount = 2;                        // �ִ� ���� Ƚ�� (2�� ����)
    private int jumpCount = 0;                          // ���� ���� Ƚ��
    [Header("�浹 �ִϸ��̼� ����")]
    public string victoryAnimationName = "victory";     // �÷��̾ ai�� ������ �� ����� �ִϸ��̼�
    public string loseAnimationName = "lose";           // ai�� �÷��̾�� ������ �� ����� �ִϸ��̼�
    [Header("AI ������Ʈ ����")]                         // ai ���� �̵��ӵ� 0 ������
    public GameObject aiObject;

    // �浹 ó���� �� ���� ����ǵ��� �ϴ� �÷���
    private bool collisionOccurred = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        // ���� �� run �ִϸ��̼� ��� (�ʿ��ϸ� �ּ� ����)
        // animator.Play("run", 0);
    }

    void Update()
    {
        // ���������� �ڵ� �̵�
        rb.velocity = new Vector2(speed, rb.velocity.y);
        // ���� �Է�
        if(gameObject.CompareTag("player"))
        {
            if (Input.GetKeyDown(KeyCode.Space) && jumpCount < maxJumpCount && !collisionOccurred)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                jumpCount++;      // ���� ī��Ʈ ����
                isGrounded = false;
            }
        }
        // ���� ���� �� �ִϸ��̼� ����, ���� �� �簳
        if (!isGrounded)
        {
            animator.speed = 0f;
        }
        else
        {
            animator.speed = 1f;
        }
    }

    // �÷��̾�� AI�� �浹�ϸ� ȣ��
    void OnCollisionEnter2D(Collision2D collision)
    {
        // �ٴڿ� ������ isGrounded�� true�� ����
        if(collision.gameObject.CompareTag("ground"))
        {
            isGrounded = true;
        }
        // �浹�� ������Ʈ�� Tag�� "ai"���� Ȯ�� (�ҹ��� "ai" ���)
        if (collision.gameObject.CompareTag("ai"))
        {
            // �÷��̾� �¸� �ִϸ��̼� ��� �� �̵� ����
            animator.Play(victoryAnimationName);
            speed = 0f;
            rb.velocity = Vector2.zero;

            StartCoroutine(DelayedAIDeath(collision.gameObject, 0f));
        }
    }

    // AI ��� �ִϸ��̼��� ���� �� �����ϴ� �ڷ�ƾ
    IEnumerator DelayedAIDeath(GameObject aiObject, float delay)
    {
        yield return new WaitForSeconds(delay);

        Character aiRunner = aiObject.GetComponent<Character>();

        // AI ������Ʈ�� Animator ��������
        Animator aiAnimator = aiObject.GetComponent<Animator>();
        if (aiAnimator != null)
        {
            aiAnimator.Play(loseAnimationName);
            aiRunner.speed = 0f;
        }
    }
}

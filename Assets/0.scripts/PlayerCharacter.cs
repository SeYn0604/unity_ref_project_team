using UnityEngine;
using System.Collections;

public class PlayerCharacter : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;          // ��������Ʈ ������Ʈ ����

    [Header("�̵�/���� ����")]
    public float startSpeed = 5f;                   // ���� �ӵ�
    public float accelerationRate = 2f;             // �ʴ� �ӵ� ������
    public float maxSpeed = 7f;                     // �ִ� �ӵ�
    private float currentSpeed;                     // ���� �̵� �ӵ�
    public float jumpForce = 10f;                   // ���� �� ���� ������ ��
    private bool isGrounded = false;                // ���� ��Ҵ��� üũ

    [Header("�浹 �ִϸ��̼� ����")]
    public string victoryAnimationName = "victory"; // �÷��̾� �¸� �ִϸ��̼�

    [Header("���� ���� ����")]
    public int maxJumpCount = 2;                    // �ִ� ���� Ƚ�� (2�� ����)
    private int jumpCount = 0;                      // ���� ���� Ƚ��

    [Header("�浹 ��ȿȭ �ð� ����")]
    public float ignoreDuration = 2f;               // �浹 ��ȿȭ �ð�(��)

    // AI���� �浹 �߻� �� ����
    private bool aiCollisionOccurred = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>(); // SpriteRenderer �ʱ�ȭ
        currentSpeed = startSpeed;
    }

    void Update()
    {
        // AI���� �浹�� �߻��ϸ� ���� ����
        if (aiCollisionOccurred)
        {
            rb.velocity = Vector2.zero;
            return;
        }

        // ���ӵ� ����
        currentSpeed += accelerationRate * Time.deltaTime;
        if (currentSpeed > maxSpeed)
            currentSpeed = maxSpeed;

        rb.velocity = new Vector2(currentSpeed, rb.velocity.y);

        // ���� 
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < maxJumpCount)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpCount++;
            isGrounded = false;
        }

        // ���� ���� �� �ִϸ��̼� ����, ���� �� �簳
        animator.speed = isGrounded ? 1f : 0f;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // ���� �浹�ϸ� ���� Ƚ�� �ʱ�ȭ
        if (collision.gameObject.CompareTag("ground"))
        {
            isGrounded = true;
            jumpCount = 0;
        }

        // �÷��̾ AI�� �浹�ϸ� ���� ���� ó��
        if (collision.gameObject.CompareTag("ai") && !aiCollisionOccurred)
        {
            aiCollisionOccurred = true;

            // �¸� �ִϸ��̼� ��� �� ���� ����
            animator.Play(victoryAnimationName);
            currentSpeed = 0f;
            rb.velocity = Vector2.zero;
        }

        // �÷��̾ ��ֹ��� �浹�ϸ� �Ͻ������� ����
        if (collision.gameObject.CompareTag("Obstacles"))
        {
            // �Ͻ� ���� �ڷ�ƾ ����
            StartCoroutine(TemporaryStop());

            // �浹 ������ ���� �ð� ��ȿȭ
            Collider2D playerCollider = GetComponent<Collider2D>();
            Collider2D obstacleCollider = collision.collider;
            StartCoroutine(DisableCollisionForSeconds(playerCollider, obstacleCollider, ignoreDuration));

            // ��ȿȭ �ð� ���� ��������Ʈ ������ ȿ�� ����
            StartCoroutine(BlinkSprite(ignoreDuration, 0.1f));
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ground"))
            isGrounded = false;
    }

    // ��ֹ� �浹 �� �Ͻ� ���� �� ȸ���ϴ� �ڷ�ƾ
    IEnumerator TemporaryStop()
    {
        // ����� �ӵ��� ������ �ʿ䰡 �ִٸ� ���⼭ �ӽ� ����
        float savedSpeed = currentSpeed;
        currentSpeed = 2f;
        rb.velocity = Vector2.zero;

        yield return new WaitForSeconds(ignoreDuration);

        // �Ͻ� ���� �� �ٽ� �⺻ �ӵ�(�Ǵ� ���ϴ� �ӵ�)�� ȸ��
        currentSpeed = startSpeed;
    }

    // �÷��̾�� ��ֹ� �� �浹 ������ ���� �ð� ��ȿȭ�ϴ� �ڷ�ƾ
    IEnumerator DisableCollisionForSeconds(Collider2D col1, Collider2D col2, float duration)
    {
        Physics2D.IgnoreCollision(col1, col2, true);
        yield return new WaitForSeconds(duration);
        Physics2D.IgnoreCollision(col1, col2, false);
    }

    // ��ȿȭ �ð� ���� ��������Ʈ ������ ȿ���� �ִ� �ڷ�ƾ
    IEnumerator BlinkSprite(float duration, float blinkInterval)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            spriteRenderer.enabled = false;
            yield return new WaitForSeconds(blinkInterval);
            spriteRenderer.enabled = true;
            yield return new WaitForSeconds(blinkInterval);
            elapsed += blinkInterval * 2;
        }
        // ������ ���� �� ��������Ʈ�� Ȱ�� �������� Ȯ��
        spriteRenderer.enabled = true;
    }
}

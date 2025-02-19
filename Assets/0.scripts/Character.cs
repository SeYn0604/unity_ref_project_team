using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;

    [Header("이동/점프 설정")]
    public float speed = 5f;                            // 자동 이동 속도
    public float jumpForce = 10f;                       // 점프 시 위로 가해질 힘
    private bool isGrounded = false;                    // 땅에 닿아있는지 체크
    [Header("더블 점프 설정")]
    public int maxJumpCount = 2;                        // 최대 점프 횟수 (2단 점프)
    private int jumpCount = 0;                          // 현재 점프 횟수
    [Header("충돌 애니메이션 설정")]
    public string victoryAnimationName = "victory";     // 플레이어가 ai랑 겹쳤을 때 출력할 애니메이션
    public string loseAnimationName = "lose";           // ai가 플레이어랑 겹쳤을 때 출력할 애니메이션
    [Header("AI 오브젝트 참조")]                         // ai 전용 이동속도 0 구현용
    public GameObject aiObject;

    // 충돌 처리가 한 번만 실행되도록 하는 플래그
    private bool collisionOccurred = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        // 시작 시 run 애니메이션 재생 (필요하면 주석 해제)
        // animator.Play("run", 0);
    }

    void Update()
    {
        // 오른쪽으로 자동 이동
        rb.velocity = new Vector2(speed, rb.velocity.y);
        // 점프 입력
        if(gameObject.CompareTag("player"))
        {
            if (Input.GetKeyDown(KeyCode.Space) && jumpCount < maxJumpCount && !collisionOccurred)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                jumpCount++;      // 점프 카운트 증가
                isGrounded = false;
            }
        }
        // 점프 중일 때 애니메이션 정지, 착지 시 재개
        if (!isGrounded)
        {
            animator.speed = 0f;
        }
        else
        {
            animator.speed = 1f;
        }
    }

    // 플레이어와 AI가 충돌하면 호출
    void OnCollisionEnter2D(Collision2D collision)
    {
        // 바닥에 닿으면 isGrounded를 true로 설정
        if(collision.gameObject.CompareTag("ground"))
        {
            isGrounded = true;
        }
        // 충돌한 오브젝트의 Tag가 "ai"인지 확인 (소문자 "ai" 사용)
        if (collision.gameObject.CompareTag("ai"))
        {
            // 플레이어 승리 애니메이션 재생 및 이동 정지
            animator.Play(victoryAnimationName);
            speed = 0f;
            rb.velocity = Vector2.zero;

            StartCoroutine(DelayedAIDeath(collision.gameObject, 0f));
        }
    }

    // AI 사망 애니메이션을 지연 후 실행하는 코루틴
    IEnumerator DelayedAIDeath(GameObject aiObject, float delay)
    {
        yield return new WaitForSeconds(delay);

        Character aiRunner = aiObject.GetComponent<Character>();

        // AI 오브젝트의 Animator 가져오기
        Animator aiAnimator = aiObject.GetComponent<Animator>();
        if (aiAnimator != null)
        {
            aiAnimator.Play(loseAnimationName);
            aiRunner.speed = 0f;
        }
    }
}

using UnityEngine;

public class RedEyeGlow : MonoBehaviour
{
    [Header("�ʼ� ����")]
    public SpriteRenderer spriteRenderer; // Inspector���� ����
    public Transform player;              // �÷��̾�
    public Transform ai;                  // AI (�Ÿ� ��� ���)

    // ���� ���� �� ������ �÷��̾�-AI �� �ʱ� �Ÿ�
    private float initialDistance;

    void Start()
    {
        if (spriteRenderer == null)
        {
            Debug.LogError("[RedEyeGlow] SpriteRenderer�� ������� �ʾҽ��ϴ�.");
            enabled = false;
            return;
        }

        if (player == null || ai == null)
        {
            Debug.LogError("[RedEyeGlow] Player Ȥ�� AI�� ������� �ʾҽ��ϴ�.");
            enabled = false;
            return;
        }

        // ���� ���� �� �÷��̾�� AI�� 2D �Ÿ� ����
        initialDistance = Vector2.Distance(player.position, ai.position);

        // �ʱ� ����: �ִ� �Ÿ����� ���İ� = 0 (���� ����)
        SetAlpha(0f);
    }

    void Update()
    {
        // �÷��̾� Ȥ�� AI�� �����Ǿ����� ��Ȯ��
        if (player == null || ai == null) return;

        // ���� �Ÿ� ����
        float currentDistance = Vector2.Distance(player.position, ai.position);

        // ���İ� ���
        //    - (�ʱ� �Ÿ� - ���� �Ÿ�) / �ʱ� �Ÿ�
        //    - ����������� ���� Ŀ���� ����������
        float alpha = (initialDistance - currentDistance) / initialDistance;

        // 0~1 ������ ����
        alpha = Mathf.Clamp01(alpha);

        // ���İ� ����
        SetAlpha(alpha);

        Debug.Log($"[RedEyeGlow] Distance: {currentDistance:F3}, Alpha: {alpha:F3}");
    }

    private void SetAlpha(float alpha)
    {
        Color c = spriteRenderer.color;
        c.a = alpha;
        spriteRenderer.color = c;
    }
}

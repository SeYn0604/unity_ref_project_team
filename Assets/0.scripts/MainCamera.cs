using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;              // �÷��̾��� Transform
    public float smoothSpeed = 0.125f;    // ī�޶� ���󰡴� �ӵ�
    public Vector3 offset;                // ī�޶�� �÷��̾� ������ �Ÿ�(������)

    private Vector3 velocity = Vector3.zero;

    void LateUpdate()
    {
        if (player == null) return;

        // ��ǥ ��ġ: �÷��̾� ��ġ + ������
        Vector3 targetPosition = player.position + offset;

        // ī�޶� �̵� 
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothSpeed);
    }
}

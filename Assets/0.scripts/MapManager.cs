using UnityEngine;
using System.Collections.Generic;

public class MapManager : MonoBehaviour
{
    [Header("�� ���� ����")]
    public List<GameObject> mapSegments;

    [Tooltip("�� �� ������ ���� ����")]
    public float segmentWidth = 50f;

    [Header("�÷��̾�/ī�޶� ����")]
    public Transform player;

    void Update()
    {
        // �� ������ ���� �÷��̾�� ���ʿ� ������ ������� üũ
        foreach (GameObject segment in mapSegments)
        {
            // ������ ������ �� x ��ǥ�� �÷��̾��� x���� ������
            if (segment.transform.position.x + segmentWidth < player.position.x)
            {
                // ���� �����ʿ� �ִ� ������ ã�Ƽ�, �� �����ʿ� �� ������ ���ġ
                float newX = GetRightmostX();
                Vector3 newPos = segment.transform.position;
                newPos.x = newX;
                segment.transform.position = newPos;
            }
        }
    }

    // ����Ʈ ������ ���� �����ʿ� �ִ� ������ ������ �� x ��ǥ�� ��ȯ
    float GetRightmostX()
    {
        float maxX = float.MinValue;
        foreach (GameObject segment in mapSegments)
        {
            if (segment.transform.position.x > maxX)
            {
                maxX = segment.transform.position.x;
            }
        }
        // ������ �� ��ġ�� maxX + segmentWidth
        return maxX + segmentWidth;
    }
}

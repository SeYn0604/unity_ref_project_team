using UnityEngine;
using System.Collections.Generic;

public class MapManager : MonoBehaviour
{
    [Header("�� ���׸�Ʈ ����")]
    public List<GameObject> mapSegments;

    [Tooltip("�� �� ���׸�Ʈ�� ���� ����")]
    public float segmentWidth = 20f;

    [Header("�÷��̾�/ī�޶� ����")]
    public Transform player;

    void Update()
    {
        // �� ���׸�Ʈ�� ���� �÷��̾�� ���ʿ� ������ ������� üũ
        foreach (GameObject segment in mapSegments)
        {
            // ���׸�Ʈ�� ������ �� x ��ǥ�� �÷��̾��� x���� ������
            if (segment.transform.position.x + segmentWidth < player.position.x)
            {
                // ���� �����ʿ� �ִ� ���׸�Ʈ�� ã�Ƽ�, �� �����ʿ� �� ���׸�Ʈ�� ���ġ
                float newX = GetRightmostX();
                Vector3 newPos = segment.transform.position;
                newPos.x = newX;
                segment.transform.position = newPos;
            }
        }
    }

    // ����Ʈ ������ ���� �����ʿ� �ִ� ���׸�Ʈ�� ������ �� x ��ǥ�� ��ȯ
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

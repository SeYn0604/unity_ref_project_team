using UnityEngine;
using System.Collections.Generic;

public class MapManager : MonoBehaviour
{
    [Header("맵 세그먼트 설정")]
    public List<GameObject> mapSegments;

    [Tooltip("각 맵 세그먼트의 가로 길이")]
    public float segmentWidth = 20f;

    [Header("플레이어/카메라 참조")]
    public Transform player;

    void Update()
    {
        // 각 세그먼트에 대해 플레이어보다 왼쪽에 완전히 벗어났는지 체크
        foreach (GameObject segment in mapSegments)
        {
            // 세그먼트의 오른쪽 끝 x 좌표가 플레이어의 x보다 작으면
            if (segment.transform.position.x + segmentWidth < player.position.x)
            {
                // 가장 오른쪽에 있는 세그먼트를 찾아서, 그 오른쪽에 이 세그먼트를 재배치
                float newX = GetRightmostX();
                Vector3 newPos = segment.transform.position;
                newPos.x = newX;
                segment.transform.position = newPos;
            }
        }
    }

    // 리스트 내에서 가장 오른쪽에 있는 세그먼트의 오른쪽 끝 x 좌표를 반환
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
        // 오른쪽 끝 위치는 maxX + segmentWidth
        return maxX + segmentWidth;
    }
}

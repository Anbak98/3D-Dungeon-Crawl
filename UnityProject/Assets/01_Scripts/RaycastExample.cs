using UnityEngine;

public class RaycastExample : MonoBehaviour
{
    public float rayDistance = 10f; // 레이의 최대 거리
    public LayerMask layerMask;     // 레이캐스트가 충돌을 감지할 레이어

    void Update()
    {
        // 레이캐스트 발사
        RaycastHit hit;
        Vector3 rayDirection = transform.forward * rayDistance;

        // 레이캐스트를 시각적으로 표시
        Debug.DrawRay(transform.position, rayDirection, Color.red);

        // 레이캐스트가 충돌한 오브젝트 출력
        if (Physics.Raycast(transform.position, transform.forward, out hit, rayDistance, layerMask))
        {
            Debug.Log("Hit Object: " + hit.collider.gameObject.name);
        }
    }
}

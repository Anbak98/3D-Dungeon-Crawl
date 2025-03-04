using UnityEngine;

public class RaycastExample : MonoBehaviour
{
    public float rayDistance = 10f; // ������ �ִ� �Ÿ�
    public LayerMask layerMask;     // ����ĳ��Ʈ�� �浹�� ������ ���̾�

    void Update()
    {
        // ����ĳ��Ʈ �߻�
        RaycastHit hit;
        Vector3 rayDirection = transform.forward * rayDistance;

        // ����ĳ��Ʈ�� �ð������� ǥ��
        Debug.DrawRay(transform.position, rayDirection, Color.red);

        // ����ĳ��Ʈ�� �浹�� ������Ʈ ���
        if (Physics.Raycast(transform.position, transform.forward, out hit, rayDistance, layerMask))
        {
            Debug.Log("Hit Object: " + hit.collider.gameObject.name);
        }
    }
}

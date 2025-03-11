using System;
using UnityEngine;

public class InteractableObjectDetector : MonoBehaviour
{
    public float rayDistance = 10f; // ������ �ִ� �Ÿ�
    public LayerMask layerMask;     // ����ĳ��Ʈ�� �浹�� ������ ���̾�
    public Action OnInteractableDetected;
    public Action OnInteractableRemoved;

    void Update()
    {
        // ����ĳ��Ʈ �߻�
        RaycastHit hit;
        Vector3 rayDirection = transform.forward * rayDistance;

        // ����ĳ��Ʈ�� �ð������� ǥ��
        // ����ĳ��Ʈ�� �浹�� ������Ʈ ���
        if (Physics.Raycast(transform.position, transform.forward, out hit, rayDistance, layerMask))
        {
            OnInteractableDetected?.Invoke();
        }
        else
        {
            OnInteractableRemoved?.Invoke();
        }
    }
}

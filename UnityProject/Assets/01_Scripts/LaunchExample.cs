using UnityEngine;

public class LaunchExample : MonoBehaviour
{
    public float launchForce = 10f; // ���� ���� ũ��

    void OnCollisionEnter(Collision collision)
    {
        // �浹�� ������Ʈ�� Rigidbody�� �ִ��� Ȯ��
        Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
        if (rb != null)
        {
            // Rigidbody�� ���� ���� ����
            rb.AddForce(Vector3.up * launchForce, ForceMode.Impulse);
        }
    }
}

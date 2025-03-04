using UnityEngine;

public class LaunchExample : MonoBehaviour
{
    public float launchForce = 10f; // 날릴 힘의 크기

    void OnCollisionEnter(Collision collision)
    {
        // 충돌한 오브젝트에 Rigidbody가 있는지 확인
        Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
        if (rb != null)
        {
            // Rigidbody에 위로 힘을 가함
            rb.AddForce(Vector3.up * launchForce, ForceMode.Impulse);
        }
    }
}

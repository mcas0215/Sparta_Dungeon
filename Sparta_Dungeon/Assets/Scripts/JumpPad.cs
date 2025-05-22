using UnityEngine;

public class JumpPad : MonoBehaviour
{
    public float jumpForce = 10f; // 점프 힘 조절용

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger Enter: " + other.name); // 여기에 로그 추가
        Rigidbody rb = other.GetComponent<Rigidbody>();
        if (rb != null)
        {
            Debug.Log("Rigidbody Found: " + rb.name); // 여기에도 로그 추가
            rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

}

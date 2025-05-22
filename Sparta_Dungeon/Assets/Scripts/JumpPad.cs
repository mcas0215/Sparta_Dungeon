using UnityEngine;

public class JumpPad : MonoBehaviour
{
    public float jumpForce = 10f; // ���� �� ������

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger Enter: " + other.name); // ���⿡ �α� �߰�
        Rigidbody rb = other.GetComponent<Rigidbody>();
        if (rb != null)
        {
            Debug.Log("Rigidbody Found: " + rb.name); // ���⿡�� �α� �߰�
            rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

}

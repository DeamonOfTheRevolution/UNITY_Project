using UnityEngine;

public class Move_Rigidbody : MonoBehaviour
{
    public float speedForce;
    public Rigidbody cubeRigidbody;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cubeRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 input = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Jump"), Input.GetAxis("Vertical"));
        cubeRigidbody.AddForce(speedForce * Time.deltaTime * input, ForceMode.Impulse);
    }
}

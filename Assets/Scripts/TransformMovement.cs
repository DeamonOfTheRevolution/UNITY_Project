using UnityEngine;

public class TransformMovement : MonoBehaviour
{
    public float speed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 input = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Jump"), Input.GetAxis("Vertical"));
        transform.position = transform.position + input * speed * Time.deltaTime;
    }
}

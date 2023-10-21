using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public int offset_y = 3;
    public float speed = 10;

    [SerializeField]
    Transform land;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(this.transform.position.x, land.position.y + offset_y, this.transform.position.z);
        transform.LookAt(land.position);

    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        float mouse_x = Input.GetAxis("Mouse X");
        float mouse_y = Input.GetAxis("Mouse Y");

        this.transform.Translate(Vector3.forward * vertical * Time.deltaTime * speed, Space.Self);
        this.transform.Translate(Vector3.right * horizontal * Time.deltaTime * speed, Space.Self);

        this.transform.Rotate(Vector3.up * mouse_x);
        this.transform.Rotate(Vector3.left * mouse_y);
    }
}

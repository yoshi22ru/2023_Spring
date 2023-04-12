using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightCon : MonoBehaviour
{
    public Vector2 turn;
    public float sensitivity = 5f;
    public Vector3 deltaMove;
    public float speed = 1;
    public GameObject mover;

    private void Start()
    {
      
    }

    void FixedUpdate()
    {
        turn.x += Input.GetAxis("Mouse X");
        turn.y -= Input.GetAxis("Mouse Y");
        mover.transform.localRotation = Quaternion.Euler(0, turn.x, 0);
        transform.localRotation = Quaternion.Euler(-turn.y, 0, 0);

        deltaMove = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxisRaw("Vertical") * speed * Time.deltaTime);

        this.transform.LookAt(mover.transform);
    }
}

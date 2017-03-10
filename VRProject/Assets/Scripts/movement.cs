using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour {

    // Use this for initialization
    public float keySpeed = 10;
    public float mouseSpeed = 1.25f;
    public GameObject eye;
    static movement instance = null;

    private Quaternion originalRotation;
    private Vector2 angle = new Vector2(0f, 0f);
    private Vector2 minAngle = new Vector2(-360f, -90f);
    private Vector2 maxAngle = new Vector2(360f, 90f);
    private float limit = 360f;
    void Start () {
        if (instance == null)
        {
            instance = this;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            originalRotation = transform.localRotation;

        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetAxis("Horizontal") < 0)
        {
            Strafe(-keySpeed * Time.deltaTime);
        }else if (Input.GetAxis("Horizontal") > 0)
        {
            Strafe(keySpeed * Time.deltaTime);
        }
        if (Input.GetAxis("Vertical") > 0)
        {
            Fly(keySpeed * Time.deltaTime);
        }
        else if (Input.GetAxis("Vertical") < 0)
        {
            Fly(-keySpeed * Time.deltaTime);
        }

        float dx = Input.GetAxis("RightHorizontal");
        float dy = Input.GetAxis("RightVertical");
        Look(new Vector2(dx, dy) * mouseSpeed);
    }

    void Strafe(float dist)
    {
        transform.position += eye.transform.right * dist;
    }

    void Fly(float dist)
    {
        transform.position += eye.transform.forward * dist;
    }

    void Look(Vector2 dist)
    {
        angle += dist;

        angle.x = ClampAngle(angle.x, minAngle.x, maxAngle.x);
        angle.y = ClampAngle(angle.y, minAngle.y, maxAngle.y);

        Quaternion quatX = Quaternion.AngleAxis(angle.x, Vector3.up);
        Quaternion quatY = Quaternion.AngleAxis(angle.y, -Vector3.right);

        transform.localRotation = originalRotation * quatX * quatY;
    }

    float ClampAngle(float angle, float min, float max)
    {
        if (angle < -limit)
        {
            angle += limit;
        }
        else if (angle > limit)
        {
            angle -= limit;
        }
        return Mathf.Clamp(angle, min, max);
    }
}

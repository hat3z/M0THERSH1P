using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MS_PlayerShipController : MonoBehaviour
{
    public float verticalInputAcceleration = 1;
    public float horizontalInputAcceleration = 20;

    public float maxSpeed = 10;
    public float maxRotationSpeed = 100;

    public float velocityDrag = 1;
    public float rotationDrag = 1;

    private Vector3 velocity;
    private float zRotationVelocity;
    private Vector3 mousePos;

    public bool useMouseAim;

    // DASH SETTINGS
    public float dashForce;
    public float dashCooldown;
    private float dashCDCounter;
    bool canUseDash;
    Rigidbody rb;

    private void Start()
    {

        rb = GetComponent<Rigidbody>();
        dashCDCounter = dashCooldown;
        canUseDash = true;
    }

    private void Update()
    {
        // apply forward input
        Vector3 acceleration = Input.GetAxis("Vertical") * verticalInputAcceleration * transform.up;
        velocity += acceleration * Time.deltaTime;

        // apply turn input
        //float zTurnAcceleration = -1 * Input.GetAxis("Horizontal") * horizontalInputAcceleration ;
        //zRotationVelocity += zTurnAcceleration * Time.deltaTime;


    }

    private void FixedUpdate()
    {
        // apply velocity drag
        velocity = velocity * (1 - Time.deltaTime * velocityDrag);

        // clamp to maxSpeed
        velocity = Vector3.ClampMagnitude(velocity, maxSpeed);

        // apply rotation drag
        zRotationVelocity = zRotationVelocity * (1 - Time.deltaTime * rotationDrag);

        // clamp to maxRotationSpeed
        zRotationVelocity = Mathf.Clamp(zRotationVelocity, -maxRotationSpeed, maxRotationSpeed);

        // update transform
        transform.position += velocity * Time.deltaTime;

        if (useMouseAim)
        {
            mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10);
            Vector3 lookPos = Camera.main.ScreenToWorldPoint(mousePos);
            lookPos = lookPos - transform.position;
            float angle = Mathf.Atan2(lookPos.y, lookPos.x) * Mathf.Rad2Deg - 90;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }

        else
        {
            transform.Rotate(0, 0, zRotationVelocity * Time.deltaTime);
        }

        UseDash();

    }

    void UseDash()
    {
        if(canUseDash)
        {
            if (Input.GetKey(KeyCode.D))
            {
                rb.AddForce(transform.right * dashForce, ForceMode.Impulse);

                canUseDash = false;
            }
            if (Input.GetKey(KeyCode.A))
            {
                rb.AddForce(-transform.right * dashForce, ForceMode.Impulse);

                canUseDash = false;
            }
        }
        else
        {
            dashCDCounter -= Time.deltaTime;
        }
        if (dashCDCounter <= 0)
        {
            dashCDCounter = dashCooldown;
            canUseDash = true;
        }
    }

}

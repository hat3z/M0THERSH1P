using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MS_PlayerShipController : MonoBehaviour
{
    [Header("Player Ship Control")]
    public MS_PlayerProfile PlayerProfile;
    private float dashCDCounter;
    private float zRotationVelocity;

    // privates
    private Vector3 velocity;
    private Vector3 mousePos;
    Rigidbody rb;

    private void Start()
    {

        rb = GetComponent<Rigidbody>();
        dashCDCounter = PlayerProfile.ShipControl.dashCooldown;
}

    private void Update()
    {
        // apply forward input
        Vector3 acceleration = Input.GetAxis("Vertical") * PlayerProfile.ShipControl.verticalInputAcceleration * transform.up;
        velocity += acceleration * Time.deltaTime;

        // apply turn input
        //float zTurnAcceleration = -1 * Input.GetAxis("Horizontal") * horizontalInputAcceleration ;
        //zRotationVelocity += zTurnAcceleration * Time.deltaTime;


    }

    private void FixedUpdate()
    {

        ShipControl();
        UseDash();

    }

    void ShipControl()
    {
        // apply velocity drag
        velocity = velocity * (1 - Time.deltaTime * PlayerProfile.ShipControl.velocityDrag);

        // clamp to maxSpeed
        velocity = Vector3.ClampMagnitude(velocity, PlayerProfile.ShipControl.maxSpeed);

        // apply rotation drag
        zRotationVelocity = zRotationVelocity * (1 - Time.deltaTime * PlayerProfile.ShipControl.rotationDrag);

        // clamp to maxRotationSpeed
        zRotationVelocity = Mathf.Clamp(zRotationVelocity, -PlayerProfile.ShipControl.maxRotationSpeed, PlayerProfile.ShipControl.maxRotationSpeed);

        // update transform
        transform.position += velocity * Time.deltaTime;

        if (PlayerProfile.ShipControl.useMouseAim)
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
    }

    void UseDash()
    {
        if(PlayerProfile.ShipControl.CanUseDash)
        {
            if (Input.GetKey(KeyCode.D))
            {
                rb.AddForce(transform.right * PlayerProfile.ShipControl.dashForce, ForceMode.Impulse);

                PlayerProfile.ShipControl.CanUseDash = false;
            }
            if (Input.GetKey(KeyCode.A))
            {
                rb.AddForce(-transform.right * PlayerProfile.ShipControl.dashForce, ForceMode.Impulse);

                PlayerProfile.ShipControl.CanUseDash = false;
            }
        }
        else
        {
            dashCDCounter -= Time.deltaTime;
        }
        if (dashCDCounter <= 0)
        {
            dashCDCounter = PlayerProfile.ShipControl.dashCooldown;
            PlayerProfile.ShipControl.CanUseDash = true;
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementPlayer : MonoBehaviour
{
    public float speed = 6f;
    float staticSpeed;

    float fSPtimer = 0.0f;
    float fSPdelay = 1.0f;
    float fSPusage = -20.0f;
    bool bIsSprint = false;

    Vector3 movement;
    Animator playerAnim;
    Rigidbody playerRigidBody;
    int floorMask;
    float camRayLength = 100f;
    PlayerBaseScript playerBaseScript;

    void Awake() // Runs regardless if script is enabled
    {
        floorMask = LayerMask.GetMask("Floor");
        playerAnim = GetComponent<Animator>();
        playerRigidBody = GetComponent<Rigidbody>();
        playerBaseScript = GetComponent<PlayerBaseScript>();
        staticSpeed = speed;
    }

    void Update()
    {
        if (bIsSprint)
        {
            if (playerBaseScript.pfStamina < -(fSPusage))
            {
                bIsSprint = false;
                speed = staticSpeed;
                playerBaseScript.pfStaminaRegen = 10.0f;
            }
            else
            {
                fSPtimer += Time.deltaTime;
                if (fSPtimer >= fSPdelay)
                {
                    playerBaseScript.SetStamina(fSPusage);
                    fSPtimer = 0.0f;
                }
            }
        }
        else
        {
            fSPtimer = 0.0f;
        }
    }

    void FixedUpdate() // FixedUpdate runs with every physics update, normal update runs with rendering
    {
        float fRawHorizontalAxis = Input.GetAxisRaw("Horizontal");
        // Raw Input only has values -1 , 0 , 1. Normal Input will have any number between -1 to 1. This will allow character to snap to full speed.
        float fRawVerticalAxis = Input.GetAxisRaw("Vertical");

        if (Input.GetButtonDown("Sprint") && playerBaseScript.pfStamina >= -(fSPusage))
        {
            speed *= 1.5f;
            bIsSprint = true;
            playerBaseScript.pfStaminaRegen = 0.0f;
        }

        if(Input.GetButtonUp("Sprint"))
        {
            speed = staticSpeed;
            bIsSprint = false;
        }

        Move(fRawHorizontalAxis, fRawVerticalAxis);
        Turning();
        Animating(fRawHorizontalAxis, fRawVerticalAxis);
    }

    void Move(float h, float v)
    {
        movement.Set(h, 0f, v);
        movement = movement.normalized * speed * Time.deltaTime;

        // normalize movement so that moving diagonal is same as move horizontal/vertical
        // speed so that it does not move at 1 unit
        // Time.deltaTime so that it moves with time instead of per frame.

        playerRigidBody.MovePosition(transform.position + movement);
        // move player current position + our movement calculated
    }

    void Turning()
    {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        //Screenpointtoray cast a point from the screen into the scene
        //Fires to point underneath the mouse

        RaycastHit floorHit;
        //Get a variable back when ray hit floor

        if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))
        //(Shoot ray, Get variable back/store hit info, Ray length, hit floor layer)
        {
            Vector3 playerToMouse = floorHit.point - transform.position;
            //Get the vector direction from player to mouse;
            playerToMouse.y = 0f;

            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
            //Store Rotation and move the vector3 the new forward facing vector
            playerRigidBody.MoveRotation(newRotation);
        }
    }

    void Animating(float h, float v)
    {
        bool walking = h != 0f || v != 0f;
        //walking is true if H or V is non zero
        playerAnim.SetBool(("bIsWalking"), walking);
    }
}

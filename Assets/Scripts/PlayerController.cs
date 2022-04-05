using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float speed = 0;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;

    private Rigidbody rb;
    private float movementX;
    private float movementY;
    private int count; 

    private bool dj;
    private bool IsGrounded;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        dj = true;
        count = 0;

        SetCountText();
        winTextObject.SetActive(false);

    }

    void OnJump()
    {
        if (IsGrounded)
        {

            Vector3 movz = new Vector3(0.0f, 300.0f, 0.0f);
            rb.AddForce(movz);
        }
        if (!IsGrounded && dj)
        {

            Vector3 movz = new Vector3(0.0f, 300.0f, 0.0f);
            rb.AddForce(movz);
            dj = false;
        }



    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if (count >= 12)
        {
            winTextObject.SetActive(true);
        }
    }
    void FixedUpdate()
    {
        //grounded = (lastYposition == rb.position.y); // Checks if Y has changed since last frame
        //lastYposition = rb.position.y;
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        rb.AddForce(movement * speed);

    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp")) 
        {
            other.gameObject.SetActive(false);
            count = count + 1;

            SetCountText();
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Ground")
        {
            IsGrounded = false;
        }


    }
    private void OnTriggerStay(Collider other)
    {
        if (other.transform.tag == "Ground")
        {
            IsGrounded = true;
            dj = true;
        }

    }


}

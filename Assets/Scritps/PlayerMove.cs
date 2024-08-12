using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Rigidbody rb;
    private bool isSwiping = false;
    private float XPosition;
    private Vector2 SwipePosition;
    private Vector2 SwipeDelta;
    public float speed = 1f;
    public static bool Gamestarted = false;
    private bool isMoving = false;
    private float xboundary;
    private Animator playerAnimator;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        playerAnimator = gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = new Vector3(0, 0, speed);

        /*if(Input.GetKey(KeyCode.Space))
        {
            //transform.Translate(new Vector3(xboundary, transform.position.y, transform.position.z) * Time.deltaTime);
            transform.Translate(5*Vector3.left * Time.deltaTime);
            Debug.Log("Space");
        }*/

        if (Input.touchCount > 0)
        {
            Gamestarted = true;


            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                XPosition = transform.position.x;
                isSwiping = true;
                SwipePosition = Input.GetTouch(0).position;
                gameObject.GetComponent<Animator>().SetBool("IsJump", true);

            }

            else if (touch.phase == TouchPhase.Canceled || touch.phase == TouchPhase.Ended)
            {
                //transform.position = new Vector3(xboundary, transform.position.y, transform.position.z);
                isMoving = true;
                gameObject.GetComponent<Animator>().SetBool("IsJump", false);
                ResetSwipe();
            }

        }

        CheckSwipe();

        if (isMoving && transform.position != new Vector3(xboundary, transform.position.y, transform.position.z))
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(xboundary, transform.position.y, transform.position.z), 5 * Time.deltaTime);
        }
        else
        {
            isMoving = false;
        }
    }

    private void CheckSwipe()

    {
        SwipeDelta = Vector2.zero;

        if (Input.touchCount > 0)
        {
            SwipeDelta = Input.GetTouch(0).position - SwipePosition;
  
        }

        if (SwipeDelta.magnitude > 80)
        {

            if (Mathf.Abs(SwipeDelta.x) > Mathf.Abs(SwipeDelta.y))
            {
                OnSwipe(SwipeDelta.x > 0 ? Vector2.right : Vector2.left);
            }

            ResetSwipe();
        }


    }

    private void ResetSwipe()
    {
        isSwiping = false;
        SwipePosition = Vector2.zero;
        SwipeDelta = Vector2.zero;
    }


    private void OnSwipe(Vector3 direction)

    {
        Vector3 newposition = transform.position + 0.9f*direction;
        xboundary = Mathf.Clamp(newposition.x, -1.4f, 1.3f);
        //rb.AddForce(direction * 9);
    }
}

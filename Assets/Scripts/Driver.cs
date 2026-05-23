using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class Driver : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        boostText.gameObject.SetActive(false);
    }

    // Update is called once per frame
    [Header("Speed Settings")]
    [SerializeField] float moveSpeed = 7f;
    [SerializeField] float currentSpeed = 7f;
    [SerializeField] float steerSpeed = 200f;
    [SerializeField] float slowSpeed = 4f;
    [SerializeField] float package = 0f;
    [SerializeField] float boostSpeed = 11f;

    [Header("UI")]
    [SerializeField] TMP_Text boostText;
    void Update()
    {
        float move = 0f;
        float steer = 0f;
        if (Keyboard.current.wKey.isPressed)
            move = 1f;
        else if (Keyboard.current.sKey.isPressed)
            move = -1f;

        if (Keyboard.current.aKey.isPressed)
            steer = 1f;
        else if (Keyboard.current.dKey.isPressed)
            steer = -1f;

        // TIME.DELTATIME ADDED HERE!
        float moveAmount = move * currentSpeed * Time.deltaTime;
        float steerAmount = steer * steerSpeed * Time.deltaTime;

        transform.Translate(0, moveAmount, 0);
        transform.Rotate(0, 0, steerAmount);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("WorldCollision"))
        {
            currentSpeed = slowSpeed;
            boostText.gameObject.SetActive(false);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        currentSpeed = moveSpeed;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Boost"))
        {
            currentSpeed = boostSpeed;
            boostText.gameObject.SetActive(true);
            Invoke(nameof(resetSpeed), 5f);
            Destroy(collision.gameObject);
        }
    }

    void resetSpeed()
    {
        currentSpeed = moveSpeed;
    }
}

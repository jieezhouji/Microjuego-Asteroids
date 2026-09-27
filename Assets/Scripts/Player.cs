using UnityEngine;

public class Player : MonoBehaviour
{
    public static int SCORE = 0;

    float thrustForce = 5f;
    float rotationSpeed = 120f;

    Vector2 thrustDirection;
    Rigidbody2D _rigidbody;

    void Awake()
    {
        SCORE = 0;
    }

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        float rotation = Input.GetAxis("Rotate") * rotationSpeed * Time.deltaTime;
        float thrust = Input.GetAxis("Thrust") * thrustForce;
        thrustDirection = transform.right;
        transform.Rotate(Vector3.forward, -rotation);
        _rigidbody.AddForce(thrust * thrustDirection);

        WrapAroundScreen();
    }

    void WrapAroundScreen()
    {
        Vector3 viewportPos = Camera.main.WorldToViewportPoint(transform.position);
        bool wrapped = false;

        if (viewportPos.x > 1) { viewportPos.x = 0; wrapped = true; }
        else if (viewportPos.x < 0) { viewportPos.x = 1; wrapped = true; }

        if (viewportPos.y > 1) { viewportPos.y = 0; wrapped = true; }
        else if (viewportPos.y < 0) { viewportPos.y = 1; wrapped = true; }

        if (wrapped)
            transform.position = Camera.main.ViewportToWorldPoint(viewportPos);
    }
}
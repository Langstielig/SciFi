using UnityEngine;

public class MainCharacterController : MonoBehaviour
{
    private Rigidbody rb;
    private Animator animator;

    private float movingSpeed = 5f;
    private Vector3 moveVector;

    private Vector3 worldPosition;
    private Plane plane = new Plane(Vector3.up, 0);

    public GameObject bullet;
    private float startTimeBetweenShots = 0.5f;
    private float timeBetweenShots;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) && timeBetweenShots <= 0)
        {
            Instantiate(bullet, GetCenterOfMainCharacter(), transform.rotation);
            timeBetweenShots = startTimeBetweenShots;
        }
        else
        {
            timeBetweenShots -= Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        Move();
        Rotate();
        ChangeAnimation();
    }

    private void Move()
    {
        moveVector.x = Input.GetAxis("Horizontal");
        moveVector.z = Input.GetAxis("Vertical");
        rb.MovePosition(rb.position + moveVector * movingSpeed * Time.deltaTime);
    }

    private void Rotate()
    {
        float distance;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(plane.Raycast(ray, out distance))
        {
            worldPosition = ray.GetPoint(distance);
        }

        Vector3 direction = worldPosition - transform.position;
        transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
    }

    private void ChangeAnimation()
    {
        animator.SetBool("isShooting", Input.GetMouseButton(0));
        animator.SetBool("isForwardMoving", Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D));
    }

    private Vector3 GetCenterOfMainCharacter()
    {
        return transform.position + Vector3.up;
    }
}

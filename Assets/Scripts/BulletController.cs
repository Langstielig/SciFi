using UnityEngine;

public class BulletController : MonoBehaviour
{
    private float speed = 20f;
    private int damage = 10;
    private float distance = 0.5f;
    private Vector3 movement = new Vector3(0, 0, 1);

    [SerializeField] private LayerMask whatIsSolid;
    [SerializeField] private GameObject particlesPrefab;
    private GameObject particles;

    private void Update()
    {
        transform.Translate(movement * speed * Time.deltaTime);
        Shoot();
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void Shoot() 
    {
        RaycastHit hit;
        Debug.DrawRay(transform.position, transform.forward * 100f, Color.blue);
        if (Physics.Raycast(transform.position, transform.forward, out hit, distance, whatIsSolid))
        {
            if (hit.transform.tag == "Robot")
            {
                RobotController robotController = hit.transform.GetComponent<RobotController>();
                robotController.TakeDamage(damage);
            }
            particles = Instantiate(particlesPrefab);
            particles.transform.position = transform.position;

            Vector3 particleRotation = transform.rotation.eulerAngles;
            particleRotation.x += 180;
            particles.transform.rotation = Quaternion.Euler(particleRotation);

            Destroy(gameObject);
        }
    }
}

using UnityEngine;

public class RobotController : MonoBehaviour
{
    private int health = 50;
    private float speed = 5f;
    private float currentYAngle;
    private bool isMaterialChanged = false;
    private bool isDestroyed = false;

    [SerializeField] private Material backMaterial;
    [SerializeField] private Material frontMaterial;
    [SerializeField] private GameObject particles;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        currentYAngle = GenerateAngle() + transform.rotation.y;
        transform.rotation = Quaternion.Euler(0, currentYAngle, 0);
    }

    private void FixedUpdate()
    {
        if (!isDestroyed)
        {
            Move();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        currentYAngle += GenerateAngle() + 180;
        transform.rotation = Quaternion.Euler(0, currentYAngle, 0);
    }

    private void Move()
    {
        transform.position += transform.forward * -1f * speed * Time.deltaTime;
    }

    private float GenerateAngle()
    {
        return Random.Range(-61f, 61f);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if(health <= 0) 
        {
            isDestroyed = true;
            ChangeToDestroyAnimation();
            InstantiateParticles();
            Invoke("Die", 1f);
        }
        else if(health <= 25 && !isMaterialChanged)
        {
            ChangeMaterial();
            isMaterialChanged = true;
        }
    }

    private void ChangeMaterial()
    {
        Transform back = transform.Find("Back");
        Transform front = transform.Find("Front");
        
        if(back != null)
        {
            MeshRenderer meshRendererBack = back.GetComponent<MeshRenderer>();
            meshRendererBack.material = backMaterial;
        }

        if (front != null)
        {
            MeshRenderer meshRendererFront = front.GetComponent<MeshRenderer>();
            meshRendererFront.material = frontMaterial;
        }
    }

    private void ChangeToDestroyAnimation()
    {
        animator.SetBool("isDestroyed", isDestroyed);
    }

    private void InstantiateParticles()
    {
        GameObject newParticles = Instantiate(particles);
        Vector3 position = transform.position;
        position.y = 0f;
        newParticles.transform.position = position;
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}

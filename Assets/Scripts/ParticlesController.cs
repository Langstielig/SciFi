using UnityEngine;

public class ParticlesController : MonoBehaviour
{
    private void Start()
    {
        Invoke("DestroyParticles", 2f);
    }

    private void DestroyParticles()
    {
        Destroy(gameObject);
    }
}

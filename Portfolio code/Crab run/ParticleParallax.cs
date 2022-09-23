using UnityEngine;

public class ParticleParallax : MonoBehaviour
{
    [SerializeField] float parallaxEffect;
    float speed;

    Spawner spawner;
    ParticleSystem ps;
  
    void Start()
    {
        spawner = FindObjectOfType<Spawner>();
        ps = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        var velocity = ps.velocityOverLifetime;
        speed = spawner.Speed / parallaxEffect;
        velocity.speedModifier = speed;
    }
}

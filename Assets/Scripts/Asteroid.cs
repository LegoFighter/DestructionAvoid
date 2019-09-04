using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public int HP = 40;
    public Transform ExplosionPoint;

    public ParticleSystem[] ParticleSystem;
    private List<ParticleSystem> Particles;
    private MeshRenderer meshRenderer;
    public GameEvent AsteroidDestroyd;


    void Start()
    {
        Particles = new List<ParticleSystem>();
        meshRenderer = GetComponent<MeshRenderer>();
    }

    // void Update()
    // {
    //     if (Input.GetKeyDown(KeyCode.Space))
    //     {
    //         meshRenderer.enabled = false;
    //         PlayExplosion();
    //         StartCoroutine(WinMessage());
    //     }
    // }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Rocket"))
        {
            Rocket rocket = other.gameObject.GetComponent<Rocket>();

            HP -= rocket.Damage;

            if (HP <= 0)
            {
                HP = 0;
                meshRenderer.enabled = false;
                PlayExplosion();
                StartCoroutine(WinMessage());
            }
        }
    }




    void PlayExplosion()
    {
        foreach (var item in ParticleSystem)
        {
            Particles.Add(Instantiate(item, ExplosionPoint.transform.position, Quaternion.identity, ExplosionPoint.transform));
        }
    }

    IEnumerator WinMessage()
    {
        yield return new WaitForSeconds(3);

        foreach (var item in Particles)
        {
            Destroy(item.gameObject);
        }
        AsteroidDestroyd.Raise();
    }

}

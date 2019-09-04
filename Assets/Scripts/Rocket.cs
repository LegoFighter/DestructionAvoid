using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    public GameObject Target;
    public int Damage;
    public float Speed = 3;
    public float SelfDestructDelay = 30;
    public GameObject Explosion;
    private MeshRenderer[] MeshRenderer;
    private ParticleSystem ParticleSystem;
    private Collider[] Collider;
    public GameEvent RocketSelfDestruct;


    void Start()
    {
        StartCoroutine(SelfDestruct());
        MeshRenderer = GetComponentsInChildren<MeshRenderer>();
        ParticleSystem = GetComponentInChildren<ParticleSystem>();
        Collider = GetComponentsInChildren<Collider>();
    }

    void LateUpdate()
    {
        if (Target != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, Target.transform.position, Time.deltaTime * Speed);
        }

    }

    void OnTriggerEnter(Collider other)
    {
        PlayExplosion();
        StartCoroutine(Destroy());
    }

    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }

    IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(SelfDestructDelay);
        foreach (var item in Collider)
        {
            item.enabled = false;
        }
        PlayExplosion();
        RocketSelfDestruct.Raise();
        StartCoroutine(Destroy());
    }
    void PlayExplosion()
    {
        Instantiate(Explosion, transform.position, transform.rotation, transform);
        foreach (var mesh in MeshRenderer)
        {
            mesh.enabled = false;
        }
        ParticleSystem.Stop();
    }
}

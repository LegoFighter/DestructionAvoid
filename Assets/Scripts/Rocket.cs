using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    public Transform Target;
    public int Damage;
    public float Speed = 1;
    public float SelfDestructDelay = 30;
    public GameObject Explosion;


    void Start()
    {
        StartCoroutine(DestroyTimer());
    }

    void LateUpdate()
    {
        if (Target != null)
        {
            float step = Speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, Target.position, step);
            transform.LookAt(Target);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Asteroid"))
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit))
            {
                Instantiate(Explosion, hit.point, Quaternion.identity);
            }
            //Destroy(this.gameObject);
        }
    }

    IEnumerator DestroyTimer()
    {
        yield return new WaitForSeconds(SelfDestructDelay);
        Debug.Log("Rocket selfdestruct.");
        Destroy(this.gameObject);
    }
}

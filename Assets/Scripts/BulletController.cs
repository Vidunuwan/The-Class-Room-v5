using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] private GameObject bulltDecal;

    private float speed = 50f;
    private float timeToDestroyee = 3f;

    public Vector3 target { get; set; }
    public bool hit { get; set; }


    private void OnEnable()
    {
        Destroy(gameObject, timeToDestroyee);
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        if (!hit && Vector3.Distance(transform.position, target) < 0.1f) Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        ContactPoint contact = collision.GetContact(0);
        GameObject.Instantiate(bulltDecal, contact.point+contact.normal*0.0001f,Quaternion.LookRotation(contact.normal));
        Destroy(gameObject);
    }
}

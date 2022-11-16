using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] float life = 3;
    [SerializeField] float moveSpeed = 10;
    [SerializeField] ParticleSystem Explode;
    MeshRenderer meshRenderer;
    Rigidbody rb;
    Vector3 moveDirection = Vector3.down;

    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        rb = GetComponent<Rigidbody>();

        rb.velocity = Vector3.down * moveSpeed;
        Destroy(gameObject, 8);
    }

    public void TakeDmg(float dmg)//, Vector3 Force)
    {
        life -= dmg;
        if (life <= 0)
        {
            //faudra mettre de la juicyness ici

            meshRenderer.enabled = false;
            GetComponent<Collider>().enabled = false;

            Explode.Play();
            Destroy(this.gameObject, 2);
            //GetComponent<Rigidbody>().isKinematic = false;
            //GetComponent<Rigidbody>().AddForce(Force);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<Controler>()?.Hit();
            Destroy(gameObject);
        }
    }
}

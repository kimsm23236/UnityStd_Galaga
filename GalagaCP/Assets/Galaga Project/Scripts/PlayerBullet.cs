using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public float bulletSpeed;
    private Rigidbody bulletRigidbody;
    // Start is called before the first frame update
    void Start()
    {
        bulletRigidbody = gameObject.GetComponent<Rigidbody>();
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        
    }

    public void Activate(Transform startTransform)
    {
        gameObject.SetActive(true);
        gameObject.transform.position = startTransform.position;
        gameObject.transform.rotation = startTransform.rotation;
        bulletRigidbody.velocity = transform.forward * bulletSpeed;

    }
}

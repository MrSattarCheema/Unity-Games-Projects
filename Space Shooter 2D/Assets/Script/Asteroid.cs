using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField]
    private float speed=22f;
    private Animator animator;
    private SpawnManager spawnManager;
    [SerializeField]
    private AudioClip clip;
    // Start is called before the first frame update
    void Start()
    {
        animator= GetComponent<Animator>();
        spawnManager= GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward * speed * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Laser")
        {
            AudioSource.PlayClipAtPoint(clip, transform.position);
            animator.SetTrigger("Astroid_Explosion");
            Destroy(this.gameObject,2.5f);
            Destroy(other.gameObject);
            spawnManager.startCoroutine();
        }
    }
}

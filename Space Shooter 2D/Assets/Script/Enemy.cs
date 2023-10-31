using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float speed = 4.0f;
    [SerializeField]
    private Player player;
    [SerializeField]
    private Animator animator;
    private bool isDestoyed=false;
    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame y=6.37 and -4, x = -9.7 and 9.7
    void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);
        if (isDestoyed==false)
        {
            if (transform.position.y < -4f)
            {
                float randomX = Random.Range(-9, 9);
                transform.position = new Vector3(randomX, 6.37f, 0);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            if(player != null)
            {
                player.Demage();
            }
            Destroy(GetComponent<Collider2D>());
            animator.SetTrigger("OnEnemyDeath");
            isDestoyed = true;
            audioSource.Play();
            Destroy(this.gameObject, 2.8f);
            //Debug.Log("h");
        }
        if(other.tag == "Laser")
        {
            if (player != null)
            {
                player.AddScore(10);
            }
            Destroy(GetComponent<Collider2D>());
            animator.SetTrigger("OnEnemyDeath");
            Destroy(other.gameObject);
            isDestoyed=true;
            audioSource.Play();
            Destroy(this.gameObject,2.8f);
        }
    }
}

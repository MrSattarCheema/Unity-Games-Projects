using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField]
    private int speed = 4;
    private Player player;
    [SerializeField]
    private int powerUpId;
    [SerializeField]
    private AudioClip audioClip;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        if (player == null)
        {
            Debug.LogError("The player is null!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);
        if(transform.position.y < -5.75)
        {
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            AudioSource.PlayClipAtPoint(audioClip, transform.position);
            switch (powerUpId)
            {
                case 0:
                    {
                        player.activateTripleShot();
                    }
                    break;
                case 1:
                    {
                        player.SpeedPowerUp();
                    }
                    break;
                case 2:
                    {
                        player.ShieldPowerUp();
                    }
                    break;
            }
            Destroy(this.gameObject); 
        }
    }
}

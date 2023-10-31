using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private int speed = 8;
    [SerializeField]
    private GameObject laserPrefab;
    [SerializeField]
    private float canFire = -1f;
    [SerializeField]
    private float fireRate = .1f;
    [SerializeField]
    private int lives = 3;
    private SpawnManager spawnManager;
    [SerializeField]
    private bool is3ShotEnabled = false;
    [SerializeField]
    private GameObject tripleLaser;
    [SerializeField]
    private bool isShieldPowerUpEnabled = false;
    [SerializeField]
    private GameObject PlayerShield;
    [SerializeField]
    private int score;
    [SerializeField]
    private UiManager uiManager;
    [SerializeField]
    private GameObject LeftEngineFail;
    [SerializeField]
    private GameObject RightEngineFail;
    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
        spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        uiManager = GameObject.Find("Canvas").GetComponent<UiManager>();
        audioSource = GetComponent<AudioSource>();
        if(spawnManager == null )
        {
            Debug.LogError("The spawn is null!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        Movemoent();
        if (Input.GetKeyDown(KeyCode.Space) && (Time.time > canFire)) {
            FireLaser();
        }
    }
    void Movemoent()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        //transform.Translate(Vector3.right * speed * horizontalInput * Time.deltaTime);
        // transform.Translate(Vector3.up * speed * verticalInput * Time.deltaTime);
        // anothr way to do
        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);
        //if (transform.position.y >= 5.7)
        //{
        //    transform.position = (new Vector3(transform.position.x, 5.7f, 0));
        //}
        //else if (transform.position.y < -3)
        //{
        //    transform.position = (new Vector3(transform.position.x, -3, 0));
        //}
        //  Instead of using if else statement we can aslo use clamp
        transform.position = new Vector3(transform.position.x, Math.Clamp(transform.position.y, -4f, 6f), 0);
        if (transform.position.x >= 9.31f)
        {
            transform.position = (new Vector3(9.31f, transform.position.y, 0));
        }
        else if (transform.position.x < -9.31f)
        {
            transform.position = (new Vector3(-9.31f, transform.position.y, 0));
        }
            transform.Translate(direction * speed * Time.deltaTime);
    }
    void FireLaser()
    {
        canFire = Time.time + fireRate;
        //Debug.Log($"fire rate {canFire} and time {Time.time}");
        if(is3ShotEnabled )
        {
            Instantiate(tripleLaser, transform.position + new Vector3(0f,1f,0f)  , Quaternion.identity);
        }
        else { 
        Instantiate(laserPrefab, new Vector3(transform.position.x, transform.position.y + .8f, transform.position.z), Quaternion.identity);
        }
        audioSource.Play();
    }
    public void Demage()
    {
        if (isShieldPowerUpEnabled==false)
        {
            lives--;
            uiManager.updateLivesImg(lives);
            if(lives == 2)
            {
                LeftEngineFail.SetActive(true);
            }
            if(lives == 1)
            {
                RightEngineFail.SetActive(true);
            }
            {

            }
        }
        if (lives == 0)
        {
            Destroy(this.gameObject);
            spawnManager.OnPlayerDeath();
            uiManager.gameOvertxt();
        }
    }
    public void activateTripleShot()
    {
        is3ShotEnabled = true;
        StartCoroutine(PowerDown());
    }
    IEnumerator PowerDown()
    {
        yield return new WaitForSeconds(5);
        is3ShotEnabled = false;
        isShieldPowerUpEnabled = false;
        speed = 8;
        PlayerShield.SetActive(false);
    }
    //IEnumerator activateSpeedUp()
    //{
    //    yield return new WaitForSeconds(5);
    //}
    public void SpeedPowerUp()
    {
        speed = 14;
        StartCoroutine(PowerDown());
    }
    public void ShieldPowerUp()
    {
        isShieldPowerUpEnabled = true;
        StartCoroutine (PowerDown());
        PlayerShield.SetActive(true);
    }
    public void AddScore(int points)
    {
        score += points;
        uiManager.updateScore(score);
    }
}
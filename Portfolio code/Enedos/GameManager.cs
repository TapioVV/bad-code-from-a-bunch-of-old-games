using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] AudioSource dayAmbience;
    [SerializeField] AudioSource nightAmbience;

    [SerializeField] AudioSource cannonSound;
    [SerializeField] AudioSource laserSound;



    [SerializeField] Sprite PlanetDay;
    [SerializeField] Sprite PlanetNight;
    [SerializeField] Sprite BackGroundDay;
    [SerializeField] Sprite BackGroundNight;
    [SerializeField] SpriteRenderer BackGround;

    [SerializeField] Sprite houseDay;
    [SerializeField] Sprite houseBuy;
    [SerializeField] Sprite houseNight;
    [SerializeField] SpriteRenderer houseSR;

    public int MaxHealth;

    [SerializeField] PlaceObject po;
    [SerializeField] EnemySpawn es;
    [SerializeField] BasicTurret[] btA;

    Planet planet;
    Player player;

    [SerializeField] Transform house;

    [SerializeField] float btBulletSpeed;

    [SerializeField] LaserTurret[] ltA;
    [SerializeField] float ltShootTime;

    [SerializeField] float shootTimer;
    public float shootTimerR;

    public bool dayState;
    public bool buyState;
    public bool nightState;

    public int lvl = 1;
    public int cycle = 1;

    [SerializeField] TMP_Text lvlText;
    [SerializeField] TMP_Text cycleText;
    [SerializeField] TMP_Text timerText;

    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject miscUI;

    public float gameTimer;
    float dayTimer = 15;
    float buyTimer = 15;
    float nightTimer = 30;


    [Header("Enemy")]
    public float eShootTime;
    public float eMaxRotSpeed;
    public float eMinRotSpeed;
    public int eAmountMax;
    public int eAmountMin;
    public float eSpawnTimerMin;
    public float eSpawnTimerMax;


    void Start()
    {
        planet = FindObjectOfType<Planet>();
        player = FindObjectOfType<Player>();

        shootTimerR = shootTimer;
        nightAmbience.volume = 0;
    }

    public void SetDay()
    {
        dayState = true;
        gameTimer = dayTimer;
        miscUI.SetActive(true);
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    void Update()
    {
        int intTime = (int)gameTimer;
        timerText.text = intTime.ToString();

        ChangeState();        

        lvlText.text = lvl.ToString();
        cycleText.text = cycle.ToString();

        if(gameTimer > 0 && mainMenu.activeInHierarchy == false)
        {
            gameTimer -= Time.deltaTime;
        }
    }
    void ChangeState()
    {
        if (dayState == true)
        {
            mainMenu.SetActive(false);
            planet.Health = MaxHealth;
            planet.GetComponent<SpriteRenderer>().sprite = PlanetDay;
            BackGround.sprite = BackGroundDay;

            houseSR.sprite = houseDay;

            dayAmbience.volume = 0.1f;
            nightAmbience.volume = 0f;

            DayState();

            if (gameTimer <= 0)
            {
                gameTimer = buyTimer;
                BuyState();
            }
        }
        if (buyState == true)
        {
            planet.GetComponent<SpriteRenderer>().sprite = PlanetDay;
            BackGround.sprite = BackGroundDay;

            houseSR.sprite = houseBuy;

            planet.Health = MaxHealth;

            dayAmbience.volume = 0.1f;
            nightAmbience.volume = 0f;

            BuyState();
            if (gameTimer <= 0)
            {
                gameTimer = nightTimer;
                NightState();
            }
        }
        if (nightState == true)
        {
            planet.GetComponent<SpriteRenderer>().sprite = PlanetNight;
            BackGround.sprite = BackGroundNight;

            houseSR.sprite = houseNight;

            dayAmbience.volume = 0f;
            nightAmbience.volume = 0.1f;

            NightState();
            if (gameTimer <= 0)
            {
                player.transform.position = house.position;
                player.transform.rotation = house.rotation;
                cycle += 1;
                gameTimer = dayTimer;
                DayState();
                if(eMaxRotSpeed < 80)
                {
                    eMaxRotSpeed += 2.5f;
                    eMinRotSpeed += 2.5f;
                }
                if(eSpawnTimerMax > 4.5f)
                {
                    eSpawnTimerMax -= 0.1f;
                }
                if (eSpawnTimerMin > 2f)
                {
                    eSpawnTimerMin -= 0.1f;
                }

                if(eAmountMax < 9)
                {
                    eAmountMax += 1;
                }
                if(eAmountMax >= 9 && eAmountMin < 6) 
                {
                    eAmountMin += 1;
                }
            }
        }
    }
    void DayState()
    {
        dayState = true;
        buyState = false;
        nightState = false;

        po.activatedOnce = true;
        po.activated = false;

        planet.activated = false;
        player.activated = true;
        player.GetComponent<SpriteRenderer>().enabled = true;

        es.activated = false;
    }

    void BuyState()
    {
        dayState = false;
        buyState = true;
        nightState = false;

        po.activated = true;

        planet.activated = true;
        player.activated = false;
        player.GetComponent<SpriteRenderer>().enabled = false;

        es.activated = false;
    }
    void NightState()
    {
        dayState = false;
        buyState = false;
        nightState = true;

        ShootingTimer();

        po.activated = false;
        po.activatedOnce = true;

        planet.activated = true;
        player.activated = false;
        player.GetComponent<SpriteRenderer>().enabled = false;

        es.activated = true;
    }

    void ShootingTimer()
    {
        btA = FindObjectsOfType<BasicTurret>();
        ltA = FindObjectsOfType<LaserTurret>();

        if (shootTimerR > 0)
        {
            shootTimerR -= Time.deltaTime;
        }
        if (shootTimerR <= 0)
        {
            foreach (BasicTurret bt in btA)
            {
                bt.BasicTurretShoot(btBulletSpeed);
            }
            foreach (LaserTurret lt in ltA)
            {
                lt.LaserTurretShoot(ltShootTime);
            }

            if(btA.Length != 0)
            {
                cannonSound.Play();
            }
            if(ltA.Length != 0)
            {
                laserSound.Play();
            }
            shootTimerR = shootTimer;
        }
    }
}

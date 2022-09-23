using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;

public class PlaceObject : MonoBehaviour
{
    [SerializeField] Button CannonButton;
    [SerializeField] int CannonPrize;
    [SerializeField] Button ShieldButton;
    [SerializeField] int ShieldPrize;
    [SerializeField] Button LaserButton;
    [SerializeField] int LaserPrize;
    [SerializeField] Button UpgradeButton;
    [SerializeField] int UpgradePrize;

    GameManager gm;
    Planet planetObj;


    [SerializeField] TMP_Text moneyText;
    [SerializeField] BasicTurret basicTurretPrefab;
    [SerializeField] LaserTurret laserTurretPrefab;
    [SerializeField] ShieldTurret shieldTurretPrefab;

    [SerializeField] GameObject buttonHolder;

    [SerializeField] Transform center;
    [SerializeField] Transform planet;

    [SerializeField] float radius;

    public int Money;

    private Transform pivot;

    [SerializeField] Transform TurretSpawnPoint;

    public int turretNumber;

    public bool activated;
    public bool activatedOnce = true;

    public int startMoney;

    [SerializeField] TMP_Text levelUpText;
    [SerializeField] TMP_Text HealthText;
    [SerializeField] TMP_Text upgradePrizeText;

    [SerializeField] AudioSource turretBuild;
    [SerializeField] AudioSource upgradeSound;
    void Start()
    {
        gm = FindObjectOfType<GameManager>();
        planetObj = FindObjectOfType<Planet>();

        pivot = center.transform;
        TurretSpawnPoint.parent = pivot;
        TurretSpawnPoint.position += Vector3.up * radius;

        Money = startMoney;
    }

    void Update()
    {
        moneyText.text = Money.ToString();
        upgradePrizeText.text = UpgradePrize.ToString();

        MoneyHandler();

        rotateSpawnPoint();
        if (activated == true)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0) && buttonHolder.activeInHierarchy == false)
            {
                SpawnTurret();
                TurretSpawnPoint.GetComponent<SpriteRenderer>().enabled = false;
                buttonHolder.SetActive(true);
            }
            if (Input.GetKeyDown(KeyCode.Mouse1) && buttonHolder.activeInHierarchy == false)
            {
                TurretSpawnPoint.GetComponent<SpriteRenderer>().enabled = false;
                buttonHolder.SetActive(true);
            }
        }

        if (activated == true && activatedOnce == true)
        {
            TurretSpawnPoint.GetComponent<SpriteRenderer>().enabled = false;
            buttonHolder.SetActive(true);
            activatedOnce = false;
        }

        if (activated == false)
        {
            buttonHolder.SetActive(false);
            TurretSpawnPoint.GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    void MoneyHandler()
    {
        if (Money < CannonPrize)
        {
            CannonButton.enabled = false;
        }
        else if (Money >= CannonPrize)
        {
            CannonButton.enabled = true;
        }
        if (Money < ShieldPrize)
        {
            ShieldButton.enabled = false;
        }
        else if (Money >= ShieldPrize)
        {
            ShieldButton.enabled = true;
        }
        if (Money < LaserPrize)
        {
            LaserButton.enabled = false;
        }
        else if (Money >= LaserPrize)
        {
            LaserButton.enabled = true;
        }
        if (Money < UpgradePrize)
        {
            UpgradeButton.enabled = false;
        }
        else if (Money >= UpgradePrize)
        {
            UpgradeButton.enabled = true;
        }
    }
    public void DisableButtons()
    {
        TurretSpawnPoint.GetComponent<SpriteRenderer>().enabled = true;
        buttonHolder.SetActive(false);
    }

    public void ChangeTurretNumber(int tNumber)
    {
        turretNumber = tNumber;
    }
    void SpawnTurret()
    {
        if (turretNumber == 1)
        {
            turretBuild.Play();
            Money -= CannonPrize;
            BasicTurret basicTurret = Instantiate(basicTurretPrefab, transform.position = TurretSpawnPoint.transform.position, transform.rotation = TurretSpawnPoint.transform.rotation);
            basicTurret.transform.SetParent(planet, true);
        }
        if (turretNumber == 2)
        {
            turretBuild.Play();
            Money -= LaserPrize;
            LaserTurret laserTurret = Instantiate(laserTurretPrefab, transform.position = TurretSpawnPoint.transform.position, transform.rotation = TurretSpawnPoint.transform.rotation);
            laserTurret.transform.SetParent(planet, true);
        }
        if (turretNumber == 3)
        {
            Money -= ShieldPrize;
            ShieldTurret shieldTurret = Instantiate(shieldTurretPrefab, transform.position = TurretSpawnPoint.transform.position, transform.rotation = TurretSpawnPoint.transform.rotation);
            shieldTurret.transform.SetParent(planet, true);
        }
    }

    public void IncreaseLevel()
    {
        Money -= UpgradePrize;
        upgradeSound.Play();
        UpgradePrize += 5;
        gm.lvl += 1;
        planetObj.rotationSpeed += 15;
        gm.MaxHealth += 4;
        levelUpText.DOFade(1, 0.5f).OnComplete(FadeBack);
    }
    void FadeBack()
    {
        levelUpText.DOFade(0, 0.5f);
    }
    void rotateSpawnPoint()
    {
        Vector3 orbVector = Camera.main.WorldToScreenPoint(center.position);
        orbVector = Input.mousePosition - orbVector;
        float angle = Mathf.Atan2(orbVector.y, orbVector.x) * Mathf.Rad2Deg;

        pivot.position = center.position;
        pivot.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
    }
}
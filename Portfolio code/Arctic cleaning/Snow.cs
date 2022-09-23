using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Snow : MonoBehaviour
{
    [SerializeField] LayerMask wallLayer;
    [SerializeField] LayerMask snowLayer;
    [SerializeField] LayerMask grondLayer;
    [SerializeField] LayerMask waterLayer;

    public AudioSource plowSound;
    public AudioSource plowSound2;
    public AudioSource plowSound3;
    public AudioSource waterHitSoundEffect;
    public ParticleSystem ps;

    public bool hitWall;

    GameObject water;

    public GameObject player;
    GameObject snow;
    GameObject spawningSnow;
    public List<GameObject> objects;
    public float snowHighPoint = 0.2f;
    int currentObject = 0;

    public int snowCount;
    int stolenSnowCount;

    RaycastHit hit;

    Vector3 spawnPoint;

    bool beingPushed = false;

    float moveTime = 0.25f;

    bool falling;

    bool fell = false;

    public bool moving;

    void Start()
    {
        snow = GameObject.FindGameObjectWithTag("ExtraSnow");
        spawningSnow = GameObject.FindGameObjectWithTag("ExtraSnow");
        player = GameObject.FindGameObjectWithTag("Player");

        water = GameObject.FindGameObjectWithTag("Water");
    }

    private void Update()
    {
        if (Physics.Raycast(transform.position, -transform.up, 0.2f, waterLayer) && fell == false)
        {
            waterHitSoundEffect.Play();
            ps.Play();
            fell = true;
            water.GetComponent<GameUI>().resetTextActivated = true;
        }


        if (Physics.Raycast(transform.position, -transform.up, 1f, grondLayer))
        {
            falling = true;
        }
        else if(falling == true)
        {
            transform.DOMoveY(-20, 5f);
            player.GetComponent<Player>().plowingSnow = false;
            falling = false;
        }
    }

    void RandomSounds()
    {
        int randomSound = Random.Range(1, 4);

        if (randomSound == 1)
        {
            plowSound.Play();
        }
        if (randomSound == 2)
        {
            plowSound2.Play();
        }
        if (randomSound == 3)
        {
            plowSound3.Play();
        }
    }

    public void ToggleBeingPushed()
    {
        beingPushed = !beingPushed;
    }

    public void PlowSnow()
    {
        moving = true;
        Vector3 moveForward = transform.forward + transform.position;
        transform.DOMove(moveForward, moveTime).OnComplete(TurnMovingFalse);
        RandomSounds();

        if (Physics.Raycast(transform.position, transform.forward, out hit, 0.75f, snowLayer))
        {
            stolenSnowCount = hit.collider.gameObject.GetComponent<Snow>().snowCount;
            AddSnowLoop();
            Destroy(hit.collider.gameObject, 0.2f);
        }
    }
    public void TurnMovingFalse()
    {
        moving = false;
    }

    public void AddSnowLoop()
    {
        while(stolenSnowCount > 0)
        {
            stolenSnowCount--;
            AddSnow();
        }        
    }

    public void AddSnow()
    {
        if (snowHighPoint > 0.1f)
        {
            spawnPoint = new Vector3(transform.position.x, transform.position.y + snowHighPoint, transform.position.z);
            GameObject spawnedSnow = Instantiate(spawningSnow, spawnPoint, Quaternion.identity, gameObject.transform.parent = transform);
            snowHighPoint += 0.2f;
            objects.Add(spawnedSnow);
            currentObject++;
            snowCount++;
        }
    }
}



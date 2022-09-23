using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player : MonoBehaviour
{
    [SerializeField] LayerMask wallLayer;
    [SerializeField] LayerMask snowLayerMask;
    [SerializeField] LayerMask grondLayer;
    [SerializeField] LayerMask waterLayer;

    Rigidbody rb;

    GameObject snow;
    GameObject snow2;

    GameObject water;

    public AudioSource waterHitSoundeffect;
    public AudioSource fallSoundEffect;
    public AudioSource moveSoundEffect1;
    public AudioSource moveSoundEffect2;
    public AudioSource moveSoundEffect3;

    public AudioSource failedToRotate;

    public ParticleSystem ps;

    RaycastHit hit;
    RaycastHit hit2;

    RaycastHit plowSideHit1;
    RaycastHit plowSideHit2;

    public Transform kolaLocation;

    Vector3 lastRotation;
    Vector3 lastPosition;

    public float moveTime;
    float movingTimeR;

    public bool rotating;

    public bool falling;

    public float rotationTime;
    float rotationTimeR;

    float InputY;

    public bool plowingSnow;
    bool pushSnow;

    bool fell = false;

    public bool activated;


    void Start()
    {
        activated = true;
        rb = GetComponent<Rigidbody>();
        snow = GameObject.FindGameObjectWithTag("ExtraSnow");
        snow2 = GameObject.FindGameObjectWithTag("ExtraSnow");

        water = GameObject.FindGameObjectWithTag("Water");
    }

    void Update()
    {
        if (Physics.Raycast(transform.position, -transform.up, 0.2f, waterLayer) && fell == false)
        {
            waterHitSoundeffect.Play();
            ps.Play();
            water.GetComponent<GameUI>().resetTextActivated = true;
            fell = true;
        }
        CheckForFall();
    }

    void CheckForFall()
    {
        if (Physics.Raycast(transform.position, -transform.up, 1f, grondLayer))
        {
            falling = false;
            if (activated == true)
            {
                UpdatingVariables();
                GridMove();
                Rotation();

                if (plowingSnow == true)
                {
                    snow = hit.transform.gameObject;
                    snow.GetComponent<Snow>().ToggleBeingPushed();
                    snow.transform.rotation = transform.rotation;
                }
            }
        }
        else if (falling == false)
        {
            falling = true;
            fallSoundEffect.Play();
            transform.DOMoveY(-20, 5f);
        }
    }

    public void UpdatingVariables()
    {
        InputY = Input.GetAxisRaw("Vertical");

        lastPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        lastRotation = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z);
    }

    void RandomSounds()
    {
        int randomSound = Random.Range(1, 4);
        
        if(randomSound == 1)
        {
            moveSoundEffect1.Play();
        }
        if (randomSound == 2)
        {
            moveSoundEffect2.Play();
        }
        if (randomSound == 3)
        {
            moveSoundEffect3.Play();
        }
    }
    void GridMove()
    {
        if (movingTimeR >= 0)
        {
            movingTimeR -= Time.deltaTime;
        }

        if (movingTimeR <= 0 && rotationTimeR <= 0)
        {
            if (Input.GetButton("Vertical"))
            {
                GridMoveForward();
                movingTimeR = moveTime;
            }
        }
    }
    void GridMoveForward()
    {
        Vector3 moveDirForward = transform.forward + transform.position;
        Vector3 moveDirBack = -transform.forward + transform.position;

        if (InputY == 1)
        {
            if (!Physics.Raycast(kolaLocation.position, kolaLocation.transform.forward, 1f, wallLayer))
            {
                transform.DOMove(moveDirForward, moveTime);
                RandomSounds();

                if (plowingSnow == true)
                {
                    snow.GetComponent<Snow>().PlowSnow();
                }

                if (Physics.Raycast(transform.position, transform.forward, out hit, 1.70f, snowLayerMask))
                {
                    plowingSnow = true;
                }
            }
        }
        if (InputY == -1)
        {
            if (!Physics.Raycast(transform.position, -transform.forward, 1f, wallLayer | snowLayerMask))
            {
                transform.DOMove(moveDirBack, moveTime);
                RandomSounds();

                if (plowingSnow == true)
                {
                    plowingSnow = false;
                    snow.GetComponent<Snow>().ToggleBeingPushed();
                    snow = GameObject.FindGameObjectWithTag("ExtraSnow");
                }
            }
        }
    }

    public void RotateRight()
    {
        rotating = true;
        transform.DOLocalRotate(new Vector3(0, 90f, 0), rotationTime, RotateMode.LocalAxisAdd);
        rotationTimeR = rotationTime;
        RandomSounds();
        if (plowingSnow == true)
        {
            plowingSnow = false;
            snow.GetComponent<Snow>().ToggleBeingPushed();
            snow = GameObject.FindGameObjectWithTag("ExtraSnow");
        }
    }

    public void RotateLeft()
    {
        rotating = true;
        transform.DOLocalRotate(new Vector3(0, -90f, 0), rotationTime, RotateMode.LocalAxisAdd);
        rotationTimeR = rotationTime;
        RandomSounds();
        if (plowingSnow == true)
        {
            plowingSnow = false;
            snow.GetComponent<Snow>().ToggleBeingPushed();
            snow = GameObject.FindGameObjectWithTag("ExtraSnow");
        }
    }

    void Rotation()
    {
        if (rotationTimeR >= 0)
        {
            rotationTimeR -= Time.deltaTime;
        }


        if(plowingSnow == true)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            {
                failedToRotate.Play();
            }
        }

        else if(plowingSnow == false)
        {
            if (movingTimeR <= 0 && rotationTimeR <= 0)
            {
                rotating = false;
                if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
                {
                    rotationTimeR = rotationTime;
                    if (Physics.Raycast(kolaLocation.position, kolaLocation.right, 1f, wallLayer) || Physics.Raycast(gameObject.transform.position, gameObject.transform.right, 1f, wallLayer))
                    {

                    }
                    else if (Physics.Raycast(kolaLocation.position, kolaLocation.right, 2.5f, wallLayer) && Physics.Raycast(kolaLocation.position, kolaLocation.right, 1f, snowLayerMask) || Physics.Raycast(transform.position, transform.right - transform.forward, 2f, wallLayer) && Physics.Raycast(transform.position, transform.right, 1f, snowLayerMask))
                    {

                    }
                    else
                    {
                        rotationTimeR = rotationTime;
                        RotateRight();

                        if (rotating == true && Physics.Raycast(kolaLocation.position, kolaLocation.right, out hit, 1f, snowLayerMask))
                        {
                            snow = hit.collider.gameObject;
                            snow.transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + 90, transform.eulerAngles.z);
                            snow.GetComponent<Snow>().PlowSnow();
                        }
                        if (rotating == true && Physics.Raycast(transform.position, transform.right, out hit2, 1f, snowLayerMask))
                        {
                            snow2 = hit2.collider.gameObject;
                            snow2.transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + 180, transform.eulerAngles.z);
                            snow2.GetComponent<Snow>().PlowSnow();
                        }
                    }
                }
                else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
                {
                    rotationTimeR = rotationTime;
                    if (Physics.Raycast(kolaLocation.position, -kolaLocation.right, 1f, wallLayer) || Physics.Raycast(gameObject.transform.position, -gameObject.transform.right, 1f, wallLayer))
                    {

                    }
                    else if (Physics.Raycast(kolaLocation.position, -kolaLocation.right, 2f, wallLayer) && Physics.Raycast(kolaLocation.position, -kolaLocation.right, 1f, snowLayerMask) || Physics.Raycast(transform.position, -transform.right - transform.forward, 2f, wallLayer) && Physics.Raycast(transform.position, -transform.right, 1f, snowLayerMask))
                    {

                    }

                    else
                    {
                        rotationTimeR = rotationTime;
                        RotateLeft();

                        if (rotating == true && Physics.Raycast(kolaLocation.position, -kolaLocation.right, out hit, 1f, snowLayerMask))
                        {
                            snow = hit.collider.gameObject;
                            snow.transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y - 90, transform.eulerAngles.z);
                            snow.GetComponent<Snow>().PlowSnow();
                        }
                        if (rotating == true && Physics.Raycast(transform.position, -transform.right, out hit2, 1f, snowLayerMask))
                        {
                            snow2 = hit2.collider.gameObject;
                            snow2.transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y - 180, transform.eulerAngles.z);
                            snow2.GetComponent<Snow>().PlowSnow();
                        }
                    }
                }
            }
        }
    }
}


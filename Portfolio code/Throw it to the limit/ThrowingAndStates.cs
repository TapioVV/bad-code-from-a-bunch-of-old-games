using UnityEngine;
using DG.Tweening;
using TMPro;
using Cinemachine;
using UnityEngine.SceneManagement;

//Controls the state of the game, aiming and throwing
public class ThrowingAndStates : MonoBehaviour
{
    enum AimStates { GETREADY, CHOOSEDIRECTION, CHOOSEPOWER, SHOOT, GAMEOVER};
    AimStates AimState;

    [SerializeField] Cinemachine.CinemachineVirtualCamera cmvCam;

    [Header("Center Pivot")]
    [SerializeField] Transform centerPivotTransform;
    Vector3 centerPivotStartRotation = new Vector3(0, 0, 45);
    Vector3 centerPivotEndRotation = new Vector3(0, 0, -45);
    [SerializeField] float centerPivotRotationSpeed;

    [Header("Reticle")]
    [SerializeField] Transform reticleTransform;
    [SerializeField] float reticleStartScale;
    [SerializeField] float reticleEndScale;
    Vector3 reticleStartScaleVector;
    Vector3 reticleEndScaleVector;
    [SerializeField] float reticleScaleSpeed;
    [SerializeField] SpriteRenderer reticleSpriteRenderer;
    [SerializeField] Sprite[] reticleSprites;

    [SerializeField] GameObject throwable;
    GameObject spawnedThrowable;
    [SerializeField] GameObject fakeThrowable;
    [SerializeField] Sprite[] throwableSprites;
    Sprite throwableSprite;
    [SerializeField] float throwPower;

    public bool gameOver = false;
    public bool reallyGameOVer = false;

    public Transform goalLine;
    public Transform limitLine;
    [SerializeField] float goalLineMinHeight;
    [SerializeField] float goalLineMaxHeight;
    [SerializeField] float limitLineMinDistance;
    [SerializeField] float limitLineMaxDistance;

    [SerializeField] Animator playerAnim;

    [SerializeField] GameObject[] backGroundCharacters;

    int scoreAsInt;

    public float score;

    [SerializeField] TMP_Text scoreText;
    [SerializeField] GameObject gameOverText;

    AudioSource audioSource;

    [SerializeField] AudioClip cheerSound;
    [SerializeField] AudioClip tooLowSound;
    [SerializeField] AudioClip tooHighSound;
    [SerializeField] AudioClip gameOverSound;

    bool playGameOverSound = true;

    [SerializeField] float cameraMoveSpeed;

    [SerializeField] GameObject musicObject;


    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        ToGetReady();
    }

    void Update()
    {
        StateChange();
        scoreAsInt = (int)score;
        scoreText.text = scoreAsInt.ToString();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }

        if(cmvCam.transform.position.y < -1)
        {
            cmvCam.transform.position = new Vector3(0, 0, -10);
        }

        if (cmvCam.transform.position.y > 30)
        {
            cmvCam.transform.position = new Vector3(0, 30, -10);
        }

        reticleSpriteRenderer.size = new Vector2(0.84f, reticleTransform.localScale.y);
    }

    // Changes the state of the game. States include aiming and powering up for example.
    void StateChange()
    {
        switch (AimState)
        {
            case AimStates.GETREADY:
                cmvCam.m_Follow = null;
                if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
                {
                    MoveCameraUp();
                }
                if(Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
                {
                    MoveCameraDown();
                }
                
                reticleSpriteRenderer.enabled = false;
                
                if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Mouse0))
                {
                    ToChooseDirection();
                }
                break;
            case AimStates.CHOOSEDIRECTION:
       
                cmvCam.m_Follow = centerPivotTransform.transform;
                if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Mouse0))
                {
                    ToChoosePower();
                }
                break;
            case AimStates.CHOOSEPOWER:
                if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Mouse0))
                {
                    DOTween.Kill("ScalingTween");
                    reticleSpriteRenderer.enabled = false;
                    playerAnim.Play("CharacterThrow");
                }
                break;
            case AimStates.SHOOT:
                cmvCam.m_Follow = spawnedThrowable.transform;
                break;
            case AimStates.GAMEOVER:
                Destroy(musicObject);
                if (reallyGameOVer == true)
                {
                    if(playGameOverSound == true)
                    {
                        audioSource.Play();
                        playGameOverSound = false;
                    }

                    if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Mouse0))
                    {
                        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                    }
                    if (Input.GetKeyDown(KeyCode.Escape))
                    {
                        SceneManager.LoadScene(0);
                    }

                    if(scoreAsInt > PlayerPrefs.GetInt("HighScore"))
                    {
                        PlayerPrefs.SetInt("HighScore", scoreAsInt);
                    }

                    gameOverText.SetActive(true);
                }
                break;
        }
    }

    void ToChooseDirection()
    {
        throwableSprite = throwableSprites[Random.Range(0, 10)];
        fakeThrowable.GetComponent<SpriteRenderer>().sprite = throwableSprite;
        playerAnim.Play("CharacterAim");
        reticleSpriteRenderer.enabled = true;
        reticleSpriteRenderer.sprite = reticleSprites[0];
        reticleSpriteRenderer.color = Color.white;

        centerPivotTransform.rotation = Quaternion.Euler(centerPivotStartRotation);
        centerPivotTransform.DORotate(centerPivotEndRotation, centerPivotRotationSpeed).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear).SetId("RotationTween");
        AimState = AimStates.CHOOSEDIRECTION;
    }

    void ToChoosePower()
    {
        DOTween.Kill("RotationTween");
        reticleSpriteRenderer.sprite = reticleSprites[1];
        //reticleSpriteRenderer.color = Color.red;
        reticleTransform.DOScaleY(reticleEndScale, reticleScaleSpeed).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear).SetId("ScalingTween");
        AimState = AimStates.CHOOSEPOWER;
    }

    public void ToShoot()
    {
        spawnedThrowable = Instantiate(throwable, reticleTransform.position, centerPivotTransform.rotation);
        spawnedThrowable.GetComponent<Rigidbody2D>().AddForce(spawnedThrowable.transform.up * throwPower * reticleTransform.localScale.y, ForceMode2D.Impulse);
        spawnedThrowable.GetComponent<Rigidbody2D>().AddTorque(-reticleTransform.position.x * reticleTransform.localScale.y / 17, ForceMode2D.Impulse);
        spawnedThrowable.GetComponent<SpriteRenderer>().sprite = throwableSprite;
        fakeThrowable.GetComponent<SpriteRenderer>().sprite = null;
        reticleTransform.localScale = reticleStartScaleVector;
        AimState = AimStates.SHOOT;
    }

    void ToGetReady()
    {
        playerAnim.Play("CharacterIdle");
        reticleStartScaleVector = new Vector3(1, reticleStartScale, 1);
        reticleEndScaleVector = new Vector3(1, reticleEndScale, 1);

        reticleTransform.localScale = reticleStartScaleVector;

        RandomizeGoalLinePosition();
        RandomizeGoalLinePosition();
        AimState = AimStates.GETREADY;
    }

    void ToGameOver()
    {
        foreach(GameObject character in backGroundCharacters)
        {
            Destroy(character);
        }
        playerAnim.Play("DeathTransfer");
        audioSource.PlayOneShot(tooHighSound, audioSource.volume + 0.175f);

        AimState = AimStates.GAMEOVER;
    }

    // Randomizez a position for the goal line and the limit line
    void RandomizeGoalLinePosition()
    {
        goalLine.transform.position = new Vector2(0, Random.Range(goalLineMinHeight, goalLineMaxHeight));
        limitLine.transform.position = new Vector2(0, goalLine.transform.position.y + Random.Range(limitLineMinDistance, limitLineMaxDistance));
    }

    // When the thrown object hits the floor it calls this method which changes the state
    public void ThrowableHitFloor()
    {
        if(gameOver == true)
        {
            ToGameOver();
        }
        if(AimState == AimStates.SHOOT && gameOver == false)
        {
            ToGetReady();
        }
    }

    public void PlayCheerSound()
    {
        audioSource.PlayOneShot(cheerSound, audioSource.volume);
    }
    public void PlayTooLowSound()
    {
        audioSource.PlayOneShot(tooLowSound, audioSource.volume);
    }

    public void MoveCameraUp()
    {
        cmvCam.transform.position = new Vector3(0, cmvCam.transform.position.y + cameraMoveSpeed * Time.deltaTime, -10);
    }
    public void MoveCameraDown()
    {
        cmvCam.transform.position = new Vector3(0, cmvCam.transform.position.y - cameraMoveSpeed * Time.deltaTime, -10);
    }
}

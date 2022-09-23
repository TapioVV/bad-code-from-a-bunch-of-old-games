using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using TMPro;
public class Planet : MonoBehaviour
{
    public float rotationSpeed;
    float xInput;

    public bool activated;

    public int Health;

    [SerializeField] TMP_Text healthText;

    SpriteRenderer sr;

    [SerializeField] AudioSource damageSound;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {       
        GetInputs();

        healthText.text = Health.ToString();

        if(activated == true)
        {
            Vector3 rotationAmount = new Vector3(0, 0, -xInput * rotationSpeed * Time.deltaTime);
            transform.Rotate(rotationAmount, Space.Self);
        }

        if(Health <= 0)
        {
            DOTween.KillAll();
            SceneManager.LoadScene(0);
        }
    }
    
    public void TakeDamage()
    {
        Health -= 1;
        damageSound.Play();
        sr.DOColor(Color.red, 0.1f).OnComplete(ResetColor);
    }
    void ResetColor()
    {
        sr.DOColor(Color.white, 0.1f);
    }

    void GetInputs()
    {
        xInput = Input.GetAxisRaw("Horizontal");
    }
}

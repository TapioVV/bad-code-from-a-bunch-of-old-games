using UnityEngine;

public class Parallax : MonoBehaviour
{
    float lenght;
    float startPos;
    public GameObject cam;

    public float parallaxEffect;
    void Start()
    {
        startPos = transform.position.x;
        lenght = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    private void FixedUpdate()
    {
        float temp = cam.transform.position.x * (1 - parallaxEffect);
        float dist = (cam.transform.position.x * parallaxEffect);

        transform.position = new Vector3(startPos + dist, transform.position.y, transform.position.z);

        if (temp > startPos + lenght)
        {
            startPos += lenght;
        }
        else if(temp < startPos - lenght)
        {
            startPos -= lenght;
        }
    }
}

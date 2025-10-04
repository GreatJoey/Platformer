using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    public float parallaxSpeed = 0.5f;
    private float width;
    private float startPos;

    void Start()
    {
        startPos = transform.position.x;
        width = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void Update()
    {
        float distance = (Camera.main.transform.position.x * (1 - parallaxSpeed));
        float offset = distance % width;
        transform.position = new Vector3(startPos + offset, transform.position.y, transform.position.z);
    }
}

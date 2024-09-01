using UnityEngine;

public class ParallaxScript : MonoBehaviour
{
    public GameObject cam;
    public float parallaxEffect;
    private float length, startPos, offset;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position.x;
        offset = startPos;
        length = 32;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float temp = cam.transform.position.x * ( 1 - parallaxEffect);
        float dist = cam.transform.position.x * parallaxEffect;
        transform.position = new Vector3(dist + offset, transform.position.y, transform.position.z);
        
        if (temp > length + startPos)
        {
            startPos += length;
            Transform firstChild = transform.GetChild(0);
            Transform lastChild = transform.GetChild(transform.childCount - 1);
            firstChild.position = new Vector3(lastChild.position.x + length, firstChild.position.y, 10);
            firstChild.SetAsLastSibling();
        }
    }
}

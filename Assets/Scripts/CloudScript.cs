using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudScript : MonoBehaviour
{

    public GameObject parrot;
    ParticleSystem.EmissionModule emission;
    float defaultRateOverTime;

    // Start is called before the first frame update
    void Start()
    {
        emission = GetComponent<ParticleSystem>().emission;
        defaultRateOverTime = emission.rateOverTime.constant;
    }

    // Update is called once per frame
    void Update()
    {
        float multiplier = parrot.GetComponent<Rigidbody2D>().velocity.x / parrot.GetComponent<birdScript>().defaultSlideSpeed;
        emission.rateOverTime = defaultRateOverTime * (1+ multiplier);
    }
}

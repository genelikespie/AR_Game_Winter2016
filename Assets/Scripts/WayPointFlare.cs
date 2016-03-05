using UnityEngine;
using System.Collections;

public class WayPointFlare : MonoBehaviour {

    public Vector3 localSpawnPosition;
    public float acceleration;
    public float startSpeed;

    //private ParticleSystem particles;
    private float currSpeed;

    void Awake()
    {
       // particles = GetComponent<ParticleSystem>();
        //UnityEngine.Assertions.Assert.IsTrue(particles);
    }
    void OnEnable()
    {
        Debug.Log("waypoint enabled");
       //particles.Play();
        transform.localPosition = localSpawnPosition;
        currSpeed = startSpeed;
    }
    void FixedUpdate()
    {
        if (transform.localPosition.y <= 0)
            return;
        transform.localPosition += new Vector3(0, -currSpeed * Time.deltaTime, 0);
        currSpeed += acceleration;
        if (transform.localPosition.y <= 0)
            transform.localPosition = new Vector3(0, 0, 0);
    }
}

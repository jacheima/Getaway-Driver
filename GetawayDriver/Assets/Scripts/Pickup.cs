using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pickup : MonoBehaviour
{
    [SerializeField] protected GameObject myTile;

    public Renderer myRenderer;
    public Collider myCollider;
    public float spinSpeed;

    [Tooltip("This holds the audiosource that is on the coin, the audio clip should be already loaded into the audiosource.")]
    protected AudioSource audioSource;

    protected virtual void Start()
    {
        audioSource = GetComponent<AudioSource>();
        myRenderer = GetComponent<Renderer>();
        myCollider = GetComponent<Collider>();
    }

    protected virtual void Update()
    {
        transform.Rotate(Vector3.up, spinSpeed * Time.deltaTime);
    }
}

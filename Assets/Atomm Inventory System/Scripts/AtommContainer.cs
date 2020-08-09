using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtommContainer : MonoBehaviour
{
    public List<AtommInventory.Slot> slots;
    public List<AtommInventory.Document> documents;

    public AudioClip open, close;

    AudioSource source;

    private void Start()
    {
        source = GetComponent<AudioSource>();
    }

    public void Action()
    {
        if (source.clip == open)
        { source.clip = close; source.Play(); }
        else
        { source.clip = open; source.Play(); }
    }
}
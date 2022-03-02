using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkOfArt : MonoBehaviour
{
    public Texture2D texture2;
    public bool isMatisse = false;

    void Update()
    {
        
    }

    public void Build(Texture2D texture)
    {
        Renderer rend = GetComponent<Renderer>();
        texture2 = texture;
        rend.material.mainTexture = texture;
        gameObject.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            PlayerActions pa = other.gameObject.GetComponent<PlayerActions>();
            pa.currentWorkOfArt = this;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            PlayerActions pa = other.gameObject.GetComponent<PlayerActions>();
            pa.currentWorkOfArt = null;
        }
    }
}

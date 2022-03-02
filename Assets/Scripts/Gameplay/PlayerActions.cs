using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerActions : MonoBehaviour
{
    public List<WorkOfArt> workOfArts = new List<WorkOfArt>();
    public WorkOfArt currentWorkOfArt = null;

    public Text seeActionText;
    public Text stoleActionText;
    public RawImage artRawImage;

    bool isWatching = false;

    void Start()
    {
        seeActionText.enabled = false;
        stoleActionText.enabled = false;
        artRawImage.enabled = false;
    }

    void Update()
    {
        if (currentWorkOfArt != null)
        {
            if (seeActionText.enabled == false)
            {
                seeActionText.enabled = true;
            }
            
            if (isWatching == false && Input.GetKeyDown(KeyCode.V))
            {
                isWatching = true;
                artRawImage.texture = currentWorkOfArt.texture2;
                artRawImage.enabled = true;

            }
            if (isWatching)
            {
                seeActionText.enabled = false;
                stoleActionText.enabled = true;
                if (Input.GetKeyDown(KeyCode.F))
                {
                    workOfArts.Add(currentWorkOfArt);
                    currentWorkOfArt.gameObject.SetActive(false);
                    currentWorkOfArt = null;
                    seeActionText.enabled = false;
                    stoleActionText.enabled = false;
                    isWatching = false;
                    artRawImage.enabled = false;
                }
                else if (Input.GetKeyDown(KeyCode.E))
                {
                    artRawImage.enabled = false;
                    stoleActionText.enabled = false;
                    isWatching = false;
                }
            }


        }
        else
        {
            if (seeActionText.enabled == true)
            {
                seeActionText.enabled = false;
            }
        }
    }
}

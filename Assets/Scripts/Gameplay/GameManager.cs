using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private Object[] trueWorkOfArtTextures;
    private Object[] falseWorkOfArtTextures;
    private List<Object> allWorkOfArtTextures = new List<Object>();

    public List<WorkOfArt> boards = new List<WorkOfArt>();
    public List<WorkOfArt> actifBoards = new List<WorkOfArt>();

    public GameObject player;


    void Desactive()
    {
        foreach (WorkOfArt item in boards)
        {
            item.gameObject.SetActive(false);
        }
    }


    void Start()
    {
        // Récupération des textures
        trueWorkOfArtTextures = Resources.LoadAll("Works_of_art/True", typeof(Texture2D));
        falseWorkOfArtTextures = Resources.LoadAll("Works_of_art/False", typeof(Texture2D));
        foreach (Object obj in trueWorkOfArtTextures)
        {
            allWorkOfArtTextures.Add(obj);
        }
        foreach (Object obj in falseWorkOfArtTextures)
        {
            allWorkOfArtTextures.Add(obj);
        }

        // Création des tableaux
        int nbTextures = allWorkOfArtTextures.Count;

        int nbactiveTextures = 0;

        List<int> textureIndexUse = new List<int>();

        Desactive();

        while (nbactiveTextures < allWorkOfArtTextures.Count)
        {
            for (int boardIndex = 0; boardIndex < boards.Count; boardIndex++)
            {
                if (nbactiveTextures < allWorkOfArtTextures.Count && Random.Range(0,101) > 50)
                {
                    WorkOfArt wOA = boards[boardIndex];

                    //Récupération texture random
                    int index = Random.Range(0, allWorkOfArtTextures.Count);
                    while (textureIndexUse.Contains(index))
                    {
                        index = Random.Range(0, allWorkOfArtTextures.Count);
                    }

                    wOA.Build((Texture2D)allWorkOfArtTextures[index]);
                    foreach (Object texture2D in trueWorkOfArtTextures)
                    {
                        if (texture2D == allWorkOfArtTextures[index])
                        {
                            wOA.isMatisse = true;
                        }
                    }
                    nbactiveTextures++;
                    textureIndexUse.Add(index);
                    actifBoards.Add(wOA);
                }

            }
        }
    }


    public void FinalCountDownHook()
    {
        if (player)
        {
            PlayerActions pAction = player.GetComponent<PlayerActions>();
            int trueArt = 0;
            int falseArt = 0;

            foreach (WorkOfArt item in pAction.workOfArts)
            {
                if (item.isMatisse)
                    trueArt++;
                else
                    falseArt++;
            }
            PlayerPrefs.SetInt("MatisseTrue", trueArt);
            PlayerPrefs.SetInt("MatisseFalse", falseArt);
            PlayerPrefs.SetInt("Total", actifBoards.Count);

        }
        SceneManager.LoadScene("Debrif");
    }

    public void Detected()
    {
        SceneManager.LoadScene("GameOver");
    }

    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    //General Variables
    GameObject mainCamera;
    GameObject Player;

    public GameObject pauseMenu;
    bool gameIsPaused = false;

    //Level 1 Variables 

    [Header("Level 1")]
    public GameObject directionalLightObject;
    public GameObject truckSpawner;
    public bool familyCrossing;
    //Level 4 Variables
    public bool isHome;

    [Header("Level 2")]
    public GameObject perro;
    public GameObject perroLaberinto;
    public GameObject perro2;
    public Transform puertaLaberinto;
    public Vector3 lastSeenPos;
    public GameObject[] preBolsaBasura;
    public GameObject[] BolsaBasura;
    public Transform[] basuraSpawn;

    [Header("Level 6")]
    public PatoSpawner patoSpawner;
    bool patosSpawneados = false;
    public FadeTransparentMaterials cartelFinal;
    public GameObject fadeToBlack;

    [Header("Level8")]
    public GameObject botonesMenu;
    public GameObject botonesNiveles;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
        Player = GameObject.FindGameObjectWithTag("Player");
        //changeLightLevel1 = ChangeLightLevel1(0.1f);
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseMenu == null)
            {
                return;
            }
            else
            {
                if (gameIsPaused)
                {
                    ResumeGame();
                }
                else
                {
                    PauseGame();
                }
            }
        }
    }

    private void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            Cursor.visible = true;
        }
        else
        {
            Cursor.visible = false; 
        }
    }

    public void Die()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }

    public void HaveCrossed()
    {
        mainCamera.GetComponent<CameraFollow>().FollowFamily();
        Player.GetComponent<CharacterController>().enabled = false;
        familyCrossing = true;
        truckSpawner.GetComponent<TruckSpawner>().SpawnTruck();
    }

    public void MidCross()
    {
        familyCrossing = true;
    }

    public void DeadFamily()
    {
        Player.GetComponent<CharacterController>().enabled = true;
        mainCamera.GetComponent<CameraFollow>().FollowPlayer();

        StartCoroutine(FadeToBlack());
    }

    IEnumerator FadeToBlack()
    {
        while (true)
        {
            mainCamera.GetComponentInChildren<MeshRenderer>().enabled = true;
            yield return new WaitForSeconds(3);
            GoToScene(2);
            StopAllCoroutines();
        }
    }

    public void Ruido()
    {
        //animator = perro.gameObject.GetComponent<Animator>();
        //animator.SetBool("Lanza", true);
    }

    public void PlayerDetected(Vector3 position)
    {
        lastSeenPos = position;
        perro.GetComponent<Animator>().SetBool("PlayerDetect", true);
    }

    public void PerroLaberintico(int fase)
    {
        switch (fase)
        {
            case 1:
                perro.GetComponent<Animator>().SetBool("Laberinto", true);
                break;
            case 2:
                perro.SetActive(false);
                perroLaberinto.SetActive(true);
                perroLaberinto.transform.position = puertaLaberinto.transform.position;
                break;
        }
    }

    public void BasuraBipeda(string nombreBasura)
    {
        if (nombreBasura == "PreBolsaBasura1")
        {
            preBolsaBasura[0].SetActive(false);
            BolsaBasura[0].SetActive(true);
            BolsaBasura[0].transform.position = basuraSpawn[0].transform.position;
        }
    }

    public void CartelDelFinalVisible(int num)
    {
        switch (num)
        {
            case 1:
                cartelFinal.FadeIn();
                break;
            case 2:
                cartelFinal.FadeOut();
                break;
        }
    }

    public void CallEndingDucks()
    {
        if (patosSpawneados == false)
        {
            patoSpawner.Spawn();
            cartelFinal.FadeOut();
            fadeToBlack.SetActive(true);
            patosSpawneados = true;
            StartCoroutine(WaitForSeconds(10));
        }
    }

    IEnumerator WaitForSeconds(float time)
    {
        yield return new WaitForSeconds(time);
        GoToScene(8);
    }

    void PauseGame()
    {
        Cursor.visible = true;
        gameIsPaused = true;
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        Cursor.visible = false;
        gameIsPaused = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void GoToScene(int scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void SelectScene()
    {
        if (botonesMenu.activeSelf == true)
        {
            botonesMenu.SetActive(false);
            botonesNiveles.SetActive(true);
        }
        else
        {
            botonesNiveles.SetActive(false);
            botonesMenu.SetActive(true);
        }
    }

    public void QuitApp()
    {
        Application.Quit();
        Debug.Log("Application has quit");
    }
}
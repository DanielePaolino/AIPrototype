using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    public static LevelController Instance { get; private set; }
    public bool HouseIsBurning { get; set; } = false;
    public Vector3 BurningRoom { get; set; }
    public bool EmergencyCallDone { get; set; } = false;
    public bool FiremenTruckHasArrivedAtEmergencyLocation { get; set; } = false;
    public bool FiremanIsReturnedToTruck { get; set; } = false;

    private int speedUp = 6;

    [SerializeField] private GameObject fireEffectPrefab;

    private GameObject fireEffectSpawned;

    private void Awake()
    {
        Singleton();
    }

    private void Update()
    {
        CheckUserInput();
    }

    void Singleton()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;
    }

    public void StartFire()
    {
        if (HouseIsBurning)
            fireEffectSpawned = Instantiate(fireEffectPrefab, BurningRoom, Quaternion.identity);

    }

    public void ExtinguishFire()
    {
        fireEffectSpawned.GetComponent<ParticleSystem>().Stop();
        Destroy(fireEffectSpawned, 3f);
    }

    private void CheckUserInput()
    {
        if (Input.GetKeyDown(KeyCode.R))
            SceneManager.LoadScene("SampleScene");

        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();

        if (Input.GetKeyDown(KeyCode.S))
            Time.timeScale = (Time.timeScale == speedUp) ? 1 : speedUp;
    }

}

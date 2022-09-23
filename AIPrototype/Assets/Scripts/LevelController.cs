using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public static LevelController Instance { get; private set; }
    public bool HouseIsBurning { get; set; } = false;
    public Vector3 BurningRoom { get; set; }
    public bool EmergencyCallDone { get; set; } = false;
    public bool FiremenTruckHasArrivedAtEmergencyLocation { get; set; } = false;
    public bool FiremanIsReturnedToTruck { get; set; } = false;

    [SerializeField] private GameObject fireEffectPrefab;

    private GameObject fireEffectSpawned;

    private void Awake()
    {
        Singleton();
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

}

using System;
using UnityEngine;


/// <summary>
/// Se encarga de interactuar con los GO tirando rayos desde la camara
/// </summary>
public class PlayerInteraction : MonoBehaviour
{
    [Header("Distancia para agarrar")]
    [SerializeField] float interactionDistance;

    [Header("Transform del GO que controla la mira")]
    [SerializeField] Transform cameraTransf;

    // private GameManager gameManager;
    private PlayerInventory playerInventory;
    // private PuzzleAltar puzzleAltar;
    // private AudioManager audioManager;
    private Flashlight flashLight;

    private KeyCode interactionKey;

    private void Awake()
    {
        playerInventory = FindObjectOfType<PlayerInventory>();
        flashLight = FindObjectOfType<Flashlight>();

        interactionKey = KeyCode.E;
    }

    private void Update()
    {
        Interact(Input.GetKeyDown(interactionKey));
    }
    void Interact(bool input) //Puede implementarse con PuzzleInteraction
    {
        if (input)
        {
            RunMethod(() => PickUpPuzzlePiece(), "PPuzzle");
            RunMethod(() => PickUpBattery(), "Battery");
        }
    }
    void RunMethod(Action method, string tag)
    {
        if (CompareObject(CastRay(), tag))
        {
            method();
        }
    }

    public GameObject CastRay()
    {
        RaycastHit rayCastInfo;

        if (Physics.Raycast(cameraTransf.position, cameraTransf.forward, out rayCastInfo, interactionDistance))
        {
            return rayCastInfo.transform.gameObject;
        }
        return null;
    }

    public bool CompareObject(GameObject go, string tag)
    {
        if (go == null)
            return false;

        if (go.CompareTag(tag))
        {
            return true;
        }
        return false;
    }

    void PickUpPuzzlePiece()
    {
        GameObject go = CastRay();

        if (go == null) return;

        go.SetActive(false);

        // playerInventory.AddPuzzle(go);
        // PlayPickUpSound();
    }


    void PickUpBattery()
    {
        GameObject go = CastRay();

        if (go == null) return;

        go.SetActive(false);

        if (flashLight == null) Debug.LogWarning("Falta asignar FlashLight Script en PlayerInteraction");

        flashLight.ChargeBattery();
        // PlayPickUpSound();
    }


}
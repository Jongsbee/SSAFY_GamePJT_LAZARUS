using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using StarterAssets;

public class CharacterLobbyController : MonoBehaviour
{

    // 싱글 플레이 전용
    public static CharacterLobbyController instance { get; private set; }

    //ThirdPersonControllerForSurvive thirdPersonControllerForSurvive;
    StarterAssets.StarterAssetsInputs starterAssetsInputs;
    PlayerInput playerInput;
    GameObject hand;
    Scene scene;

    bool check = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance);
            instance = this;
        }

        scene = SceneManager.GetActiveScene();
        //thirdPersonControllerForSurvive = GetComponent<ThirdPersonControllerForSurvive>();
        playerInput = GetComponent<PlayerInput>();
        starterAssetsInputs = GetComponent<StarterAssetsInputs>();
        hand = GameObject.Find("hand");
        hand.SetActive(false);
        playerInput.actions.Disable();

    }

    private void Start()
    {



    }


    // Update is called once per frame
    void Update()
    {
        if (scene.name == "JaeYoung_Lobby" || scene.name == "HaeSuk_Lobby")
        {
            
            //playerInput.DeactivateInput();
        }
        else
        {
            if (!check)
            {
                hand.SetActive(true);
                playerInput.actions.Enable();
                starterAssetsInputs.cursorInputForLook = true;
                starterAssetsInputs.cursorLocked = true;
                check = true;
            }
            
        }

    }
}

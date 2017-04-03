using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
    /*
     * Buttons
    Square  = joystick button 0
    X       = joystick button 1
    Circle  = joystick button 2
    Triangle= joystick button 3
    L1      = joystick button 4
    R1      = joystick button 5
    L2      = joystick button 6
    R2      = joystick button 7
    Share   = joystick button 8
    Options = joystick button 9
    L3      = joystick button 10
    R3      = joystick button 11
    PS      = joystick button 12
    PadPress= joystick button 13

Axes:
    LeftStickX      = X-Axis
    LeftStickY      = Y-Axis (Inverted?)
    RightStickX     = 3rd Axis
    RightStickY     = 4th Axis (Inverted?)
    L2              = 5th Axis (-1.0f to 1.0f range, unpressed is -1.0f)
    R2              = 6th Axis (-1.0f to 1.0f range, unpressed is -1.0f)
    DPadX           = 7th Axis
    DPadY           = 8th Axis (Inverted?)
    */

    private GameManager gameManager;
    private Camera playerCamera;
    private Animator animator;
    private CharacterController characterController;
    private bool RightShoot = true;

    private RaycastHit rayHit = new RaycastHit();
    private Ray ray = new Ray();
    
    public GameObject SpawnBulletRight;
    public GameObject SpawnBulletLeft;
    public GameObject GunRight;
    public GameObject GunLeft;
    public GameObject BulletPrefab;

    public int movementSpeed = 8;
    public float BulletSpeed = 10000.0f;
    public float MoveSpeed;
    public Vector3 positioDest;

    private GameObject eventSystem;

    void Awake(){
        gameManager = GameObject.FindGameObjectWithTag("GameManager")
                                .GetComponent<GameManager>();

        characterController = GetComponent<CharacterController>();

        playerCamera = GetComponentInChildren<Camera>();

        eventSystem = GameObject.FindGameObjectWithTag("EventSystem");
    }
    void Start () {
        positioDest = this.transform.position;
        animator = GetComponent<Animator>();

        Debug.Log("Joysticks: ");
        foreach(var joystick in Input.GetJoystickNames())
        {
            Debug.Log(joystick);
        }
        
    }
	
    void MoveToDestPosition()
    {
        float step = MoveSpeed * Time.deltaTime;
        Vector3 newPosition = Vector3.MoveTowards(this.transform.position, positioDest, step);
        newPosition.y = transform.position.y;
        transform.position = newPosition;
    }

	void Update () {

        MoveToDestPosition();

        if (gameManager.gameStarted)
        {
            /* Nao sei se funciona
             * Testar com o controle do PS4
             * LEMBRAR DE CONFIGURAR NO INPUT DA UNITY
             
            Vector3 movementDirection = Vector3.zero;
            movementDirection.x = Input.GetAxis("Vertical") * movementSpeed;
            movementDirection.z = Input.GetAxis("Horizontal") * movementSpeed;
            characterController.Move(movementDirection * Time.deltaTime);
            */

            if (Input.GetMouseButtonDown(0) || Input.GetButtonDown("Fire1"))
            {
                var gazeInput = eventSystem.GetComponent<GazeInputModule>();
                var gazeGO = gazeInput.GetCurrentGameObject();

                if(gazeGO != null && gazeGO.tag == "PositionMove")
                {
                    positioDest = gazeGO.transform.position;
                    var cubeController = gazeGO.GetComponent<CubeController>();
                    cubeController.PlayNavigateToSound();
                    return;
                }
                if(gazeGO != null){
                    Debug.Log(gazeGO.tag);
                    Debug.Log(gazeGO.gameObject.layer);
                }
                
                if(gazeGO != null && gazeGO.layer == LayerMask.NameToLayer("UI")){
                    return;
                }

                var camera = GetComponentInChildren<Camera>();

                var spawnPosition = Vector3.zero;

                if (RightShoot)
                {
                    spawnPosition = SpawnBulletRight.transform.position;
                    GunRight.GetComponent<GvrAudioSource>().Play();
                    animator.SetTrigger("gunRightShoot");
                }else
                {
                    spawnPosition = SpawnBulletLeft.transform.position;
                    GunLeft.GetComponent<GvrAudioSource>().Play();
                    animator.SetTrigger("gunLeftShoot");
                }

                RightShoot = !RightShoot;
                var bullet = (GameObject)Instantiate(BulletPrefab, spawnPosition, Quaternion.identity);
                bullet.GetComponent<Rigidbody>().AddForce(camera.transform.forward * BulletSpeed);
                Object.Destroy(bullet, 10.0f);



            }
        }


    }
}

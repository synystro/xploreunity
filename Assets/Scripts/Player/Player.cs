using UnityEngine.EventSystems;
using UnityEngine;
using System;

public class Player : MonoBehaviour {

    [Header("Movement")]
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;

    [Header("Inventory")]
    public GameObject inventoryPanel;

    private float currentSpeed;

    private Vector3 mousePos;
    private Vector2 inputDirection;

    private Animator anim;

    void Start() {

        // set initial speed.
        currentSpeed = walkSpeed;

        // get animator component.
        anim = GetComponent<Animator>();

    }

    void Update() {

        // get player input.
        CheckInput();
        // change player face direction.
        FaceDirection();

    }

    private void CheckInput() {

        #region mouse input

        // get mouse input pos.
        mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);

        // mouse input outside UI.
        if (Input.GetMouseButtonDown(1) & !EventSystem.current.IsPointerOverGameObject()) {

            CheckForObstacle();

            CheckAboveGround();

            // get tile name on mouse click.
            //GetTileNameOnClick();

        }

        #endregion

        #region keyboard input

        // run.
        if (Input.GetButton("Run")) {
            currentSpeed = runSpeed;
        } else {
            currentSpeed = walkSpeed;
        }

        // inventory.
        if (Input.GetButtonDown("Inventory")) { inventoryPanel.SetActive(!inventoryPanel.activeSelf); }

        #endregion

        #region player movement

        inputDirection = new Vector2(
            Mathf.RoundToInt(Input.GetAxis("Horizontal")),
            Mathf.RoundToInt(Input.GetAxis("Vertical"))
            );

        CheckForCollision();

        MovePlayer();

        #endregion

    }

    private void CheckForObstacle() {

        Collider2D obstacle = Physics2D.OverlapPoint(mousePos, LayerMask.GetMask("Obstacle"));

        if (obstacle != null) {
            // resource
            if (obstacle.GetComponent<Resource>()) {
                obstacle.GetComponent<Resource>().Extract();
            } else if (obstacle.GetComponent<Chest>()) {
                // chest
                obstacle.GetComponent<Chest>().Interact();
            }

        }
    }

    private void CheckAboveGround() {

        Collider2D aboveGround = Physics2D.OverlapPoint(mousePos, LayerMask.GetMask("Aboveground"));
      
        if (aboveGround != null) {
            // resource.
            if (aboveGround.GetComponent<Resource>()) {
                aboveGround.GetComponent<Resource>().Extract();
            }
        }
    }

    private void GetTileNameOnClick() {

        string tileName = Planet.instance.GetTileAt((int)mousePos.x, (int)mousePos.y).type.ToString();
        print(tileName);

    }

    private void MovePlayer() {
        // move player.
        transform.Translate(inputDirection * currentSpeed * Time.deltaTime);
    }

    private void CheckForCollision() {
        // raycast for collisions.
        Debug.DrawRay(transform.position, inputDirection * 0.1f, Color.red);
        if (Physics2D.Raycast(transform.position, inputDirection, 0.1f, LayerMask.GetMask("Obstacle"))) {
            transform.Translate(-inputDirection * currentSpeed * Time.deltaTime);
            return;
        }
    }

    private void FaceDirection() {

        // face up.
        if (inputDirection.x == 0 && inputDirection.y == 1) {
            anim.SetInteger("direction", 1);
        }
        // face right_up.
        if (inputDirection.x == 1 && inputDirection.y == 1) {
            anim.SetInteger("direction", 2);
        }
        // face right.
        if (inputDirection.x == 1 && inputDirection.y == 0) {
            anim.SetInteger("direction", 3);
        }
        // face right_down.
        if (inputDirection.x == 1 && inputDirection.y == -1) {
            anim.SetInteger("direction", 4);
        }
        // face down.
        if (inputDirection.x == 0 && inputDirection.y == -1) {
            anim.SetInteger("direction", 5);
        }
        // face left_down
        if (inputDirection.x == -1 && inputDirection.y == -1) {
            anim.SetInteger("direction", 6);
        }
        // face left
        if (inputDirection.x == -1 && inputDirection.y == 0) {
            anim.SetInteger("direction", 7);
        }
        // face left_up
        if (inputDirection.x == -1 && inputDirection.y == 1) {
            anim.SetInteger("direction", 8);
        }
        // idle.
        if (inputDirection.x == 0 && inputDirection.y == 0) {
            anim.SetInteger("direction", 0);
        }

    }

}

// PlayerController.cs
// CENG 454 – HW3: Core Breach
// Author: ABDIRAHMAN HUSSEIN | Student ID: 230446614
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 8f;
    [SerializeField] private BaseWeapon weapon;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        HandleMovement();
        HandleShooting();
    }

    private void HandleMovement()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 move = new Vector3(h, 0, v).normalized;
        rb.linearVelocity = new Vector3(
            move.x * moveSpeed, rb.linearVelocity.y, move.z * moveSpeed);

        // Rotate player to face mouse position
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Vector3 lookDir = hit.point - transform.position;
            lookDir.y = 0;
            if (lookDir != Vector3.zero)
                transform.rotation = Quaternion.LookRotation(lookDir);
        }
    }

    private void HandleShooting()
    {
        if (Input.GetMouseButton(0))
            weapon?.Fire();
    }
}
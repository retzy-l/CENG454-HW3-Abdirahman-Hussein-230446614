// FlankingStrategy.cs
// CENG 454 – HW3: Core Breach
// Author: ABDIRAHMAN HUSSEIN | Student ID: 230446614
using UnityEngine;

public class FlankingStrategy : IMovementStrategy
{
    private float flankOffset = 5f;
    private float approachSpeed = 1.2f;

    public void Move(Transform enemy, Transform target, float speed)
    {
        // Move toward core but slightly to the side
        Vector3 toTarget = target.position - enemy.position;
        float distance = toTarget.magnitude;

        // Apply a lateral offset that reduces as enemy gets closer
        Vector3 right = Vector3.Cross(toTarget.normalized, Vector3.up);
        float offsetAmount = Mathf.Clamp(distance * 0.3f, 0f, flankOffset);
        Vector3 flankTarget = target.position + right * offsetAmount;

        Vector3 direction = (flankTarget - enemy.position).normalized;
        enemy.position += direction * speed * approachSpeed * Time.deltaTime;
        enemy.LookAt(target);
    }
}
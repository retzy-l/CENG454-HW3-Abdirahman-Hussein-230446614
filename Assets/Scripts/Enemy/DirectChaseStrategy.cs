// DirectChaseStrategy.cs
// CENG 454 – HW3: Core Breach
// Author: ABDIRAHMAN HUSSEIN | Student ID: 230446614
using UnityEngine;

public class DirectChaseStrategy : IMovementStrategy
{
    public void Move(Transform enemy, Transform target, float speed)
    {
        Vector3 direction = (target.position - enemy.position).normalized;
        enemy.position += direction * speed * Time.deltaTime;
        enemy.LookAt(target);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public float followSpeed = 2f;
    public Transform player;
    public float minX = -10000;
    public float maxX = 10000;
    public float minY = -10000;
    public float maxY = 10000;
    
    void FixedUpdate()
    {
        Vector3 newPos = new Vector3(player.position.x, player.position.y, -10);
        if (player.position.x < minX)
        {
            newPos.x = minX;
        }

        if (player.position.x > maxX)
        {
            newPos.x = maxX;
        }

        if (player.position.y < minY)
        {
            newPos.y = minY;
        }

        if (player.position.y > maxY)
        {
            newPos.y = maxY;
        }

            transform.position = Vector3.Slerp(transform.position, newPos, followSpeed * Time.deltaTime);
    }
}

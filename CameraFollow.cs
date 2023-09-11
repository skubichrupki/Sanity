using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float cameraFollowSpeed = 4f;
    [SerializeField] private float offsetY = 4f;

    // Update is called once per frame
    private void Update()
    {
        CameraUpdate();
    }


    private void CameraUpdate()
    {
    // new position of the player in next frame
            Vector3 newPlayerPos = new Vector3(player.position.x, 0f + offsetY, -10f);

            transform.position = Vector3.Slerp(transform.position, newPlayerPos, cameraFollowSpeed * Time.deltaTime);
    }

}

    
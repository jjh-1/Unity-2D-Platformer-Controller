using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameObject currCheckPoint;

    private PlayerController2D player;

    private void Start()
    {
        player = FindObjectOfType<PlayerController2D>();
    }

    public void RespawnPlayer()
    {
        player.transform.position = currCheckPoint.transform.position;
    }
}

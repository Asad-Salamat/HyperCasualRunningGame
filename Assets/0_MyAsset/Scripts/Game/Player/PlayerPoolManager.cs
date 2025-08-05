using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPoolManager : MonoBehaviour
{
    [SerializeField] private GameObject[] players;
    public static PlayerPoolManager i;
    [HideInInspector] public List<PlayerController> playerPool;
    private int currentPlayerIndex = 0;
    private int poolCount = 80;

    //ーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーー
    void Awake()
    {
        i = this;
    }

    void Start()
    {
        currentPlayerIndex = PlayerPrefs.GetInt("playerIndex", 0);
        PreparePlayerPool();
        // for (int i = 0; i < transform.childCount; i++)
        // {
        //     Transform child = transform.GetChild(i);
        //     if (child.TryGetComponent(out PlayerController player))
        //     {
        //         player.Die(true);
        //     }
        // }
    }
    private void PreparePlayerPool()
    {
        for (int i = 0; i < poolCount; i++)
        {
            GameObject playerObj = Instantiate(players[currentPlayerIndex], transform);
            playerObj.SetActive(false);
            playerPool.Add(playerObj.GetComponent<PlayerController>());
        }
    }

    //ーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーーー
    public void EnqueuePlayer(PlayerController player)
    {
        player.transform.parent = transform;
        playerPool.Add(player);
    }

    public PlayerController DequeuePlayer()
    {
        PlayerController player = playerPool[0];
        player.gameObject.SetActive(true);
        playerPool.RemoveAt(0);
        return player;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorHub : MonoBehaviour
{
    public SO_Door door;
    public SpriteRenderer lightCheck;

    public string playerTag;
    public string playerCollectTag;
    public GameObject infoBox;

    [SerializeField]
    private GameManager _gameManager;

    private void Awake()
    {
        if (_gameManager == null)
        {
            _gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        }
    }

    private void Update()
    {
        if(door.isOpen)
        {
            lightCheck.color = Color.green;
        }
        else
        {
            lightCheck.color= Color.red;
        }
    }

    private void OnValidate()
    {
        if(door != null)
        {
            gameObject.name = "door_" + door.level.name;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collision with " + collision.name);

        if (collision.CompareTag(playerTag))
        {
            infoBox.SetActive(true);

            if (door.isOpen)
            {
            }
        }

        if (collision.CompareTag(playerCollectTag))
        {
            _gameManager.LoadScene(door.level.sceneName);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(playerTag))
        {
            infoBox.SetActive(false);
        }
    }
}

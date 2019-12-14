using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour

{

    public Camera fpsCamera;
    public GameObject Player;
    public float dis;
    public float range = 4f;

    PlayerHealth playerHealth;

    // Start is called before the first frame update
    void Start()
    {
        playerHealth = Player.GetComponent<PlayerHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        dis = Vector3.Distance(transform.position, Player.transform.position);

        if (Input.GetKeyDown("e"))
        {
            RaycastHit hit;
            if (Physics.Raycast(fpsCamera.transform.position, fpsCamera.transform.forward, out hit, range))
            {
                if (hit.transform.tag == "Exit" && dis < 3f)
                {
                    playerHealth.saveData();
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                }
            }
        }

    }
}

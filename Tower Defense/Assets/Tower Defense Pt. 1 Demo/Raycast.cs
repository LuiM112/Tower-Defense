using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Raycast : MonoBehaviour
{
    public Camera camera;

    public GameObject coinText;

    public int coin = 3;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hitInfo))
            {
                if (hitInfo.collider.gameObject.GetComponent<EnemyDemo>() != null)
                {
                    hitInfo.collider.gameObject.GetComponent<EnemyDemo>().health--;
                    Debug.Log("Enemy's health is " + hitInfo.collider.gameObject.GetComponent<EnemyDemo>().health);
                    if (hitInfo.collider.gameObject.GetComponent<EnemyDemo>().health == 0)
                    {
                        Destroy(hitInfo.collider.gameObject);
                        coinText.GetComponent<TextMeshProUGUI>().text = coin.ToString().PadLeft(2, '0');
                    }
                }
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineDetector : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Mine")
        {
            ExploderBehaviour eb = transform.parent.GetComponent<ExploderBehaviour>();
            eb.OnMineFound(other.gameObject);
            gameObject.SetActive(false);
        }
    }
}

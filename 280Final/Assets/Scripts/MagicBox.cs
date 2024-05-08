using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBox : MonoBehaviour
{
    public GameObject fire;
    public GameObject ice;
    public GameObject star;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject other = collision.gameObject;
        if (other.tag==("Player"))
        {
            Vector3 holdPos = this.gameObject.transform.position;
            Quaternion holdRot = this.gameObject.transform.rotation;
            this.gameObject.SetActive(false);
            Destroy(this.gameObject);
            int pick = Random.Range(0, 3);
            if (pick == 0)
            {
                Instantiate(fire, holdPos, holdRot);
            }
            else if (pick == 1)
            {
                Instantiate(ice, holdPos, holdRot);
            }
            else
            {
                Instantiate(star, holdPos, holdRot);
            }
        }
    }
}

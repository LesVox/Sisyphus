using UnityEngine;
using System.Collections;

public class Shot : MonoBehaviour {

    public int damage = 1;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("DEDD");      
        if(col.gameObject.GetComponent<Enemy>() != null)
        {
            col.gameObject.GetComponent<Enemy>().health -= damage;
        }
        if (col.gameObject.GetComponent<PlayerControl>() == null)
        {
            Destroy(this.gameObject);
        }
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    PlayerStats stats;
    // Start is called before the first frame update
    void Start()
    {
        stats = GetComponent<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void sword(){
        if (stats.gold >= 10) {
            stats.damage += 2;
            stats.gold -= 10;
        }
    }

    public void armour(){
        if (stats.gold >= 10) {
            stats.armour += 2;
            stats.gold -= 10;
        }
    }
}

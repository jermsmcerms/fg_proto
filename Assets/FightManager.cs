using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightManager : MonoBehaviour
{
    [SerializeField] private Fighter[] fighters;

    // Start is called before the first frame update
    void Start()
    {
        fighters = GetComponents<Fighter>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

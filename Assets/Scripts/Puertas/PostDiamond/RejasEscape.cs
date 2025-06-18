using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RejasEscape : MonoBehaviour
{
   [SerializeField] private bool _diamondWasPicked = false;
   [SerializeField] private GameObject grillObject;

    private void Awake()
    {
        grillObject.SetActive(false);
    }

    private void Update()
    {
        if (_diamondWasPicked)
        {
            ActivateGrill();
        }
    }

    private void ActivateGrill()
    {
        grillObject.SetActive(true);
    }


    public void DiamondWasPick(bool Picked)
    {
        _diamondWasPicked = Picked;
    }
}
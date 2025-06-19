using UnityEngine;

public class RejasEscape : MonoBehaviour
{
    [SerializeField] private bool _diamondWasPicked = false;
    [SerializeField] private GameObject[] grillObject;

    private void Awake()
    {
        for (int i = 0; i < grillObject.Length; i++)
        {
            if (grillObject[i] != null)
                grillObject[i].SetActive(false);
        }
    }

    private void ActivateGrill()
    {
        for (int i = 0; i < grillObject.Length; i++)
        {
            if (grillObject[i] != null)
                grillObject[i].SetActive(true);
        }

        Debug.Log("Rejas activadas.");
    }

    public void DiamondWasPick(bool Picked)
    {
        _diamondWasPicked = Picked;

        if (_diamondWasPicked)
        {
            ActivateGrill();
        }
    }
}

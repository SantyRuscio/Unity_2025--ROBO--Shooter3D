using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BulletsUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI bulletText;

    private Weapon currentWeapon;

    public void SetWeapon(Weapon newWeapon)
    {
        if (currentWeapon != null)
            currentWeapon.AmmoChanged -= UpdateBulletText; 

        currentWeapon = newWeapon;

        if (currentWeapon != null)
            currentWeapon.AmmoChanged += UpdateBulletText; 

        UpdateBulletText();
    }

    public void UpdateBulletText()
    {
        if (currentWeapon != null)
        {
            bulletText.text = "" + currentWeapon.GetRemainingBullets();
        }
        else
        {
            bulletText.text = "Sin arma";
        }
    }
}

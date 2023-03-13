using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterMenu : MonoBehaviour
{

    // Text fields
    public Text levelText, hitpointText, coinText, upgradeMainCostText, xpText, upgradeSubCostText;

    // logic
    public Image mainWeaponSprite;
    public Image subWeaponSprite;
    public RectTransform xpBar;

    //weapon Upgrade
    public void OnMainUpgradeClick()
    {
        if (GameManager.instance.TryUpgradeMainWeapon())
            UpdateMenu();
    }

    public void OnSubUpgradeClick()
    {
        if (GameManager.instance.TryUpgradeSubWeapon())
            UpdateMenu();
    }


    // Upgrade character information
    public void UpdateMenu()
    {
        // weapon
        mainWeaponSprite.sprite = GameManager.instance.mainWeaponSprites[GameManager.instance.mainWeapon.weaponLevel];
        subWeaponSprite.sprite = GameManager.instance.subWeaponSprites[GameManager.instance.subWeapon.weaponLevel];
        upgradeMainCostText.text = GameManager.instance.mainWeaponPrices[GameManager.instance.mainWeapon.weaponLevel].ToString();
        upgradeSubCostText.text = GameManager.instance.subWeaponPrices[GameManager.instance.subWeapon.weaponLevel].ToString(); ;

        // meta
        levelText.text = GameManager.instance.GetCurrentLevel().ToString();
        hitpointText.text = GameManager.instance.player.hitPoint.ToString();
        coinText.text = GameManager.instance.coin.ToString();

        // xp bar
        int currLevel = GameManager.instance.GetCurrentLevel();
        if(currLevel == GameManager.instance.xpTable.Count)
        {
            xpText.text = GameManager.instance.exp.ToString() + " total experience points";
            xpBar.localScale = Vector3.one;
        }
        else
        {
            int precLevelXp = GameManager.instance.GetXptolevel(currLevel - 1);
            int currLevelXp = GameManager.instance.GetXptolevel(currLevel);

            int diff = currLevelXp - precLevelXp;
            int currXpIntoLevel = GameManager.instance.exp - precLevelXp;

            float completiomRatio = (float)currXpIntoLevel / (float)diff;
            xpBar.localScale = new Vector3(completiomRatio, 1, 1);
            xpText.text = currXpIntoLevel.ToString() + " / " + diff;
        }
    }

}

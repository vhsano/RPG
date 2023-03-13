using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private void Awake()
    {
        if (GameManager.instance != null)
        {
            Destroy(gameObject);
            Destroy(player.gameObject);
            Destroy(floatingTextManager.gameObject);
            Destroy(hub);
            Destroy(menu);
            Destroy(settingMenu);
            return;
        }
        instance = this;
        PlayerPrefs.DeleteAll();
        SceneManager.sceneLoaded += LoadState;
        SceneManager.sceneLoaded += OnSenceLoaded;
        
    }
    // Resoureces
    public List<Sprite> subWeaponSprites;
    public List<Sprite> mainWeaponSprites;
    public List<int> mainWeaponPrices;
    public List<int> subWeaponPrices;
    public List<int> xpTable;
    public bool Swap;

    //References
    public Player player;
    public Sword mainWeapon;
    public Bow subWeapon;
    public FloatingTextManager floatingTextManager;
    public RectTransform hitpointBar;
    public Animator deathMenu;
    public GameObject hub;
    public GameObject menu;
    public GameObject settingMenu;
    
    // Logic
    public int coin;
    public int exp;

    // floating text
    public void ShowText(string msg, int fontsize, Color color, Vector3 position, Vector3 motion, float duration)
    {
        floatingTextManager.show(msg, fontsize, color, position, motion, duration);
    }

    //upgrade weapon
    public bool TryUpgradeMainWeapon()
    {
        if (mainWeaponPrices.Count <= mainWeapon.weaponLevel)
            return false;

        if(coin >= mainWeaponPrices[mainWeapon.weaponLevel])
        {
            coin -= mainWeaponPrices[mainWeapon.weaponLevel];
            mainWeapon.UpgradeMainWeapon();
            return true;
        }
        return false;
    }

    public bool TryUpgradeSubWeapon()
    {
        if (subWeaponPrices.Count <= subWeapon.weaponLevel)
            return false;

        if (coin >= subWeaponPrices[subWeapon.weaponLevel])
        {
            coin -= subWeaponPrices[subWeapon.weaponLevel];
            subWeapon.UpgradeSubWeapon();
            return true;
        }
        return false;
    }

    // hitpoint bar
    public void OnHItponitChange()
    {
        float ratio = (float)player.hitPoint / (float)player.maxHitPoint;
        hitpointBar.localScale = new Vector3(ratio, 1, 1);
    }

    // exp system
    public int GetCurrentLevel()
    {
        int r = 0;
        int add = 0;

        while ( exp >= add)
        {
            add += xpTable[r];
            r++;

            if (r == xpTable.Count)
                return r;
        }

        return r;
    }
    public int GetXptolevel(int level)
    {
        int r = 0;
        int xp = 0;
        while(r < level)
        {
            xp += xpTable[r];
            r++;
        }

        return xp;
    }
    public void GrantXp(int xp)
    {
        int currLevel = GetCurrentLevel();
        exp += xp;
        if (currLevel < GetCurrentLevel())
            OnLevelUp();
    }

    public void OnLevelUp()
    {
        Debug.Log("level up");
        player.OnLevelUp();
    }

    /*
     * INT Coin
     * INT EXP
     * INT Weapon LV
     */

    // death menu and respawn
    public void Respawn()
    {
        PlayerPrefs.DeleteAll();
        deathMenu.SetTrigger("Hide");
        UnityEngine.SceneManagement.SceneManager.LoadScene("Cave");
        player.Respawn();
    }

    // on sence loaded
    public void OnSenceLoaded(Scene s, LoadSceneMode mode)
    {
        player.transform.position = GameObject.Find("SpawnPoint").transform.position;
    }

    // Save state
    public void SaveState()
    {
        string S = "" ;

        S += coin.ToString() + "|";
        S += exp.ToString() + "|";
        S += "0";

        PlayerPrefs.SetString("SaveState", S);
        Debug.Log("SaveState");
    }

    public void LoadState(Scene s, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= LoadState;

        if (!PlayerPrefs.HasKey("SaveState"))
            return;
        string[] data = PlayerPrefs.GetString("SaveState").Split('|');

        coin = int.Parse(data[0]);


        exp = int.Parse(data[1]);
        if(GetCurrentLevel() != 1)
            player.SetLevel(GetCurrentLevel());
        // change weapon lv
        
        GameManager.instance.OnHItponitChange();

        Debug.Log("LoadState");
    }

    
}

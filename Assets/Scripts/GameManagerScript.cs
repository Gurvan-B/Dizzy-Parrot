using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    public static GameManagerScript instance;
    private bool showMenu = true;
    private List<hideMenuCallback> hideMenuCallbacks = new List<hideMenuCallback>();
    private LogicScript logic;

    private void Awake()
    {
        if (instance != null)
        {
            instance.hideMenuCallbacks = new List<hideMenuCallback>();
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
            DontDestroyOnLoad(gameObject);
        }
    }

    public void setShowMenuWithCallbacks(bool value)
    {
        setShowMenu(value);
        if (showMenu == false)
        {
            callhideMenuCallbacks();
        }
    }

    public void setShowMenu(bool value)
    {
        showMenu = value;
        logic.setIsOnMenu(showMenu);
    }

    public bool getShowMenu()
    {
        return showMenu;
    }
    public void callhideMenuCallbacks()
    {
        foreach (hideMenuCallback hideMenuCallback in hideMenuCallbacks)
        {
            hideMenuCallback();
        }
    }

    public delegate void hideMenuCallback();

    public void subscribeHideMenuCallback(hideMenuCallback hideMenuCallback)
    {
        hideMenuCallbacks.Add(hideMenuCallback);
    }
}

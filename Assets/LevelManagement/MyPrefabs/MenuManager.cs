using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

namespace LvlManagement
{
    /**/
    public class MenuManager : MonoBehaviour
    {
        public MainMenu mainMenuPrefab;
        public SettingsMenu settingsMenuPrefab;
        public CreditsMenu creditsMenuPrefab;
        public GameMenu gameMenuPrefab;
        public PauseMenu pauseMenuPrefab;
        public WinScreen winScreenPrefab;

        [SerializeField]
        private Transform _menuParent;

        private Stack<Menu> _menuStack =  new Stack<Menu>();

        private static MenuManager _instance;

        public static MenuManager Instance { get => _instance; }

        private void Awake()
        {
            if (_instance != null)
            {
                Destroy(gameObject);
            }
            else
            {
                _instance = this;
                InitializeMenus();
                Object.DontDestroyOnLoad(gameObject);
            }
        }

        private void OnDestroy()
        {
            if (_instance == this)
            {
                _instance = null;
            }
        }

        private void InitializeMenus()
        {
            if (_menuParent == null)
            {
                GameObject menuParentObject = new GameObject("Menus");
                _menuParent = menuParentObject.transform;
            }

            DontDestroyOnLoad(_menuParent.gameObject);

            //Menu[] menuPrefabs = { mainMenuPrefab, settingsMenuPrefab, creditsMenuPrefab, gameMenuPrefab, pauseMenuPrefab, winScreenPrefab };
            System.Type myType = this.GetType();

            BindingFlags myFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly;
            FieldInfo[] fields = myType.GetFields(myFlags);

            foreach (FieldInfo field in fields)
            {
                Menu prefab = field.GetValue(this) as Menu;
                if (prefab != null)
                {
                    Menu menuInstance = Instantiate(prefab, _menuParent);
                    if (prefab != mainMenuPrefab)
                    {
                        menuInstance.gameObject.SetActive(false);
                    }
                    else
                    {
                        OpenMenu(menuInstance);
                    }
                }
            }
        }

        public void OpenMenu(Menu menuInstance)
        {
            if (menuInstance == null)
            {
                Debug.LogWarning("MENU MANAGER WARNING INVALID MENU");
                return;
            }

            if (_menuStack.Count > 0)
            {
                foreach (Menu menu in _menuStack)
                {
                    menu.gameObject.SetActive(false);
                }
                    
            }

            menuInstance.gameObject.SetActive(true);
            _menuStack.Push(menuInstance);

        }

        public void CloseMenu()
        {
            if (_menuStack.Count == 0)
            {
                Debug.LogWarning("MENUMANAGER CloseMenu() error: no menus in stack");
                return;
            }

            Menu topMenu = _menuStack.Pop();
            topMenu.gameObject.SetActive(false);

            if (_menuStack.Count > 0)
            {
                Menu nextMenu = _menuStack.Peek();
                nextMenu.gameObject.SetActive(true);
            }
        }
    }

}
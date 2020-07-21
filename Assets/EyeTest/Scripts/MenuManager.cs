﻿using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

namespace EyeTest
{
    /**/
    public class MenuManager : MonoBehaviour
    {
        [SerializeField]
        private MainMenu mainMenuPrefab;
        [SerializeField]
        private InTaskMenu inTaskMenuPrefab;
        [SerializeField]
        private PauseMenu pauseMenuPrefab;
        [SerializeField]
        private TaskSettings taskSettingsPrefab;
        [SerializeField]
        private EndMenu endMenuPrefab;

        [SerializeField]
        private Transform _menuParent;

        [SerializeField]
        private Stack<Menu> _menuStack = new Stack<Menu>();

        private static MenuManager _instance;
        public static MenuManager Instance { get { return _instance;  } }

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
                DontDestroyOnLoad(gameObject);
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

            BindingFlags myFlags = BindingFlags.Instance | BindingFlags.NonPublic |
                BindingFlags.DeclaredOnly;
            FieldInfo[] fields = this.GetType().GetFields(myFlags);

            foreach (FieldInfo field in fields)
            {
                Menu prefab = field.GetValue(this as Menu);

                if (prefab != null)
                {
                    Menu menuInstance = Instantiate(prefab, _menuParent);

                    if (prefab!= mainMenuPrefab)
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
                Debug.Log("MenuManager OpenMenu() error: invalid menu");
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
                Debug.Log("MenuManager CloseMenu() error: No menus in stack!");
                return;
            }

            Menu topMenu = _menuStack.Pop();
            topMenu.gameObject.SetActive(false);

            if(_menuStack.Count > 0)
            {
                Menu nextMenu = _menuStack.Peek();
                nextMenu.gameObject.SetActive(true);
            }
        }
    }
}

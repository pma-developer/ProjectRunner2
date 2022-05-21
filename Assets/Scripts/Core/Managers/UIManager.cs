using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Core.UI.Pages;
using TMPro;
using UnityEngine;
using Zenject;

public class UIManager : MonoBehaviour, IInitializable
{
    private List<Page> _pages;
    private Stack<IPage> _history;
    
    private IPage LastOpenedPage => _history.Peek();

    public void OpenPage<T>()
    {
        var foundPage = _pages.Find(page => page is T);
        OpenPage(foundPage);
    }

    public void OpenPage(IPage page)
    {
        page.Open();
        _history.Push(page);
    }
    
    public void ClosePage<T>()
    {
        var foundPage = _pages.Find(page => page is T);
        ClosePage(foundPage);
    }

    public void ClosePage(IPage page) => page.Close();
    
    public void ReplacePage<T>()
    {
        ClosePage(LastOpenedPage);
        OpenPage<T>();
    }

    public void Initialize()
    {
        var pagesGO = transform.GetChild(0);
        var pagesComponents = pagesGO.GetComponentsInChildren<Page>();
        _pages = pagesComponents.ToList();
    }
}

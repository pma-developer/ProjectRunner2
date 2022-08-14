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
    private Stack<IPage> _history = new();

    private IPage LastOpenedPage => _history.Peek();

    public void OpenPage<T>() where T : IPage
    {
        var foundPage = _pages.Find(page => page is T);
        OpenPage(foundPage);
    }

    public void OpenPage(IPage page)
    {
        page.Open();
        _history.Push(page);
    }

    public void ClosePage<T>() where T : IPage
    {
        var foundPage = _pages.Find(page => page is T);
        ClosePage(foundPage);
    }

    public void ClosePage(IPage page)
    {
        page.Close();
        _history.Pop();
    }

    public void ReplacePage<T>() where T : IPage
    {
        ClosePage(LastOpenedPage);
        OpenPage<T>();
    }

    public void Initialize()
    {
        var pagesGameObject = transform.GetChild(0);
        var pagesComponents = pagesGameObject.GetComponentsInChildren<Page>();
        _pages = pagesComponents.ToList();

        foreach (var page in _pages)
        {
            page.gameObject.SetActive(false);
        }
    }
}
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace ComboboxTransitionIssueTest.ViewModels;

public class MainViewModel : ViewModelBase
{
    public ICommand TransitionfromViewModel { get; }
    [Reactive]
    public ViewModelBase CurrentPage { get; set; }

    /// <summary>
    /// ComboBoxの表示アイテムと、各ViewModelを一元管理する。
    /// </summary>
    public class PageInfo
    {
        public string Name { get; init; } = string.Empty;
        public ViewModelBase ViewModel { get; init; } = new ViewModelBase();
    }

    public static List<PageInfo> PageNames => new()
    {
        new PageInfo{ Name="111", ViewModel= new OneViewModel() },
        new PageInfo{ Name="222", ViewModel= new TwoViewModel() },
    };

    /// <summary>
    /// ComboBoxのSelectedIndexにバインディングして表示したい画面を指定する。
    /// </summary>
    private int _pageIndex;
    public int PageIndex
    {
        get => _pageIndex;
        set
        {
            _pageIndex = value;
            CurrentPage = PageNames[value].ViewModel;
        }
    }

    public MainViewModel()
    {
        CurrentPage = PageNames[0].ViewModel;
        TransitionfromViewModel = ReactiveCommand.Create(() =>
            // CurrentPage = PageIndex == 0 ?
            //     PageNames[1].ViewModel : PageNames[0].ViewModel
            PageIndex = PageIndex == 0 ? 1 : 0
        );
    }
}

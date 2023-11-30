using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Labb3ProgTemplate.DataModels.Products;
using Labb3ProgTemplate.Managerrs;

namespace Labb3ProgTemplate.DataContext;

public class AdminWindowContext : INotifyPropertyChanged
{
    private string _prodName;

    public string ProdName
    {
        get { return _prodName; }
        set
        {
            _prodName = value;
            OnPropertyChanged();
        }
    }

    private string _prodPrice;

    public string ProdPrice
    {
        get { return _prodPrice; }
        set
        {
            _prodPrice = value;
            OnPropertyChanged();
        }
    }

    private bool _isToy;

    public bool IsToy
    {
        get { return _isToy; }
        set
        {
            _isToy = value;
            OnPropertyChanged();
        }
    }

    private bool _isFood;

    public bool IsFood
    {
        get { return _isFood; }
        set
        {
            _isFood = value;
            OnPropertyChanged();
        }
    }

    private bool _isInStorage;

    public bool IsInStorage
    {
        get { return _isInStorage; }
        set
        {
            _isInStorage = value;
            OnPropertyChanged();
        }
    }

    private bool _isInShop;

    public bool IsInShop
    {
        get { return _isInShop; }
        set
        {
            _isInShop = value;
            OnPropertyChanged();
        }
    }



    public ObservableCollection<BaseProduct> ProductList { get; set; } = new();

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}
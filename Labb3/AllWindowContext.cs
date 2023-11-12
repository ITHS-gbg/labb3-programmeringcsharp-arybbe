using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Labb3ProgTemplate.DataModels.Products;
using Labb3ProgTemplate.Managerrs;

namespace Labb3ProgTemplate;

public class AllWindowContext : INotifyPropertyChanged
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



    public ObservableCollection<Product> Products { get; set; } = new();

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
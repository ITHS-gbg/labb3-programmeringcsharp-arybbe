using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Labb3ProgTemplate.DataModels.Products;

namespace Labb3ProgTemplate.DataContext;

public class StoreWindowContext : INotifyPropertyChanged
{
    private int myVar;

    public int MyProperty
    {
        get { return myVar; }
        set { myVar = value; }
    }



    public ObservableCollection<Product> CartProducts { get; set; } = new();
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
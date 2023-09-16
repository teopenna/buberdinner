using BuberDinner.Domain.Common.Models;
using BuberDinner.Domain.Menu.ValueObjects;

namespace BuberDinner.Domain.Menu.Entities;

public sealed class MenuSection : Entity<MenuSectionId>
{
    private readonly List<MenuItem> _menuItems = new();
    
    public string Name { get; }
    public string Description { get; }

    public IReadOnlyList<MenuItem> Items => _menuItems.AsReadOnly();
    
    private MenuSection(MenuSectionId id, string name, string description, List<MenuItem>? menuItems) : base(id)
    {
        Name = name;
        Description = description;
        _menuItems = menuItems ?? new List<MenuItem>();
    }

    public static MenuSection Create(string name, string description, List<MenuItem>? menuItems)
    {
        return new(MenuSectionId.CreateUnique(), name, description, menuItems);
    }
}
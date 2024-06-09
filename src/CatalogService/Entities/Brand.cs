﻿namespace CatalogService.Entities;

public class Brand
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public ICollection<Model> Models { get; set; }
}

﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace DemoCoreMVC.DAL;

public partial class ProductModel
{
    public int ProductModelID { get; set; }

    public string Name { get; set; }

    public string CatalogDescription { get; set; }

    public Guid rowguid { get; set; }

    public DateTime ModifiedDate { get; set; }

    public virtual ICollection<Product> Product { get; set; } = new List<Product>();

    public virtual ICollection<ProductModelProductDescription> ProductModelProductDescription { get; set; } = new List<ProductModelProductDescription>();
}
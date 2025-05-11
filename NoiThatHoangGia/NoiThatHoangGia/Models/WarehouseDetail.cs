using System;
using System.Collections.Generic;

namespace NoiThatHoangGia.Models;

public partial class WarehouseDetail
{
    public int Id { get; set; }

    public int? WarehouseId { get; set; }

    public int? ProductId { get; set; }

    public long? Quantity { get; set; }

    public int? MinQuantity { get; set; }

    public virtual Product? Product { get; set; }

    public virtual Warehouse? Warehouse { get; set; }
}

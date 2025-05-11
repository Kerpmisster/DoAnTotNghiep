using System;
using System.Collections.Generic;

namespace NoiThatHoangGia.Models;

public partial class ExportReceipt
{
    public int Id { get; set; }

    public string? Code { get; set; }

    public DateTime? Date { get; set; }

    public int? WarehouseId { get; set; }

    public string? UserId { get; set; }

    public decimal? Total { get; set; }

    public string? CreatedBy { get; set; }

    public virtual ICollection<ExportReceiptDetail> ExportReceiptDetails { get; set; } = new List<ExportReceiptDetail>();

    public virtual Warehouse? Warehouse { get; set; }
}

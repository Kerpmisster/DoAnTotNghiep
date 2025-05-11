using System;
using System.Collections.Generic;

namespace NoiThatHoangGia.Models;

public partial class ProductExtension
{
    public int Id { get; set; }

    public int? Pid { get; set; }

    public int? Eid { get; set; }

    public virtual Extension? EidNavigation { get; set; }

    public virtual Product? PidNavigation { get; set; }
}

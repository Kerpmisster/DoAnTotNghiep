using System;
using System.Collections.Generic;

namespace NoiThatHoangGia.Models;

public partial class Menu
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public string? Slug { get; set; }

    public int? Position { get; set; }

    public byte? Isactive { get; set; }
}

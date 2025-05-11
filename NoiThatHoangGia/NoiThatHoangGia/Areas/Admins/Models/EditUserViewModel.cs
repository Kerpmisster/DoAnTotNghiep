using Microsoft.AspNetCore.Mvc.Rendering;

namespace NoiThatHoangGia.Areas.Admins.Models
{
    public class EditUserViewModel
    {
        public string Id { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public List<SelectListItem> AvailableRoles { get; set; } = new();
        public List<string> SelectedRoles { get; set; } = new();
    }
}

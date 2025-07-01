using System;
using System.ComponentModel.DataAnnotations;

namespace Switchcraft.Server.Controllers.Dtos;

public class ApplicationRequest
{
    [Required]
    public string Name { get; set; } = string.Empty;
}

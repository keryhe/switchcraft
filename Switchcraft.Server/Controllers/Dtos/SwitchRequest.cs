using System;
using System.ComponentModel.DataAnnotations;

namespace Switchcraft.Server.Controllers.Dtos;

public class SwitchRequest
{
    [Required]
    public string Name { get; set; } = String.Empty;

    [Required]
    public int ApplicationId { get; set; } = 0;
}

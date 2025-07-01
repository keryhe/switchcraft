using System;
using System.ComponentModel.DataAnnotations;

namespace Switchcraft.Server.Controllers.Dtos;

public class SwitchInstanceRequest
{
    [Required]
    public bool IsEnabled { get; set; }
}

﻿using System;
using System.Collections.Generic;

namespace OnBoardingSystem.Data.EF.Models;

public partial class MdEmpStatus
{
    public string Id { get; set; } = null!;

    public string? Status { get; set; }
}

﻿using System;
using System.Collections.Generic;

namespace DemoExamText.Models;

public partial class Country
{
    public string Code { get; set; } = null!;

    public string Country1 { get; set; } = null!;

    public virtual ICollection<Hotel> Hotels { get; set; } = new List<Hotel>();
}

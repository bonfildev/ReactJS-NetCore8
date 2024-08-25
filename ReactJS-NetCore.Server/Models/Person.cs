﻿using System;
using System.Collections.Generic;

namespace ReactJS_NetCore.Server.Models;

public partial class Person
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int Age { get; set; }

    public string Email { get; set; } = null!;
}

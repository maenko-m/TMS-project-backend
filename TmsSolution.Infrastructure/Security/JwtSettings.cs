﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TmsSolution.Infrastructure.Security
{
    public class JwtSettings
    {
        public string Secret { get; set; } = string.Empty;
        public string Issuer { get; set; } = string.Empty;
        public string Audience { get; set; } = string.Empty;
        public int TokenLifetimeMinutes { get; set; } = 1440; // 1 day default
    }
}

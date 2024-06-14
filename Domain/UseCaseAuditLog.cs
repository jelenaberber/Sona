﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class UseCaseAuditLog
    {
        public int Id { get; set; }
        public string UseCaseName { get; set; }
        public string Username { get; set; }
        public string UseCaseData { get; set; }
        public DateTime ExecutedAt { get; set; }
    }
}

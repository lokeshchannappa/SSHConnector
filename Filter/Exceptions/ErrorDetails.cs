﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SSHConnector.Filter.Exceptions
{
    public class ErrorDetails
    {
        public string CorrelationId { get; set; }
        public string OccurredAt { get; set; }
        public string Message { get; set; }
        public string Service { get; set; }


        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}

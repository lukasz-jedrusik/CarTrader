using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CarTrader.Services.Workflow.Domain.Models
{
    public class CarProcess
    {
        public Guid CarId { get; set; }
        public string CamundaProcessId { get; set; }
    }
}
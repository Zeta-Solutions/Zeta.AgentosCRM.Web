using System;
using System.Collections.Generic;
using System.Text;

namespace Zeta.AgentosCRM.TaskManagement.Dtos
{
    public class UpdateTaskIsCompletedDto
    {
        public long? TaskId { get; set; }
        public bool IsCompleted { get; set; }
    }
}

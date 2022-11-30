using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Model
{
    public class Infra_ResultDto
    {
        public Infra_ResultDto()
        {

        }

        public Infra_ResultDto(object data)
        {
            IsSuccess = true;
            Data = data;
        }

        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
    }
}

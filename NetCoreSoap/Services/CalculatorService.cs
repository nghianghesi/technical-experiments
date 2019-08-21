using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorService
{
    public class SimpleCalculatorService : ISimpleCalculator
    {
        public Task<GetDataResponse> Add(int num1, int num2)
        {
            return Task.Run(() => new GetDataResponse() { Result = num1 + num2 });
        }
    }
}

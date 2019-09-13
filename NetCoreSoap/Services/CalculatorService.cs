using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CalculatorService
{
    public class SimpleCalculatorService : ISimpleCalculator
    {
        public Task<AddDataResponse> Add(AddDataRequest request)
        {
            return Task.Run(() => new AddDataResponse()
            {
                Result = new InternalResult()
                {
                    Result = request.num1 + request.num2,
                    Operants = new List<Operant>() {
                        new Operant(){ Value = request.num1 },
                        new Operant(){ Value = request.num2 }
                    },
                    InternalOperants = new List<InternalResult>() {
                        new InternalResult(){ Result = request.num1 },
                        new InternalResult(){ Result = request.num2 }
                    }
                }
            });
        }
    }
}

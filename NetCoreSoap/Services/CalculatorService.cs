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
        public Task<AddDataResponse> Add([XmlElement(ElementName = "Request")] AddDataRequest request)
        {
            return Task.Run(() => new AddDataResponse() { Result = request.num1 + request.num2 });
        }
    }
}

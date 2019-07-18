using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorService
{
    [ServiceContract(Namespace = "http://www.test2.org/test2/types")]
    public interface ISimpleCalculator
    {
        [OperationContract()]
        Task<GetDataResponse> Add(int num1, int num2);
    }


    [DataContract(Namespace = "http://www.test2.org/test2/types")]
    public class GetDataResponse
    {
        [DataMember(Name = "FinalResult")]
        public int Result { get; set; }
    }
}

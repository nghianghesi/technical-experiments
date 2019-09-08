using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CalculatorService
{
    [ServiceContract( Namespace = "http://www.test2.org/test2/types", Name = "ApuService")]
    public interface ISimpleCalculator
    {
        [OperationContract(Name = "ApuAddV3", AsyncPattern = true)]
        Task<AddDataResponse> Add([XmlElement(ElementName = "RequestParam")] AddDataRequest request);
    }


    [DataContract(Namespace = "http://www.test2.org/test2/types", Name = "ApuResponse")]
    public class AddDataResponse
    {
        [DataMember(Name = "FinalResult")]
        public int Result { get; set; }
    }


    [DataContract(Namespace = "http://www.test2.org/test2/types", Name = "ApuRequest")]
    public class AddDataRequest
    {
        [DataMember(Name = "n1")]
        public int num1 { get; set; }
        [DataMember(Name = "n2")]
        public int num2 { get; set; }
    }
}

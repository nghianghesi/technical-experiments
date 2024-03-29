﻿using System;
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
        [OperationContract(Name = "ApuAddV3", Action = "ApuAddV3")]
        Task<AddDataResponse> Add([XmlElement("ApuRequest")] AddDataRequest request);
    }

    [DataContract(Namespace = "http://www.test2.org/test2/types", Name = "InternalResult")]
    [Serializable]
    public class Operant
    {
        [DataMember]
        public int Value { get; set; }
    }

    [DataContract(Namespace = "http://www.test2.org/test2/types", Name = "InternalResult")]
    [Serializable]
    public class InternalResult
    {
        [DataMember]
        public int Result { get; set; }
        [DataMember]
        public List<Operant> Operants { get; set; }
        [DataMember]
        public List<InternalResult> InternalOperants { get; set; }
    }

    [DataContract(Namespace = "http://www.test2.org/test2/types", Name = "ApuResponse")]
    [Serializable]
    public class AddDataResponse
    {
        [DataMember(Name = "FinalResult")]
        public InternalResult Result { get; set; }
    }


    [DataContract(Namespace = "http://www.test2.org/test2/types", Name = "ApuRequest")]
    [MessageContract()]
    [Serializable]
    public class AddDataRequest
    {
        [MessageBodyMember(Name = "n1")]
        [DataMember(Name = "n1")]
        public int num1 { get; set; }
        [MessageBodyMember(Name = "n2")]
        [DataMember(Name = "n2")]
        public int num2 { get; set; }
    }
}

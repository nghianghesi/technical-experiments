using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace AsynWebmethod
{
    /// <summary>
    /// Summary description for SyncWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class SyncWebService : System.Web.Services.WebService
    {

        public delegate string LengthyProcedureAsyncStub(
       int milliseconds);

        public string LengthyProcedure(int milliseconds)
        {
            System.Threading.Thread.Sleep(milliseconds);
            return "Success";
        }

        public class MyState
        {
            public object previousState;
            public LengthyProcedureAsyncStub asyncStub;
        }

        [System.Web.Services.WebMethod]
        public IAsyncResult BeginLengthyProcedure(int milliseconds,
            AsyncCallback cb, object s)
        {
            LengthyProcedureAsyncStub stub
                = new LengthyProcedureAsyncStub(LengthyProcedure);
            MyState ms = new MyState();
            ms.previousState = s;
            ms.asyncStub = stub;
            return stub.BeginInvoke(milliseconds, cb, ms);
        }

        [System.Web.Services.WebMethod]
        public string EndLengthyProcedure(IAsyncResult call)
        {
            MyState ms = (MyState)call.AsyncState;
            return ms.asyncStub.EndInvoke(call);
        }
    }
}

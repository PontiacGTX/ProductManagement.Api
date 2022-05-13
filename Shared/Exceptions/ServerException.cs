using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Exceptions
{
    public class ServerException:Exception
    {
        public int ApiCode  { get; set; }
        public ServerException(string errorMessage, int apiCode ) :base(errorMessage)
        {
            ApiCode = apiCode;
        }
    }
}

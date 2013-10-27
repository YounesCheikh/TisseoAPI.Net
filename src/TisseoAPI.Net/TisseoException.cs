using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TisseoAPI.Net
{
    public class TisseoException: Exception
    {
        public int ErrorId { get; private set; }

        public TisseoException(string message)
            : base(message)
        {
            ErrorId = 0;
        }

        public TisseoException(int errorId, string message)
            : base(message)
        {
            ErrorId = errorId;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace ToolManager
{
    public class Responses
    {
        public Responses(bool isSuccess = false, bool isFailed = false)
        {
            IsSuccess = isSuccess;
            IsFailed = isFailed;
        }

        public static Responses Success { get 
            {
                return new Responses(true, false);
            }
        }
        public static Responses Failed { get
            {
                return new Responses(false, true);
            }
        }

        public bool IsSuccess { get; protected set; }
        public bool IsFailed { get; protected set; }
    }
}

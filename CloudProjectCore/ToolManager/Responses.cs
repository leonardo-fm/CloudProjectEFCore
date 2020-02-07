using System;
using System.Collections.Generic;
using System.Text;

namespace ToolManager
{
    public class Responses
    {
        public Responses(bool isSuccess = false, bool isFailed = false, bool isNotAllowed = false)
        {
            IsSuccess = isSuccess;
            IsFailed = isFailed;
            IsNotAllowed = isNotAllowed;
        }

        public static Responses Success { get 
            {
                return new Responses(true, false, false);
            }
        }
        public static Responses Failed { get
            {
                return new Responses(false, true, false);
            }
        }
        public static Responses NotAllowed { get
            {
                return new Responses(false, false, true);
            }
        }

        public bool IsSuccess { get; protected set; }
        public bool IsFailed { get; protected set; }
        public bool IsNotAllowed { get; protected set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RyblikGUI.Exceptions
{
    class ExceptionFuzzing : Exception
    {
        private String errorLine = null;
        private String errorMsg = null;

        public ExceptionFuzzing(String errorMsgIn)
        {
            errorMsg = errorMsgIn;
        }

        public ExceptionFuzzing(String errorMsgIn, String errorLineIn)
        {
            errorMsg = errorMsgIn;
            errorLine = errorLineIn;
        }

        public String getErrorLine()
        {
            return errorLine;
        }

        public void setErrorLine(String errorLineIn)
        {
            errorLine = errorLineIn;
        }

        public String getErrorMsg()
        {
            return errorMsg;
        }

        public void setErrorMsg(String errorMsgIn)
        {
            errorMsg = errorMsgIn;
        }
    }
}

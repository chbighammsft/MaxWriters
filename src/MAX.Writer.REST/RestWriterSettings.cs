using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Max.Writer.Rest
{
    public class RestWriterSettings
    {
        public string ServiceURLFormat { get { return "https://outlook.office365.com/api/v1.0/{0}"; } }
    }
}
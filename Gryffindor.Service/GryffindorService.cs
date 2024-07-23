using Gryffindor.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using log4net;

namespace Gryffindor.Service
{
    [ServiceBehavior]
    public partial class GryffindorService : IGryffindorService
    {
        private ILog _log = LogManager.GetLogger(typeof(GryffindorService));
    }
}

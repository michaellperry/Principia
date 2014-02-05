using Principia.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Principia.Sharing.ViewModels
{
    public class RequestViewModel
    {
        private readonly Request _request;

        public RequestViewModel(Request request)
        {
            _request = request;
        }

        internal Request Request
        {
            get { return _request; }
        }

        public string IndividualName
        {
            get { return _request.Profile.Name.Value ?? "<Unidentified>"; }
        }
    }
}

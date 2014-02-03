using Principia.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Principia.Sharing.Models;
using UpdateControls.Correspondence;

namespace Principia.Sharing.ViewModels
{
    public class SendViewModel
    {
        private readonly ICommunity _community;
        private readonly ShareModel _shareModel;
        private readonly Individual _individual;
        
        public SendViewModel(
            ICommunity community,
            ShareModel shareModel,
            Individual individual)
        {
            _community = community;
            _shareModel = shareModel;
            _individual = individual;
        }

        public string LastException
        {
            get
            {
                if (_community.LastException == null)
                    return null;

                return _community.LastException.Message;
            }
        }

        public bool Synchronizing
        {
            get { return _community.Synchronizing; }
        }

        public Uri Uri
        {
            get { return new Uri(String.Format("principia://{0}", _shareModel.Token.Identifier)); }
        }

        public IEnumerable<RequestViewModel> Requests
        {
            get
            {
                return
                    from request in _shareModel.Token.Requests
                    select new RequestViewModel(request);
            }
        }
    }
}

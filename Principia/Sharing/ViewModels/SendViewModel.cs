using Principia.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Principia.Sharing.Models;

namespace Principia.Sharing.ViewModels
{
    public class SendViewModel
    {
        private readonly ShareModel _shareModel;
        private readonly Individual _individual;

        public SendViewModel(ShareModel shareModel, Individual individual)
        {
            _shareModel = shareModel;
            _individual = individual;
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

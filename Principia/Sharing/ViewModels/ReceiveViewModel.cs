using Principia.Sharing.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpdateControls.Fields;
using Principia.Model;
using UpdateControls.Correspondence;

namespace Principia.Sharing.ViewModels
{
    public class ReceiveViewModel
    {
        private readonly ICommunity _community;
        private readonly ShareModel _shareModel;
        private readonly Individual _individual;

        private Binding _requestBinding;
        
        public ReceiveViewModel(
            ICommunity community,
            ShareModel shareModel,
            Individual individual)
        {
            _community = community;
            _shareModel = shareModel;
            _individual = individual;

            _requestBinding = Binding.Bind(() => _shareModel.Token,
                delegate(Token token)
                {
                    if (!token.IsNull && !_individual.IsNull)
                    {
                        _shareModel.Community.Perform(async delegate
                        {
                            _shareModel.Request = await token.NewRequest(_individual);
                        });
                    }
                });
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

        public void SetUri(Uri value)
        {
            _shareModel.Community.Perform(async delegate
            {
                await _shareModel.CreateTokenAsync(value);
            });
        }

        public string TokenIdentifier
        {
            get { return _shareModel.Token.Identifier; }
        }

        public string Name
        {
            get { return _individual.Profiles.Select(p => p.Name.Value).FirstOrDefault(); }
            set
            {
                _individual.Community.Perform(async delegate
                {
                    var profile = await _individual.NewProfile();
                    profile.Name = value;
                });
            }
        }

        public IEnumerable<CourseViewModel> Courses
        {
            get
            {
                return
                    from course in _shareModel.Request.Courses
                    select new CourseViewModel(course);
            }
        }
    }
}

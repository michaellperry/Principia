using Principia.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UpdateControls.Correspondence;
using UpdateControls.Fields;

namespace Principia.Sharing.Models
{
    public class ShareModel
    {
        private static readonly Regex Punctuation = new Regex(@"[{}\-]");

        private readonly ICommunity _community;

        private Independent<Token> _token = new Independent<Token>(
            Token.GetNullInstance());
        private Independent<Course> _course = new Independent<Course>(
            Course.GetNullInstance());
        private Independent<Request> _request = new Independent<Request>(
            Request.GetNullInstance());
        
        public ShareModel(ICommunity community)
        {
            _community = community;
        }

        public ICommunity Community
        {
            get { return _community; }
        }

        public Token Token
        {
            get
            {
                lock (this)
                {
                    return _token;
                }
            }
            private set
            {
                lock (this)
                {
                    _token.Value = value;
                }
            }
        }

        public Course Course
        {
            get
            {
                lock (this)
                {
                    return _course;
                }
            }
            set
            {
                lock (this)
                {
                    _course.Value = value;
                }
            }
        }

        public Request Request
        {
            get
            {
                lock (this)
                {
                    return _request;
                }
            }
            set
            {
                lock (this)
                {
                    _request.Value = value;
                }
            }
        }

        public async Task CreateNewTokenAsync()
        {
            string identifier = Punctuation
                .Replace(Guid.NewGuid().ToString(), String.Empty)
                .ToLower();
            Token = await _community.AddFactAsync(new Token(identifier));
        }

        public async Task CreateTokenAsync(Uri uri)
        {
            Token = await _community.AddFactAsync(new Token(uri.Host));
        }
    }
}

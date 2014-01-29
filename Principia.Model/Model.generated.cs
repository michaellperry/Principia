﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UpdateControls.Correspondence;
using UpdateControls.Correspondence.Mementos;
using UpdateControls.Correspondence.Strategy;
using System;
using System.IO;

/**
/ For use with http://graphviz.org/
digraph "Principia.Model"
{
    rankdir=BT
    Request -> Individual [color="red"]
    Request -> Token [color="red"]
    Grant -> Request
    Grant -> Course
    Accept -> Grant
    AcceptDelete -> Accept
    Course__title -> Course
    Course__title -> Course__title [label="  *"]
    Course__shortDescription -> Course
    Course__shortDescription -> Course__shortDescription [label="  *"]
    Course__description -> Course
    Course__description -> Course__description [label="  *"]
    Module -> Course [color="red"]
    Module__ordinal -> Module
    Module__ordinal -> Module__ordinal [label="  *"]
    Module__title -> Module
    Module__title -> Module__title [label="  *"]
    Clip -> Course [color="red"]
    Clip__ordinal -> Clip
    Clip__ordinal -> Clip__ordinal [label="  *"]
    Clip__title -> Clip
    Clip__title -> Clip__title [label="  *"]
    ClipModule -> Clip
    ClipModule -> Module
    ClipModule -> ClipModule [label="  *"]
}
**/

namespace Principia.Model
{
    public partial class Individual : CorrespondenceFact
    {
		// Factory
		internal class CorrespondenceFactFactory : ICorrespondenceFactFactory
		{
			private IDictionary<Type, IFieldSerializer> _fieldSerializerByType;

			public CorrespondenceFactFactory(IDictionary<Type, IFieldSerializer> fieldSerializerByType)
			{
				_fieldSerializerByType = fieldSerializerByType;
			}

			public CorrespondenceFact CreateFact(FactMemento memento)
			{
				Individual newFact = new Individual(memento);

				// Create a memory stream from the memento data.
				using (MemoryStream data = new MemoryStream(memento.Data))
				{
					using (BinaryReader output = new BinaryReader(data))
					{
						newFact._anonymousId = (string)_fieldSerializerByType[typeof(string)].ReadData(output);
					}
				}

				return newFact;
			}

			public void WriteFactData(CorrespondenceFact obj, BinaryWriter output)
			{
				Individual fact = (Individual)obj;
				_fieldSerializerByType[typeof(string)].WriteData(output, fact._anonymousId);
			}

            public CorrespondenceFact GetUnloadedInstance()
            {
                return Individual.GetUnloadedInstance();
            }

            public CorrespondenceFact GetNullInstance()
            {
                return Individual.GetNullInstance();
            }
		}

		// Type
		internal static CorrespondenceFactType _correspondenceFactType = new CorrespondenceFactType(
			"Principia.Model.Individual", 8);

		protected override CorrespondenceFactType GetCorrespondenceFactType()
		{
			return _correspondenceFactType;
		}

        // Null and unloaded instances
        public static Individual GetUnloadedInstance()
        {
            return new Individual((FactMemento)null) { IsLoaded = false };
        }

        public static Individual GetNullInstance()
        {
            return new Individual((FactMemento)null) { IsNull = true };
        }

        // Ensure
        public Task<Individual> EnsureAsync()
        {
            if (_loadedTask != null)
                return _loadedTask.ContinueWith(t => (Individual)t.Result);
            else
                return Task.FromResult(this);
        }

        // Roles

        // Queries
        private static Query _cacheQueryCoursesAccepted;

        public static Query GetQueryCoursesAccepted()
		{
            if (_cacheQueryCoursesAccepted == null)
            {
			    _cacheQueryCoursesAccepted = new Query()
		    		.JoinSuccessors(Request.GetRoleIndividual())
		    		.JoinSuccessors(Grant.GetRoleRequest())
    				.JoinSuccessors(Accept.GetRoleGrant(), Condition.WhereIsEmpty(Accept.GetQueryIsDeleted())
				)
                ;
            }
            return _cacheQueryCoursesAccepted;
		}

        // Predicates

        // Predecessors

        // Fields
        private string _anonymousId;

        // Results
        private Result<Accept> _coursesAccepted;

        // Business constructor
        public Individual(
            string anonymousId
            )
        {
            InitializeResults();
            _anonymousId = anonymousId;
        }

        // Hydration constructor
        private Individual(FactMemento memento)
        {
            InitializeResults();
        }

        // Result initializer
        private void InitializeResults()
        {
            _coursesAccepted = new Result<Accept>(this, GetQueryCoursesAccepted(), Accept.GetUnloadedInstance, Accept.GetNullInstance);
        }

        // Predecessor access

        // Field access
        public string AnonymousId
        {
            get { return _anonymousId; }
        }

        // Query result access
        public Result<Accept> CoursesAccepted
        {
            get { return _coursesAccepted; }
        }

        // Mutable property access

    }
    
    public partial class Token : CorrespondenceFact
    {
		// Factory
		internal class CorrespondenceFactFactory : ICorrespondenceFactFactory
		{
			private IDictionary<Type, IFieldSerializer> _fieldSerializerByType;

			public CorrespondenceFactFactory(IDictionary<Type, IFieldSerializer> fieldSerializerByType)
			{
				_fieldSerializerByType = fieldSerializerByType;
			}

			public CorrespondenceFact CreateFact(FactMemento memento)
			{
				Token newFact = new Token(memento);

				// Create a memory stream from the memento data.
				using (MemoryStream data = new MemoryStream(memento.Data))
				{
					using (BinaryReader output = new BinaryReader(data))
					{
						newFact._identifier = (string)_fieldSerializerByType[typeof(string)].ReadData(output);
					}
				}

				return newFact;
			}

			public void WriteFactData(CorrespondenceFact obj, BinaryWriter output)
			{
				Token fact = (Token)obj;
				_fieldSerializerByType[typeof(string)].WriteData(output, fact._identifier);
			}

            public CorrespondenceFact GetUnloadedInstance()
            {
                return Token.GetUnloadedInstance();
            }

            public CorrespondenceFact GetNullInstance()
            {
                return Token.GetNullInstance();
            }
		}

		// Type
		internal static CorrespondenceFactType _correspondenceFactType = new CorrespondenceFactType(
			"Principia.Model.Token", 8);

		protected override CorrespondenceFactType GetCorrespondenceFactType()
		{
			return _correspondenceFactType;
		}

        // Null and unloaded instances
        public static Token GetUnloadedInstance()
        {
            return new Token((FactMemento)null) { IsLoaded = false };
        }

        public static Token GetNullInstance()
        {
            return new Token((FactMemento)null) { IsNull = true };
        }

        // Ensure
        public Task<Token> EnsureAsync()
        {
            if (_loadedTask != null)
                return _loadedTask.ContinueWith(t => (Token)t.Result);
            else
                return Task.FromResult(this);
        }

        // Roles

        // Queries

        // Predicates

        // Predecessors

        // Fields
        private string _identifier;

        // Results

        // Business constructor
        public Token(
            string identifier
            )
        {
            InitializeResults();
            _identifier = identifier;
        }

        // Hydration constructor
        private Token(FactMemento memento)
        {
            InitializeResults();
        }

        // Result initializer
        private void InitializeResults()
        {
        }

        // Predecessor access

        // Field access
        public string Identifier
        {
            get { return _identifier; }
        }

        // Query result access

        // Mutable property access

    }
    
    public partial class Request : CorrespondenceFact
    {
		// Factory
		internal class CorrespondenceFactFactory : ICorrespondenceFactFactory
		{
			private IDictionary<Type, IFieldSerializer> _fieldSerializerByType;

			public CorrespondenceFactFactory(IDictionary<Type, IFieldSerializer> fieldSerializerByType)
			{
				_fieldSerializerByType = fieldSerializerByType;
			}

			public CorrespondenceFact CreateFact(FactMemento memento)
			{
				Request newFact = new Request(memento);


				return newFact;
			}

			public void WriteFactData(CorrespondenceFact obj, BinaryWriter output)
			{
				Request fact = (Request)obj;
			}

            public CorrespondenceFact GetUnloadedInstance()
            {
                return Request.GetUnloadedInstance();
            }

            public CorrespondenceFact GetNullInstance()
            {
                return Request.GetNullInstance();
            }
		}

		// Type
		internal static CorrespondenceFactType _correspondenceFactType = new CorrespondenceFactType(
			"Principia.Model.Request", 914985032);

		protected override CorrespondenceFactType GetCorrespondenceFactType()
		{
			return _correspondenceFactType;
		}

        // Null and unloaded instances
        public static Request GetUnloadedInstance()
        {
            return new Request((FactMemento)null) { IsLoaded = false };
        }

        public static Request GetNullInstance()
        {
            return new Request((FactMemento)null) { IsNull = true };
        }

        // Ensure
        public Task<Request> EnsureAsync()
        {
            if (_loadedTask != null)
                return _loadedTask.ContinueWith(t => (Request)t.Result);
            else
                return Task.FromResult(this);
        }

        // Roles
        private static Role _cacheRoleIndividual;
        public static Role GetRoleIndividual()
        {
            if (_cacheRoleIndividual == null)
            {
                _cacheRoleIndividual = new Role(new RoleMemento(
			        _correspondenceFactType,
			        "individual",
			        Individual._correspondenceFactType,
			        true));
            }
            return _cacheRoleIndividual;
        }
        private static Role _cacheRoleToken;
        public static Role GetRoleToken()
        {
            if (_cacheRoleToken == null)
            {
                _cacheRoleToken = new Role(new RoleMemento(
			        _correspondenceFactType,
			        "token",
			        Token._correspondenceFactType,
			        true));
            }
            return _cacheRoleToken;
        }

        // Queries

        // Predicates

        // Predecessors
        private PredecessorObj<Individual> _individual;
        private PredecessorObj<Token> _token;

        // Fields

        // Results

        // Business constructor
        public Request(
            Individual individual
            ,Token token
            )
        {
            InitializeResults();
            _individual = new PredecessorObj<Individual>(this, GetRoleIndividual(), individual);
            _token = new PredecessorObj<Token>(this, GetRoleToken(), token);
        }

        // Hydration constructor
        private Request(FactMemento memento)
        {
            InitializeResults();
            _individual = new PredecessorObj<Individual>(this, GetRoleIndividual(), memento, Individual.GetUnloadedInstance, Individual.GetNullInstance);
            _token = new PredecessorObj<Token>(this, GetRoleToken(), memento, Token.GetUnloadedInstance, Token.GetNullInstance);
        }

        // Result initializer
        private void InitializeResults()
        {
        }

        // Predecessor access
        public Individual Individual
        {
            get { return IsNull ? Individual.GetNullInstance() : _individual.Fact; }
        }
        public Token Token
        {
            get { return IsNull ? Token.GetNullInstance() : _token.Fact; }
        }

        // Field access

        // Query result access

        // Mutable property access

    }
    
    public partial class Grant : CorrespondenceFact
    {
		// Factory
		internal class CorrespondenceFactFactory : ICorrespondenceFactFactory
		{
			private IDictionary<Type, IFieldSerializer> _fieldSerializerByType;

			public CorrespondenceFactFactory(IDictionary<Type, IFieldSerializer> fieldSerializerByType)
			{
				_fieldSerializerByType = fieldSerializerByType;
			}

			public CorrespondenceFact CreateFact(FactMemento memento)
			{
				Grant newFact = new Grant(memento);


				return newFact;
			}

			public void WriteFactData(CorrespondenceFact obj, BinaryWriter output)
			{
				Grant fact = (Grant)obj;
			}

            public CorrespondenceFact GetUnloadedInstance()
            {
                return Grant.GetUnloadedInstance();
            }

            public CorrespondenceFact GetNullInstance()
            {
                return Grant.GetNullInstance();
            }
		}

		// Type
		internal static CorrespondenceFactType _correspondenceFactType = new CorrespondenceFactType(
			"Principia.Model.Grant", -728454336);

		protected override CorrespondenceFactType GetCorrespondenceFactType()
		{
			return _correspondenceFactType;
		}

        // Null and unloaded instances
        public static Grant GetUnloadedInstance()
        {
            return new Grant((FactMemento)null) { IsLoaded = false };
        }

        public static Grant GetNullInstance()
        {
            return new Grant((FactMemento)null) { IsNull = true };
        }

        // Ensure
        public Task<Grant> EnsureAsync()
        {
            if (_loadedTask != null)
                return _loadedTask.ContinueWith(t => (Grant)t.Result);
            else
                return Task.FromResult(this);
        }

        // Roles
        private static Role _cacheRoleRequest;
        public static Role GetRoleRequest()
        {
            if (_cacheRoleRequest == null)
            {
                _cacheRoleRequest = new Role(new RoleMemento(
			        _correspondenceFactType,
			        "request",
			        Request._correspondenceFactType,
			        false));
            }
            return _cacheRoleRequest;
        }
        private static Role _cacheRoleCourse;
        public static Role GetRoleCourse()
        {
            if (_cacheRoleCourse == null)
            {
                _cacheRoleCourse = new Role(new RoleMemento(
			        _correspondenceFactType,
			        "course",
			        Course._correspondenceFactType,
			        false));
            }
            return _cacheRoleCourse;
        }

        // Queries

        // Predicates

        // Predecessors
        private PredecessorObj<Request> _request;
        private PredecessorObj<Course> _course;

        // Fields

        // Results

        // Business constructor
        public Grant(
            Request request
            ,Course course
            )
        {
            InitializeResults();
            _request = new PredecessorObj<Request>(this, GetRoleRequest(), request);
            _course = new PredecessorObj<Course>(this, GetRoleCourse(), course);
        }

        // Hydration constructor
        private Grant(FactMemento memento)
        {
            InitializeResults();
            _request = new PredecessorObj<Request>(this, GetRoleRequest(), memento, Request.GetUnloadedInstance, Request.GetNullInstance);
            _course = new PredecessorObj<Course>(this, GetRoleCourse(), memento, Course.GetUnloadedInstance, Course.GetNullInstance);
        }

        // Result initializer
        private void InitializeResults()
        {
        }

        // Predecessor access
        public Request Request
        {
            get { return IsNull ? Request.GetNullInstance() : _request.Fact; }
        }
        public Course Course
        {
            get { return IsNull ? Course.GetNullInstance() : _course.Fact; }
        }

        // Field access

        // Query result access

        // Mutable property access

    }
    
    public partial class Accept : CorrespondenceFact
    {
		// Factory
		internal class CorrespondenceFactFactory : ICorrespondenceFactFactory
		{
			private IDictionary<Type, IFieldSerializer> _fieldSerializerByType;

			public CorrespondenceFactFactory(IDictionary<Type, IFieldSerializer> fieldSerializerByType)
			{
				_fieldSerializerByType = fieldSerializerByType;
			}

			public CorrespondenceFact CreateFact(FactMemento memento)
			{
				Accept newFact = new Accept(memento);

				// Create a memory stream from the memento data.
				using (MemoryStream data = new MemoryStream(memento.Data))
				{
					using (BinaryReader output = new BinaryReader(data))
					{
						newFact._unique = (Guid)_fieldSerializerByType[typeof(Guid)].ReadData(output);
					}
				}

				return newFact;
			}

			public void WriteFactData(CorrespondenceFact obj, BinaryWriter output)
			{
				Accept fact = (Accept)obj;
				_fieldSerializerByType[typeof(Guid)].WriteData(output, fact._unique);
			}

            public CorrespondenceFact GetUnloadedInstance()
            {
                return Accept.GetUnloadedInstance();
            }

            public CorrespondenceFact GetNullInstance()
            {
                return Accept.GetNullInstance();
            }
		}

		// Type
		internal static CorrespondenceFactType _correspondenceFactType = new CorrespondenceFactType(
			"Principia.Model.Accept", -662595790);

		protected override CorrespondenceFactType GetCorrespondenceFactType()
		{
			return _correspondenceFactType;
		}

        // Null and unloaded instances
        public static Accept GetUnloadedInstance()
        {
            return new Accept((FactMemento)null) { IsLoaded = false };
        }

        public static Accept GetNullInstance()
        {
            return new Accept((FactMemento)null) { IsNull = true };
        }

        // Ensure
        public Task<Accept> EnsureAsync()
        {
            if (_loadedTask != null)
                return _loadedTask.ContinueWith(t => (Accept)t.Result);
            else
                return Task.FromResult(this);
        }

        // Roles
        private static Role _cacheRoleGrant;
        public static Role GetRoleGrant()
        {
            if (_cacheRoleGrant == null)
            {
                _cacheRoleGrant = new Role(new RoleMemento(
			        _correspondenceFactType,
			        "grant",
			        Grant._correspondenceFactType,
			        false));
            }
            return _cacheRoleGrant;
        }

        // Queries
        private static Query _cacheQueryIsDeleted;

        public static Query GetQueryIsDeleted()
		{
            if (_cacheQueryIsDeleted == null)
            {
			    _cacheQueryIsDeleted = new Query()
		    		.JoinSuccessors(AcceptDelete.GetRoleAccept())
                ;
            }
            return _cacheQueryIsDeleted;
		}

        // Predicates
        public static Condition IsDeleted = Condition.WhereIsNotEmpty(GetQueryIsDeleted());

        // Predecessors
        private PredecessorObj<Grant> _grant;

        // Unique
        private Guid _unique;

        // Fields

        // Results

        // Business constructor
        public Accept(
            Grant grant
            )
        {
            _unique = Guid.NewGuid();
            InitializeResults();
            _grant = new PredecessorObj<Grant>(this, GetRoleGrant(), grant);
        }

        // Hydration constructor
        private Accept(FactMemento memento)
        {
            InitializeResults();
            _grant = new PredecessorObj<Grant>(this, GetRoleGrant(), memento, Grant.GetUnloadedInstance, Grant.GetNullInstance);
        }

        // Result initializer
        private void InitializeResults()
        {
        }

        // Predecessor access
        public Grant Grant
        {
            get { return IsNull ? Grant.GetNullInstance() : _grant.Fact; }
        }

        // Field access
		public Guid Unique { get { return _unique; } }


        // Query result access

        // Mutable property access

    }
    
    public partial class AcceptDelete : CorrespondenceFact
    {
		// Factory
		internal class CorrespondenceFactFactory : ICorrespondenceFactFactory
		{
			private IDictionary<Type, IFieldSerializer> _fieldSerializerByType;

			public CorrespondenceFactFactory(IDictionary<Type, IFieldSerializer> fieldSerializerByType)
			{
				_fieldSerializerByType = fieldSerializerByType;
			}

			public CorrespondenceFact CreateFact(FactMemento memento)
			{
				AcceptDelete newFact = new AcceptDelete(memento);


				return newFact;
			}

			public void WriteFactData(CorrespondenceFact obj, BinaryWriter output)
			{
				AcceptDelete fact = (AcceptDelete)obj;
			}

            public CorrespondenceFact GetUnloadedInstance()
            {
                return AcceptDelete.GetUnloadedInstance();
            }

            public CorrespondenceFact GetNullInstance()
            {
                return AcceptDelete.GetNullInstance();
            }
		}

		// Type
		internal static CorrespondenceFactType _correspondenceFactType = new CorrespondenceFactType(
			"Principia.Model.AcceptDelete", -887735056);

		protected override CorrespondenceFactType GetCorrespondenceFactType()
		{
			return _correspondenceFactType;
		}

        // Null and unloaded instances
        public static AcceptDelete GetUnloadedInstance()
        {
            return new AcceptDelete((FactMemento)null) { IsLoaded = false };
        }

        public static AcceptDelete GetNullInstance()
        {
            return new AcceptDelete((FactMemento)null) { IsNull = true };
        }

        // Ensure
        public Task<AcceptDelete> EnsureAsync()
        {
            if (_loadedTask != null)
                return _loadedTask.ContinueWith(t => (AcceptDelete)t.Result);
            else
                return Task.FromResult(this);
        }

        // Roles
        private static Role _cacheRoleAccept;
        public static Role GetRoleAccept()
        {
            if (_cacheRoleAccept == null)
            {
                _cacheRoleAccept = new Role(new RoleMemento(
			        _correspondenceFactType,
			        "accept",
			        Accept._correspondenceFactType,
			        false));
            }
            return _cacheRoleAccept;
        }

        // Queries

        // Predicates

        // Predecessors
        private PredecessorObj<Accept> _accept;

        // Fields

        // Results

        // Business constructor
        public AcceptDelete(
            Accept accept
            )
        {
            InitializeResults();
            _accept = new PredecessorObj<Accept>(this, GetRoleAccept(), accept);
        }

        // Hydration constructor
        private AcceptDelete(FactMemento memento)
        {
            InitializeResults();
            _accept = new PredecessorObj<Accept>(this, GetRoleAccept(), memento, Accept.GetUnloadedInstance, Accept.GetNullInstance);
        }

        // Result initializer
        private void InitializeResults()
        {
        }

        // Predecessor access
        public Accept Accept
        {
            get { return IsNull ? Accept.GetNullInstance() : _accept.Fact; }
        }

        // Field access

        // Query result access

        // Mutable property access

    }
    
    public partial class Course : CorrespondenceFact
    {
		// Factory
		internal class CorrespondenceFactFactory : ICorrespondenceFactFactory
		{
			private IDictionary<Type, IFieldSerializer> _fieldSerializerByType;

			public CorrespondenceFactFactory(IDictionary<Type, IFieldSerializer> fieldSerializerByType)
			{
				_fieldSerializerByType = fieldSerializerByType;
			}

			public CorrespondenceFact CreateFact(FactMemento memento)
			{
				Course newFact = new Course(memento);

				// Create a memory stream from the memento data.
				using (MemoryStream data = new MemoryStream(memento.Data))
				{
					using (BinaryReader output = new BinaryReader(data))
					{
						newFact._unique = (Guid)_fieldSerializerByType[typeof(Guid)].ReadData(output);
					}
				}

				return newFact;
			}

			public void WriteFactData(CorrespondenceFact obj, BinaryWriter output)
			{
				Course fact = (Course)obj;
				_fieldSerializerByType[typeof(Guid)].WriteData(output, fact._unique);
			}

            public CorrespondenceFact GetUnloadedInstance()
            {
                return Course.GetUnloadedInstance();
            }

            public CorrespondenceFact GetNullInstance()
            {
                return Course.GetNullInstance();
            }
		}

		// Type
		internal static CorrespondenceFactType _correspondenceFactType = new CorrespondenceFactType(
			"Principia.Model.Course", 2);

		protected override CorrespondenceFactType GetCorrespondenceFactType()
		{
			return _correspondenceFactType;
		}

        // Null and unloaded instances
        public static Course GetUnloadedInstance()
        {
            return new Course((FactMemento)null) { IsLoaded = false };
        }

        public static Course GetNullInstance()
        {
            return new Course((FactMemento)null) { IsNull = true };
        }

        // Ensure
        public Task<Course> EnsureAsync()
        {
            if (_loadedTask != null)
                return _loadedTask.ContinueWith(t => (Course)t.Result);
            else
                return Task.FromResult(this);
        }

        // Roles

        // Queries
        private static Query _cacheQueryTitle;

        public static Query GetQueryTitle()
		{
            if (_cacheQueryTitle == null)
            {
			    _cacheQueryTitle = new Query()
    				.JoinSuccessors(Course__title.GetRoleCourse(), Condition.WhereIsEmpty(Course__title.GetQueryIsCurrent())
				)
                ;
            }
            return _cacheQueryTitle;
		}
        private static Query _cacheQueryShortDescription;

        public static Query GetQueryShortDescription()
		{
            if (_cacheQueryShortDescription == null)
            {
			    _cacheQueryShortDescription = new Query()
    				.JoinSuccessors(Course__shortDescription.GetRoleCourse(), Condition.WhereIsEmpty(Course__shortDescription.GetQueryIsCurrent())
				)
                ;
            }
            return _cacheQueryShortDescription;
		}
        private static Query _cacheQueryDescription;

        public static Query GetQueryDescription()
		{
            if (_cacheQueryDescription == null)
            {
			    _cacheQueryDescription = new Query()
    				.JoinSuccessors(Course__description.GetRoleCourse(), Condition.WhereIsEmpty(Course__description.GetQueryIsCurrent())
				)
                ;
            }
            return _cacheQueryDescription;
		}
        private static Query _cacheQueryModules;

        public static Query GetQueryModules()
		{
            if (_cacheQueryModules == null)
            {
			    _cacheQueryModules = new Query()
		    		.JoinSuccessors(Module.GetRoleCourse())
                ;
            }
            return _cacheQueryModules;
		}

        // Predicates

        // Predecessors

        // Unique
        private Guid _unique;

        // Fields

        // Results
        private Result<Course__title> _title;
        private Result<Course__shortDescription> _shortDescription;
        private Result<Course__description> _description;
        private Result<Module> _modules;

        // Business constructor
        public Course(
            )
        {
            _unique = Guid.NewGuid();
            InitializeResults();
        }

        // Hydration constructor
        private Course(FactMemento memento)
        {
            InitializeResults();
        }

        // Result initializer
        private void InitializeResults()
        {
            _title = new Result<Course__title>(this, GetQueryTitle(), Course__title.GetUnloadedInstance, Course__title.GetNullInstance);
            _shortDescription = new Result<Course__shortDescription>(this, GetQueryShortDescription(), Course__shortDescription.GetUnloadedInstance, Course__shortDescription.GetNullInstance);
            _description = new Result<Course__description>(this, GetQueryDescription(), Course__description.GetUnloadedInstance, Course__description.GetNullInstance);
            _modules = new Result<Module>(this, GetQueryModules(), Module.GetUnloadedInstance, Module.GetNullInstance);
        }

        // Predecessor access

        // Field access
		public Guid Unique { get { return _unique; } }


        // Query result access
        public Result<Module> Modules
        {
            get { return _modules; }
        }

        // Mutable property access
        public TransientDisputable<Course__title, string> Title
        {
            get { return _title.AsTransientDisputable(fact => fact.Value); }
			set
			{
                Community.Perform(async delegate()
                {
                    var current = (await _title.EnsureAsync()).ToList();
                    if (current.Count != 1 || !object.Equals(current[0].Value, value.Value))
                    {
                        await Community.AddFactAsync(new Course__title(this, _title, value.Value));
                    }
                });
			}
        }
        public TransientDisputable<Course__shortDescription, string> ShortDescription
        {
            get { return _shortDescription.AsTransientDisputable(fact => fact.Value); }
			set
			{
                Community.Perform(async delegate()
                {
                    var current = (await _shortDescription.EnsureAsync()).ToList();
                    if (current.Count != 1 || !object.Equals(current[0].Value, value.Value))
                    {
                        await Community.AddFactAsync(new Course__shortDescription(this, _shortDescription, value.Value));
                    }
                });
			}
        }
        public TransientDisputable<Course__description, string> Description
        {
            get { return _description.AsTransientDisputable(fact => fact.Value); }
			set
			{
                Community.Perform(async delegate()
                {
                    var current = (await _description.EnsureAsync()).ToList();
                    if (current.Count != 1 || !object.Equals(current[0].Value, value.Value))
                    {
                        await Community.AddFactAsync(new Course__description(this, _description, value.Value));
                    }
                });
			}
        }

    }
    
    public partial class Course__title : CorrespondenceFact
    {
		// Factory
		internal class CorrespondenceFactFactory : ICorrespondenceFactFactory
		{
			private IDictionary<Type, IFieldSerializer> _fieldSerializerByType;

			public CorrespondenceFactFactory(IDictionary<Type, IFieldSerializer> fieldSerializerByType)
			{
				_fieldSerializerByType = fieldSerializerByType;
			}

			public CorrespondenceFact CreateFact(FactMemento memento)
			{
				Course__title newFact = new Course__title(memento);

				// Create a memory stream from the memento data.
				using (MemoryStream data = new MemoryStream(memento.Data))
				{
					using (BinaryReader output = new BinaryReader(data))
					{
						newFact._value = (string)_fieldSerializerByType[typeof(string)].ReadData(output);
					}
				}

				return newFact;
			}

			public void WriteFactData(CorrespondenceFact obj, BinaryWriter output)
			{
				Course__title fact = (Course__title)obj;
				_fieldSerializerByType[typeof(string)].WriteData(output, fact._value);
			}

            public CorrespondenceFact GetUnloadedInstance()
            {
                return Course__title.GetUnloadedInstance();
            }

            public CorrespondenceFact GetNullInstance()
            {
                return Course__title.GetNullInstance();
            }
		}

		// Type
		internal static CorrespondenceFactType _correspondenceFactType = new CorrespondenceFactType(
			"Principia.Model.Course__title", -1257751352);

		protected override CorrespondenceFactType GetCorrespondenceFactType()
		{
			return _correspondenceFactType;
		}

        // Null and unloaded instances
        public static Course__title GetUnloadedInstance()
        {
            return new Course__title((FactMemento)null) { IsLoaded = false };
        }

        public static Course__title GetNullInstance()
        {
            return new Course__title((FactMemento)null) { IsNull = true };
        }

        // Ensure
        public Task<Course__title> EnsureAsync()
        {
            if (_loadedTask != null)
                return _loadedTask.ContinueWith(t => (Course__title)t.Result);
            else
                return Task.FromResult(this);
        }

        // Roles
        private static Role _cacheRoleCourse;
        public static Role GetRoleCourse()
        {
            if (_cacheRoleCourse == null)
            {
                _cacheRoleCourse = new Role(new RoleMemento(
			        _correspondenceFactType,
			        "course",
			        Course._correspondenceFactType,
			        false));
            }
            return _cacheRoleCourse;
        }
        private static Role _cacheRolePrior;
        public static Role GetRolePrior()
        {
            if (_cacheRolePrior == null)
            {
                _cacheRolePrior = new Role(new RoleMemento(
			        _correspondenceFactType,
			        "prior",
			        Course__title._correspondenceFactType,
			        false));
            }
            return _cacheRolePrior;
        }

        // Queries
        private static Query _cacheQueryIsCurrent;

        public static Query GetQueryIsCurrent()
		{
            if (_cacheQueryIsCurrent == null)
            {
			    _cacheQueryIsCurrent = new Query()
		    		.JoinSuccessors(Course__title.GetRolePrior())
                ;
            }
            return _cacheQueryIsCurrent;
		}

        // Predicates
        public static Condition IsCurrent = Condition.WhereIsEmpty(GetQueryIsCurrent());

        // Predecessors
        private PredecessorObj<Course> _course;
        private PredecessorList<Course__title> _prior;

        // Fields
        private string _value;

        // Results

        // Business constructor
        public Course__title(
            Course course
            ,IEnumerable<Course__title> prior
            ,string value
            )
        {
            InitializeResults();
            _course = new PredecessorObj<Course>(this, GetRoleCourse(), course);
            _prior = new PredecessorList<Course__title>(this, GetRolePrior(), prior);
            _value = value;
        }

        // Hydration constructor
        private Course__title(FactMemento memento)
        {
            InitializeResults();
            _course = new PredecessorObj<Course>(this, GetRoleCourse(), memento, Course.GetUnloadedInstance, Course.GetNullInstance);
            _prior = new PredecessorList<Course__title>(this, GetRolePrior(), memento, Course__title.GetUnloadedInstance, Course__title.GetNullInstance);
        }

        // Result initializer
        private void InitializeResults()
        {
        }

        // Predecessor access
        public Course Course
        {
            get { return IsNull ? Course.GetNullInstance() : _course.Fact; }
        }
        public PredecessorList<Course__title> Prior
        {
            get { return _prior; }
        }

        // Field access
        public string Value
        {
            get { return _value; }
        }

        // Query result access

        // Mutable property access

    }
    
    public partial class Course__shortDescription : CorrespondenceFact
    {
		// Factory
		internal class CorrespondenceFactFactory : ICorrespondenceFactFactory
		{
			private IDictionary<Type, IFieldSerializer> _fieldSerializerByType;

			public CorrespondenceFactFactory(IDictionary<Type, IFieldSerializer> fieldSerializerByType)
			{
				_fieldSerializerByType = fieldSerializerByType;
			}

			public CorrespondenceFact CreateFact(FactMemento memento)
			{
				Course__shortDescription newFact = new Course__shortDescription(memento);

				// Create a memory stream from the memento data.
				using (MemoryStream data = new MemoryStream(memento.Data))
				{
					using (BinaryReader output = new BinaryReader(data))
					{
						newFact._value = (string)_fieldSerializerByType[typeof(string)].ReadData(output);
					}
				}

				return newFact;
			}

			public void WriteFactData(CorrespondenceFact obj, BinaryWriter output)
			{
				Course__shortDescription fact = (Course__shortDescription)obj;
				_fieldSerializerByType[typeof(string)].WriteData(output, fact._value);
			}

            public CorrespondenceFact GetUnloadedInstance()
            {
                return Course__shortDescription.GetUnloadedInstance();
            }

            public CorrespondenceFact GetNullInstance()
            {
                return Course__shortDescription.GetNullInstance();
            }
		}

		// Type
		internal static CorrespondenceFactType _correspondenceFactType = new CorrespondenceFactType(
			"Principia.Model.Course__shortDescription", -1257751352);

		protected override CorrespondenceFactType GetCorrespondenceFactType()
		{
			return _correspondenceFactType;
		}

        // Null and unloaded instances
        public static Course__shortDescription GetUnloadedInstance()
        {
            return new Course__shortDescription((FactMemento)null) { IsLoaded = false };
        }

        public static Course__shortDescription GetNullInstance()
        {
            return new Course__shortDescription((FactMemento)null) { IsNull = true };
        }

        // Ensure
        public Task<Course__shortDescription> EnsureAsync()
        {
            if (_loadedTask != null)
                return _loadedTask.ContinueWith(t => (Course__shortDescription)t.Result);
            else
                return Task.FromResult(this);
        }

        // Roles
        private static Role _cacheRoleCourse;
        public static Role GetRoleCourse()
        {
            if (_cacheRoleCourse == null)
            {
                _cacheRoleCourse = new Role(new RoleMemento(
			        _correspondenceFactType,
			        "course",
			        Course._correspondenceFactType,
			        false));
            }
            return _cacheRoleCourse;
        }
        private static Role _cacheRolePrior;
        public static Role GetRolePrior()
        {
            if (_cacheRolePrior == null)
            {
                _cacheRolePrior = new Role(new RoleMemento(
			        _correspondenceFactType,
			        "prior",
			        Course__shortDescription._correspondenceFactType,
			        false));
            }
            return _cacheRolePrior;
        }

        // Queries
        private static Query _cacheQueryIsCurrent;

        public static Query GetQueryIsCurrent()
		{
            if (_cacheQueryIsCurrent == null)
            {
			    _cacheQueryIsCurrent = new Query()
		    		.JoinSuccessors(Course__shortDescription.GetRolePrior())
                ;
            }
            return _cacheQueryIsCurrent;
		}

        // Predicates
        public static Condition IsCurrent = Condition.WhereIsEmpty(GetQueryIsCurrent());

        // Predecessors
        private PredecessorObj<Course> _course;
        private PredecessorList<Course__shortDescription> _prior;

        // Fields
        private string _value;

        // Results

        // Business constructor
        public Course__shortDescription(
            Course course
            ,IEnumerable<Course__shortDescription> prior
            ,string value
            )
        {
            InitializeResults();
            _course = new PredecessorObj<Course>(this, GetRoleCourse(), course);
            _prior = new PredecessorList<Course__shortDescription>(this, GetRolePrior(), prior);
            _value = value;
        }

        // Hydration constructor
        private Course__shortDescription(FactMemento memento)
        {
            InitializeResults();
            _course = new PredecessorObj<Course>(this, GetRoleCourse(), memento, Course.GetUnloadedInstance, Course.GetNullInstance);
            _prior = new PredecessorList<Course__shortDescription>(this, GetRolePrior(), memento, Course__shortDescription.GetUnloadedInstance, Course__shortDescription.GetNullInstance);
        }

        // Result initializer
        private void InitializeResults()
        {
        }

        // Predecessor access
        public Course Course
        {
            get { return IsNull ? Course.GetNullInstance() : _course.Fact; }
        }
        public PredecessorList<Course__shortDescription> Prior
        {
            get { return _prior; }
        }

        // Field access
        public string Value
        {
            get { return _value; }
        }

        // Query result access

        // Mutable property access

    }
    
    public partial class Course__description : CorrespondenceFact
    {
		// Factory
		internal class CorrespondenceFactFactory : ICorrespondenceFactFactory
		{
			private IDictionary<Type, IFieldSerializer> _fieldSerializerByType;

			public CorrespondenceFactFactory(IDictionary<Type, IFieldSerializer> fieldSerializerByType)
			{
				_fieldSerializerByType = fieldSerializerByType;
			}

			public CorrespondenceFact CreateFact(FactMemento memento)
			{
				Course__description newFact = new Course__description(memento);

				// Create a memory stream from the memento data.
				using (MemoryStream data = new MemoryStream(memento.Data))
				{
					using (BinaryReader output = new BinaryReader(data))
					{
						newFact._value = (string)_fieldSerializerByType[typeof(string)].ReadData(output);
					}
				}

				return newFact;
			}

			public void WriteFactData(CorrespondenceFact obj, BinaryWriter output)
			{
				Course__description fact = (Course__description)obj;
				_fieldSerializerByType[typeof(string)].WriteData(output, fact._value);
			}

            public CorrespondenceFact GetUnloadedInstance()
            {
                return Course__description.GetUnloadedInstance();
            }

            public CorrespondenceFact GetNullInstance()
            {
                return Course__description.GetNullInstance();
            }
		}

		// Type
		internal static CorrespondenceFactType _correspondenceFactType = new CorrespondenceFactType(
			"Principia.Model.Course__description", -1257751352);

		protected override CorrespondenceFactType GetCorrespondenceFactType()
		{
			return _correspondenceFactType;
		}

        // Null and unloaded instances
        public static Course__description GetUnloadedInstance()
        {
            return new Course__description((FactMemento)null) { IsLoaded = false };
        }

        public static Course__description GetNullInstance()
        {
            return new Course__description((FactMemento)null) { IsNull = true };
        }

        // Ensure
        public Task<Course__description> EnsureAsync()
        {
            if (_loadedTask != null)
                return _loadedTask.ContinueWith(t => (Course__description)t.Result);
            else
                return Task.FromResult(this);
        }

        // Roles
        private static Role _cacheRoleCourse;
        public static Role GetRoleCourse()
        {
            if (_cacheRoleCourse == null)
            {
                _cacheRoleCourse = new Role(new RoleMemento(
			        _correspondenceFactType,
			        "course",
			        Course._correspondenceFactType,
			        false));
            }
            return _cacheRoleCourse;
        }
        private static Role _cacheRolePrior;
        public static Role GetRolePrior()
        {
            if (_cacheRolePrior == null)
            {
                _cacheRolePrior = new Role(new RoleMemento(
			        _correspondenceFactType,
			        "prior",
			        Course__description._correspondenceFactType,
			        false));
            }
            return _cacheRolePrior;
        }

        // Queries
        private static Query _cacheQueryIsCurrent;

        public static Query GetQueryIsCurrent()
		{
            if (_cacheQueryIsCurrent == null)
            {
			    _cacheQueryIsCurrent = new Query()
		    		.JoinSuccessors(Course__description.GetRolePrior())
                ;
            }
            return _cacheQueryIsCurrent;
		}

        // Predicates
        public static Condition IsCurrent = Condition.WhereIsEmpty(GetQueryIsCurrent());

        // Predecessors
        private PredecessorObj<Course> _course;
        private PredecessorList<Course__description> _prior;

        // Fields
        private string _value;

        // Results

        // Business constructor
        public Course__description(
            Course course
            ,IEnumerable<Course__description> prior
            ,string value
            )
        {
            InitializeResults();
            _course = new PredecessorObj<Course>(this, GetRoleCourse(), course);
            _prior = new PredecessorList<Course__description>(this, GetRolePrior(), prior);
            _value = value;
        }

        // Hydration constructor
        private Course__description(FactMemento memento)
        {
            InitializeResults();
            _course = new PredecessorObj<Course>(this, GetRoleCourse(), memento, Course.GetUnloadedInstance, Course.GetNullInstance);
            _prior = new PredecessorList<Course__description>(this, GetRolePrior(), memento, Course__description.GetUnloadedInstance, Course__description.GetNullInstance);
        }

        // Result initializer
        private void InitializeResults()
        {
        }

        // Predecessor access
        public Course Course
        {
            get { return IsNull ? Course.GetNullInstance() : _course.Fact; }
        }
        public PredecessorList<Course__description> Prior
        {
            get { return _prior; }
        }

        // Field access
        public string Value
        {
            get { return _value; }
        }

        // Query result access

        // Mutable property access

    }
    
    public partial class Module : CorrespondenceFact
    {
		// Factory
		internal class CorrespondenceFactFactory : ICorrespondenceFactFactory
		{
			private IDictionary<Type, IFieldSerializer> _fieldSerializerByType;

			public CorrespondenceFactFactory(IDictionary<Type, IFieldSerializer> fieldSerializerByType)
			{
				_fieldSerializerByType = fieldSerializerByType;
			}

			public CorrespondenceFact CreateFact(FactMemento memento)
			{
				Module newFact = new Module(memento);

				// Create a memory stream from the memento data.
				using (MemoryStream data = new MemoryStream(memento.Data))
				{
					using (BinaryReader output = new BinaryReader(data))
					{
						newFact._unique = (Guid)_fieldSerializerByType[typeof(Guid)].ReadData(output);
					}
				}

				return newFact;
			}

			public void WriteFactData(CorrespondenceFact obj, BinaryWriter output)
			{
				Module fact = (Module)obj;
				_fieldSerializerByType[typeof(Guid)].WriteData(output, fact._unique);
			}

            public CorrespondenceFact GetUnloadedInstance()
            {
                return Module.GetUnloadedInstance();
            }

            public CorrespondenceFact GetNullInstance()
            {
                return Module.GetNullInstance();
            }
		}

		// Type
		internal static CorrespondenceFactType _correspondenceFactType = new CorrespondenceFactType(
			"Principia.Model.Module", 517634414);

		protected override CorrespondenceFactType GetCorrespondenceFactType()
		{
			return _correspondenceFactType;
		}

        // Null and unloaded instances
        public static Module GetUnloadedInstance()
        {
            return new Module((FactMemento)null) { IsLoaded = false };
        }

        public static Module GetNullInstance()
        {
            return new Module((FactMemento)null) { IsNull = true };
        }

        // Ensure
        public Task<Module> EnsureAsync()
        {
            if (_loadedTask != null)
                return _loadedTask.ContinueWith(t => (Module)t.Result);
            else
                return Task.FromResult(this);
        }

        // Roles
        private static Role _cacheRoleCourse;
        public static Role GetRoleCourse()
        {
            if (_cacheRoleCourse == null)
            {
                _cacheRoleCourse = new Role(new RoleMemento(
			        _correspondenceFactType,
			        "course",
			        Course._correspondenceFactType,
			        true));
            }
            return _cacheRoleCourse;
        }

        // Queries
        private static Query _cacheQueryOrdinal;

        public static Query GetQueryOrdinal()
		{
            if (_cacheQueryOrdinal == null)
            {
			    _cacheQueryOrdinal = new Query()
    				.JoinSuccessors(Module__ordinal.GetRoleModule(), Condition.WhereIsEmpty(Module__ordinal.GetQueryIsCurrent())
				)
                ;
            }
            return _cacheQueryOrdinal;
		}
        private static Query _cacheQueryTitle;

        public static Query GetQueryTitle()
		{
            if (_cacheQueryTitle == null)
            {
			    _cacheQueryTitle = new Query()
    				.JoinSuccessors(Module__title.GetRoleModule(), Condition.WhereIsEmpty(Module__title.GetQueryIsCurrent())
				)
                ;
            }
            return _cacheQueryTitle;
		}
        private static Query _cacheQueryClips;

        public static Query GetQueryClips()
		{
            if (_cacheQueryClips == null)
            {
			    _cacheQueryClips = new Query()
    				.JoinSuccessors(ClipModule.GetRoleModule(), Condition.WhereIsEmpty(ClipModule.GetQueryIsCurrent())
				)
		    		.JoinPredecessors(ClipModule.GetRoleClip())
                ;
            }
            return _cacheQueryClips;
		}

        // Predicates

        // Predecessors
        private PredecessorObj<Course> _course;

        // Unique
        private Guid _unique;

        // Fields

        // Results
        private Result<Module__ordinal> _ordinal;
        private Result<Module__title> _title;
        private Result<Clip> _clips;

        // Business constructor
        public Module(
            Course course
            )
        {
            _unique = Guid.NewGuid();
            InitializeResults();
            _course = new PredecessorObj<Course>(this, GetRoleCourse(), course);
        }

        // Hydration constructor
        private Module(FactMemento memento)
        {
            InitializeResults();
            _course = new PredecessorObj<Course>(this, GetRoleCourse(), memento, Course.GetUnloadedInstance, Course.GetNullInstance);
        }

        // Result initializer
        private void InitializeResults()
        {
            _ordinal = new Result<Module__ordinal>(this, GetQueryOrdinal(), Module__ordinal.GetUnloadedInstance, Module__ordinal.GetNullInstance);
            _title = new Result<Module__title>(this, GetQueryTitle(), Module__title.GetUnloadedInstance, Module__title.GetNullInstance);
            _clips = new Result<Clip>(this, GetQueryClips(), Clip.GetUnloadedInstance, Clip.GetNullInstance);
        }

        // Predecessor access
        public Course Course
        {
            get { return IsNull ? Course.GetNullInstance() : _course.Fact; }
        }

        // Field access
		public Guid Unique { get { return _unique; } }


        // Query result access
        public Result<Clip> Clips
        {
            get { return _clips; }
        }

        // Mutable property access
        public TransientDisputable<Module__ordinal, int> Ordinal
        {
            get { return _ordinal.AsTransientDisputable(fact => fact.Value); }
			set
			{
                Community.Perform(async delegate()
                {
                    var current = (await _ordinal.EnsureAsync()).ToList();
                    if (current.Count != 1 || !object.Equals(current[0].Value, value.Value))
                    {
                        await Community.AddFactAsync(new Module__ordinal(this, _ordinal, value.Value));
                    }
                });
			}
        }
        public TransientDisputable<Module__title, string> Title
        {
            get { return _title.AsTransientDisputable(fact => fact.Value); }
			set
			{
                Community.Perform(async delegate()
                {
                    var current = (await _title.EnsureAsync()).ToList();
                    if (current.Count != 1 || !object.Equals(current[0].Value, value.Value))
                    {
                        await Community.AddFactAsync(new Module__title(this, _title, value.Value));
                    }
                });
			}
        }

    }
    
    public partial class Module__ordinal : CorrespondenceFact
    {
		// Factory
		internal class CorrespondenceFactFactory : ICorrespondenceFactFactory
		{
			private IDictionary<Type, IFieldSerializer> _fieldSerializerByType;

			public CorrespondenceFactFactory(IDictionary<Type, IFieldSerializer> fieldSerializerByType)
			{
				_fieldSerializerByType = fieldSerializerByType;
			}

			public CorrespondenceFact CreateFact(FactMemento memento)
			{
				Module__ordinal newFact = new Module__ordinal(memento);

				// Create a memory stream from the memento data.
				using (MemoryStream data = new MemoryStream(memento.Data))
				{
					using (BinaryReader output = new BinaryReader(data))
					{
						newFact._value = (int)_fieldSerializerByType[typeof(int)].ReadData(output);
					}
				}

				return newFact;
			}

			public void WriteFactData(CorrespondenceFact obj, BinaryWriter output)
			{
				Module__ordinal fact = (Module__ordinal)obj;
				_fieldSerializerByType[typeof(int)].WriteData(output, fact._value);
			}

            public CorrespondenceFact GetUnloadedInstance()
            {
                return Module__ordinal.GetUnloadedInstance();
            }

            public CorrespondenceFact GetNullInstance()
            {
                return Module__ordinal.GetNullInstance();
            }
		}

		// Type
		internal static CorrespondenceFactType _correspondenceFactType = new CorrespondenceFactType(
			"Principia.Model.Module__ordinal", -164203780);

		protected override CorrespondenceFactType GetCorrespondenceFactType()
		{
			return _correspondenceFactType;
		}

        // Null and unloaded instances
        public static Module__ordinal GetUnloadedInstance()
        {
            return new Module__ordinal((FactMemento)null) { IsLoaded = false };
        }

        public static Module__ordinal GetNullInstance()
        {
            return new Module__ordinal((FactMemento)null) { IsNull = true };
        }

        // Ensure
        public Task<Module__ordinal> EnsureAsync()
        {
            if (_loadedTask != null)
                return _loadedTask.ContinueWith(t => (Module__ordinal)t.Result);
            else
                return Task.FromResult(this);
        }

        // Roles
        private static Role _cacheRoleModule;
        public static Role GetRoleModule()
        {
            if (_cacheRoleModule == null)
            {
                _cacheRoleModule = new Role(new RoleMemento(
			        _correspondenceFactType,
			        "module",
			        Module._correspondenceFactType,
			        false));
            }
            return _cacheRoleModule;
        }
        private static Role _cacheRolePrior;
        public static Role GetRolePrior()
        {
            if (_cacheRolePrior == null)
            {
                _cacheRolePrior = new Role(new RoleMemento(
			        _correspondenceFactType,
			        "prior",
			        Module__ordinal._correspondenceFactType,
			        false));
            }
            return _cacheRolePrior;
        }

        // Queries
        private static Query _cacheQueryIsCurrent;

        public static Query GetQueryIsCurrent()
		{
            if (_cacheQueryIsCurrent == null)
            {
			    _cacheQueryIsCurrent = new Query()
		    		.JoinSuccessors(Module__ordinal.GetRolePrior())
                ;
            }
            return _cacheQueryIsCurrent;
		}

        // Predicates
        public static Condition IsCurrent = Condition.WhereIsEmpty(GetQueryIsCurrent());

        // Predecessors
        private PredecessorObj<Module> _module;
        private PredecessorList<Module__ordinal> _prior;

        // Fields
        private int _value;

        // Results

        // Business constructor
        public Module__ordinal(
            Module module
            ,IEnumerable<Module__ordinal> prior
            ,int value
            )
        {
            InitializeResults();
            _module = new PredecessorObj<Module>(this, GetRoleModule(), module);
            _prior = new PredecessorList<Module__ordinal>(this, GetRolePrior(), prior);
            _value = value;
        }

        // Hydration constructor
        private Module__ordinal(FactMemento memento)
        {
            InitializeResults();
            _module = new PredecessorObj<Module>(this, GetRoleModule(), memento, Module.GetUnloadedInstance, Module.GetNullInstance);
            _prior = new PredecessorList<Module__ordinal>(this, GetRolePrior(), memento, Module__ordinal.GetUnloadedInstance, Module__ordinal.GetNullInstance);
        }

        // Result initializer
        private void InitializeResults()
        {
        }

        // Predecessor access
        public Module Module
        {
            get { return IsNull ? Module.GetNullInstance() : _module.Fact; }
        }
        public PredecessorList<Module__ordinal> Prior
        {
            get { return _prior; }
        }

        // Field access
        public int Value
        {
            get { return _value; }
        }

        // Query result access

        // Mutable property access

    }
    
    public partial class Module__title : CorrespondenceFact
    {
		// Factory
		internal class CorrespondenceFactFactory : ICorrespondenceFactFactory
		{
			private IDictionary<Type, IFieldSerializer> _fieldSerializerByType;

			public CorrespondenceFactFactory(IDictionary<Type, IFieldSerializer> fieldSerializerByType)
			{
				_fieldSerializerByType = fieldSerializerByType;
			}

			public CorrespondenceFact CreateFact(FactMemento memento)
			{
				Module__title newFact = new Module__title(memento);

				// Create a memory stream from the memento data.
				using (MemoryStream data = new MemoryStream(memento.Data))
				{
					using (BinaryReader output = new BinaryReader(data))
					{
						newFact._value = (string)_fieldSerializerByType[typeof(string)].ReadData(output);
					}
				}

				return newFact;
			}

			public void WriteFactData(CorrespondenceFact obj, BinaryWriter output)
			{
				Module__title fact = (Module__title)obj;
				_fieldSerializerByType[typeof(string)].WriteData(output, fact._value);
			}

            public CorrespondenceFact GetUnloadedInstance()
            {
                return Module__title.GetUnloadedInstance();
            }

            public CorrespondenceFact GetNullInstance()
            {
                return Module__title.GetNullInstance();
            }
		}

		// Type
		internal static CorrespondenceFactType _correspondenceFactType = new CorrespondenceFactType(
			"Principia.Model.Module__title", -164203792);

		protected override CorrespondenceFactType GetCorrespondenceFactType()
		{
			return _correspondenceFactType;
		}

        // Null and unloaded instances
        public static Module__title GetUnloadedInstance()
        {
            return new Module__title((FactMemento)null) { IsLoaded = false };
        }

        public static Module__title GetNullInstance()
        {
            return new Module__title((FactMemento)null) { IsNull = true };
        }

        // Ensure
        public Task<Module__title> EnsureAsync()
        {
            if (_loadedTask != null)
                return _loadedTask.ContinueWith(t => (Module__title)t.Result);
            else
                return Task.FromResult(this);
        }

        // Roles
        private static Role _cacheRoleModule;
        public static Role GetRoleModule()
        {
            if (_cacheRoleModule == null)
            {
                _cacheRoleModule = new Role(new RoleMemento(
			        _correspondenceFactType,
			        "module",
			        Module._correspondenceFactType,
			        false));
            }
            return _cacheRoleModule;
        }
        private static Role _cacheRolePrior;
        public static Role GetRolePrior()
        {
            if (_cacheRolePrior == null)
            {
                _cacheRolePrior = new Role(new RoleMemento(
			        _correspondenceFactType,
			        "prior",
			        Module__title._correspondenceFactType,
			        false));
            }
            return _cacheRolePrior;
        }

        // Queries
        private static Query _cacheQueryIsCurrent;

        public static Query GetQueryIsCurrent()
		{
            if (_cacheQueryIsCurrent == null)
            {
			    _cacheQueryIsCurrent = new Query()
		    		.JoinSuccessors(Module__title.GetRolePrior())
                ;
            }
            return _cacheQueryIsCurrent;
		}

        // Predicates
        public static Condition IsCurrent = Condition.WhereIsEmpty(GetQueryIsCurrent());

        // Predecessors
        private PredecessorObj<Module> _module;
        private PredecessorList<Module__title> _prior;

        // Fields
        private string _value;

        // Results

        // Business constructor
        public Module__title(
            Module module
            ,IEnumerable<Module__title> prior
            ,string value
            )
        {
            InitializeResults();
            _module = new PredecessorObj<Module>(this, GetRoleModule(), module);
            _prior = new PredecessorList<Module__title>(this, GetRolePrior(), prior);
            _value = value;
        }

        // Hydration constructor
        private Module__title(FactMemento memento)
        {
            InitializeResults();
            _module = new PredecessorObj<Module>(this, GetRoleModule(), memento, Module.GetUnloadedInstance, Module.GetNullInstance);
            _prior = new PredecessorList<Module__title>(this, GetRolePrior(), memento, Module__title.GetUnloadedInstance, Module__title.GetNullInstance);
        }

        // Result initializer
        private void InitializeResults()
        {
        }

        // Predecessor access
        public Module Module
        {
            get { return IsNull ? Module.GetNullInstance() : _module.Fact; }
        }
        public PredecessorList<Module__title> Prior
        {
            get { return _prior; }
        }

        // Field access
        public string Value
        {
            get { return _value; }
        }

        // Query result access

        // Mutable property access

    }
    
    public partial class Clip : CorrespondenceFact
    {
		// Factory
		internal class CorrespondenceFactFactory : ICorrespondenceFactFactory
		{
			private IDictionary<Type, IFieldSerializer> _fieldSerializerByType;

			public CorrespondenceFactFactory(IDictionary<Type, IFieldSerializer> fieldSerializerByType)
			{
				_fieldSerializerByType = fieldSerializerByType;
			}

			public CorrespondenceFact CreateFact(FactMemento memento)
			{
				Clip newFact = new Clip(memento);

				// Create a memory stream from the memento data.
				using (MemoryStream data = new MemoryStream(memento.Data))
				{
					using (BinaryReader output = new BinaryReader(data))
					{
						newFact._unique = (Guid)_fieldSerializerByType[typeof(Guid)].ReadData(output);
					}
				}

				return newFact;
			}

			public void WriteFactData(CorrespondenceFact obj, BinaryWriter output)
			{
				Clip fact = (Clip)obj;
				_fieldSerializerByType[typeof(Guid)].WriteData(output, fact._unique);
			}

            public CorrespondenceFact GetUnloadedInstance()
            {
                return Clip.GetUnloadedInstance();
            }

            public CorrespondenceFact GetNullInstance()
            {
                return Clip.GetNullInstance();
            }
		}

		// Type
		internal static CorrespondenceFactType _correspondenceFactType = new CorrespondenceFactType(
			"Principia.Model.Clip", 517634414);

		protected override CorrespondenceFactType GetCorrespondenceFactType()
		{
			return _correspondenceFactType;
		}

        // Null and unloaded instances
        public static Clip GetUnloadedInstance()
        {
            return new Clip((FactMemento)null) { IsLoaded = false };
        }

        public static Clip GetNullInstance()
        {
            return new Clip((FactMemento)null) { IsNull = true };
        }

        // Ensure
        public Task<Clip> EnsureAsync()
        {
            if (_loadedTask != null)
                return _loadedTask.ContinueWith(t => (Clip)t.Result);
            else
                return Task.FromResult(this);
        }

        // Roles
        private static Role _cacheRoleCourse;
        public static Role GetRoleCourse()
        {
            if (_cacheRoleCourse == null)
            {
                _cacheRoleCourse = new Role(new RoleMemento(
			        _correspondenceFactType,
			        "course",
			        Course._correspondenceFactType,
			        true));
            }
            return _cacheRoleCourse;
        }

        // Queries
        private static Query _cacheQueryOrdinal;

        public static Query GetQueryOrdinal()
		{
            if (_cacheQueryOrdinal == null)
            {
			    _cacheQueryOrdinal = new Query()
    				.JoinSuccessors(Clip__ordinal.GetRoleClip(), Condition.WhereIsEmpty(Clip__ordinal.GetQueryIsCurrent())
				)
                ;
            }
            return _cacheQueryOrdinal;
		}
        private static Query _cacheQueryTitle;

        public static Query GetQueryTitle()
		{
            if (_cacheQueryTitle == null)
            {
			    _cacheQueryTitle = new Query()
    				.JoinSuccessors(Clip__title.GetRoleClip(), Condition.WhereIsEmpty(Clip__title.GetQueryIsCurrent())
				)
                ;
            }
            return _cacheQueryTitle;
		}

        // Predicates

        // Predecessors
        private PredecessorObj<Course> _course;

        // Unique
        private Guid _unique;

        // Fields

        // Results
        private Result<Clip__ordinal> _ordinal;
        private Result<Clip__title> _title;

        // Business constructor
        public Clip(
            Course course
            )
        {
            _unique = Guid.NewGuid();
            InitializeResults();
            _course = new PredecessorObj<Course>(this, GetRoleCourse(), course);
        }

        // Hydration constructor
        private Clip(FactMemento memento)
        {
            InitializeResults();
            _course = new PredecessorObj<Course>(this, GetRoleCourse(), memento, Course.GetUnloadedInstance, Course.GetNullInstance);
        }

        // Result initializer
        private void InitializeResults()
        {
            _ordinal = new Result<Clip__ordinal>(this, GetQueryOrdinal(), Clip__ordinal.GetUnloadedInstance, Clip__ordinal.GetNullInstance);
            _title = new Result<Clip__title>(this, GetQueryTitle(), Clip__title.GetUnloadedInstance, Clip__title.GetNullInstance);
        }

        // Predecessor access
        public Course Course
        {
            get { return IsNull ? Course.GetNullInstance() : _course.Fact; }
        }

        // Field access
		public Guid Unique { get { return _unique; } }


        // Query result access

        // Mutable property access
        public TransientDisputable<Clip__ordinal, int> Ordinal
        {
            get { return _ordinal.AsTransientDisputable(fact => fact.Value); }
			set
			{
                Community.Perform(async delegate()
                {
                    var current = (await _ordinal.EnsureAsync()).ToList();
                    if (current.Count != 1 || !object.Equals(current[0].Value, value.Value))
                    {
                        await Community.AddFactAsync(new Clip__ordinal(this, _ordinal, value.Value));
                    }
                });
			}
        }
        public TransientDisputable<Clip__title, string> Title
        {
            get { return _title.AsTransientDisputable(fact => fact.Value); }
			set
			{
                Community.Perform(async delegate()
                {
                    var current = (await _title.EnsureAsync()).ToList();
                    if (current.Count != 1 || !object.Equals(current[0].Value, value.Value))
                    {
                        await Community.AddFactAsync(new Clip__title(this, _title, value.Value));
                    }
                });
			}
        }

    }
    
    public partial class Clip__ordinal : CorrespondenceFact
    {
		// Factory
		internal class CorrespondenceFactFactory : ICorrespondenceFactFactory
		{
			private IDictionary<Type, IFieldSerializer> _fieldSerializerByType;

			public CorrespondenceFactFactory(IDictionary<Type, IFieldSerializer> fieldSerializerByType)
			{
				_fieldSerializerByType = fieldSerializerByType;
			}

			public CorrespondenceFact CreateFact(FactMemento memento)
			{
				Clip__ordinal newFact = new Clip__ordinal(memento);

				// Create a memory stream from the memento data.
				using (MemoryStream data = new MemoryStream(memento.Data))
				{
					using (BinaryReader output = new BinaryReader(data))
					{
						newFact._value = (int)_fieldSerializerByType[typeof(int)].ReadData(output);
					}
				}

				return newFact;
			}

			public void WriteFactData(CorrespondenceFact obj, BinaryWriter output)
			{
				Clip__ordinal fact = (Clip__ordinal)obj;
				_fieldSerializerByType[typeof(int)].WriteData(output, fact._value);
			}

            public CorrespondenceFact GetUnloadedInstance()
            {
                return Clip__ordinal.GetUnloadedInstance();
            }

            public CorrespondenceFact GetNullInstance()
            {
                return Clip__ordinal.GetNullInstance();
            }
		}

		// Type
		internal static CorrespondenceFactType _correspondenceFactType = new CorrespondenceFactType(
			"Principia.Model.Clip__ordinal", 1139792036);

		protected override CorrespondenceFactType GetCorrespondenceFactType()
		{
			return _correspondenceFactType;
		}

        // Null and unloaded instances
        public static Clip__ordinal GetUnloadedInstance()
        {
            return new Clip__ordinal((FactMemento)null) { IsLoaded = false };
        }

        public static Clip__ordinal GetNullInstance()
        {
            return new Clip__ordinal((FactMemento)null) { IsNull = true };
        }

        // Ensure
        public Task<Clip__ordinal> EnsureAsync()
        {
            if (_loadedTask != null)
                return _loadedTask.ContinueWith(t => (Clip__ordinal)t.Result);
            else
                return Task.FromResult(this);
        }

        // Roles
        private static Role _cacheRoleClip;
        public static Role GetRoleClip()
        {
            if (_cacheRoleClip == null)
            {
                _cacheRoleClip = new Role(new RoleMemento(
			        _correspondenceFactType,
			        "clip",
			        Clip._correspondenceFactType,
			        false));
            }
            return _cacheRoleClip;
        }
        private static Role _cacheRolePrior;
        public static Role GetRolePrior()
        {
            if (_cacheRolePrior == null)
            {
                _cacheRolePrior = new Role(new RoleMemento(
			        _correspondenceFactType,
			        "prior",
			        Clip__ordinal._correspondenceFactType,
			        false));
            }
            return _cacheRolePrior;
        }

        // Queries
        private static Query _cacheQueryIsCurrent;

        public static Query GetQueryIsCurrent()
		{
            if (_cacheQueryIsCurrent == null)
            {
			    _cacheQueryIsCurrent = new Query()
		    		.JoinSuccessors(Clip__ordinal.GetRolePrior())
                ;
            }
            return _cacheQueryIsCurrent;
		}

        // Predicates
        public static Condition IsCurrent = Condition.WhereIsEmpty(GetQueryIsCurrent());

        // Predecessors
        private PredecessorObj<Clip> _clip;
        private PredecessorList<Clip__ordinal> _prior;

        // Fields
        private int _value;

        // Results

        // Business constructor
        public Clip__ordinal(
            Clip clip
            ,IEnumerable<Clip__ordinal> prior
            ,int value
            )
        {
            InitializeResults();
            _clip = new PredecessorObj<Clip>(this, GetRoleClip(), clip);
            _prior = new PredecessorList<Clip__ordinal>(this, GetRolePrior(), prior);
            _value = value;
        }

        // Hydration constructor
        private Clip__ordinal(FactMemento memento)
        {
            InitializeResults();
            _clip = new PredecessorObj<Clip>(this, GetRoleClip(), memento, Clip.GetUnloadedInstance, Clip.GetNullInstance);
            _prior = new PredecessorList<Clip__ordinal>(this, GetRolePrior(), memento, Clip__ordinal.GetUnloadedInstance, Clip__ordinal.GetNullInstance);
        }

        // Result initializer
        private void InitializeResults()
        {
        }

        // Predecessor access
        public Clip Clip
        {
            get { return IsNull ? Clip.GetNullInstance() : _clip.Fact; }
        }
        public PredecessorList<Clip__ordinal> Prior
        {
            get { return _prior; }
        }

        // Field access
        public int Value
        {
            get { return _value; }
        }

        // Query result access

        // Mutable property access

    }
    
    public partial class Clip__title : CorrespondenceFact
    {
		// Factory
		internal class CorrespondenceFactFactory : ICorrespondenceFactFactory
		{
			private IDictionary<Type, IFieldSerializer> _fieldSerializerByType;

			public CorrespondenceFactFactory(IDictionary<Type, IFieldSerializer> fieldSerializerByType)
			{
				_fieldSerializerByType = fieldSerializerByType;
			}

			public CorrespondenceFact CreateFact(FactMemento memento)
			{
				Clip__title newFact = new Clip__title(memento);

				// Create a memory stream from the memento data.
				using (MemoryStream data = new MemoryStream(memento.Data))
				{
					using (BinaryReader output = new BinaryReader(data))
					{
						newFact._value = (string)_fieldSerializerByType[typeof(string)].ReadData(output);
					}
				}

				return newFact;
			}

			public void WriteFactData(CorrespondenceFact obj, BinaryWriter output)
			{
				Clip__title fact = (Clip__title)obj;
				_fieldSerializerByType[typeof(string)].WriteData(output, fact._value);
			}

            public CorrespondenceFact GetUnloadedInstance()
            {
                return Clip__title.GetUnloadedInstance();
            }

            public CorrespondenceFact GetNullInstance()
            {
                return Clip__title.GetNullInstance();
            }
		}

		// Type
		internal static CorrespondenceFactType _correspondenceFactType = new CorrespondenceFactType(
			"Principia.Model.Clip__title", 1139792024);

		protected override CorrespondenceFactType GetCorrespondenceFactType()
		{
			return _correspondenceFactType;
		}

        // Null and unloaded instances
        public static Clip__title GetUnloadedInstance()
        {
            return new Clip__title((FactMemento)null) { IsLoaded = false };
        }

        public static Clip__title GetNullInstance()
        {
            return new Clip__title((FactMemento)null) { IsNull = true };
        }

        // Ensure
        public Task<Clip__title> EnsureAsync()
        {
            if (_loadedTask != null)
                return _loadedTask.ContinueWith(t => (Clip__title)t.Result);
            else
                return Task.FromResult(this);
        }

        // Roles
        private static Role _cacheRoleClip;
        public static Role GetRoleClip()
        {
            if (_cacheRoleClip == null)
            {
                _cacheRoleClip = new Role(new RoleMemento(
			        _correspondenceFactType,
			        "clip",
			        Clip._correspondenceFactType,
			        false));
            }
            return _cacheRoleClip;
        }
        private static Role _cacheRolePrior;
        public static Role GetRolePrior()
        {
            if (_cacheRolePrior == null)
            {
                _cacheRolePrior = new Role(new RoleMemento(
			        _correspondenceFactType,
			        "prior",
			        Clip__title._correspondenceFactType,
			        false));
            }
            return _cacheRolePrior;
        }

        // Queries
        private static Query _cacheQueryIsCurrent;

        public static Query GetQueryIsCurrent()
		{
            if (_cacheQueryIsCurrent == null)
            {
			    _cacheQueryIsCurrent = new Query()
		    		.JoinSuccessors(Clip__title.GetRolePrior())
                ;
            }
            return _cacheQueryIsCurrent;
		}

        // Predicates
        public static Condition IsCurrent = Condition.WhereIsEmpty(GetQueryIsCurrent());

        // Predecessors
        private PredecessorObj<Clip> _clip;
        private PredecessorList<Clip__title> _prior;

        // Fields
        private string _value;

        // Results

        // Business constructor
        public Clip__title(
            Clip clip
            ,IEnumerable<Clip__title> prior
            ,string value
            )
        {
            InitializeResults();
            _clip = new PredecessorObj<Clip>(this, GetRoleClip(), clip);
            _prior = new PredecessorList<Clip__title>(this, GetRolePrior(), prior);
            _value = value;
        }

        // Hydration constructor
        private Clip__title(FactMemento memento)
        {
            InitializeResults();
            _clip = new PredecessorObj<Clip>(this, GetRoleClip(), memento, Clip.GetUnloadedInstance, Clip.GetNullInstance);
            _prior = new PredecessorList<Clip__title>(this, GetRolePrior(), memento, Clip__title.GetUnloadedInstance, Clip__title.GetNullInstance);
        }

        // Result initializer
        private void InitializeResults()
        {
        }

        // Predecessor access
        public Clip Clip
        {
            get { return IsNull ? Clip.GetNullInstance() : _clip.Fact; }
        }
        public PredecessorList<Clip__title> Prior
        {
            get { return _prior; }
        }

        // Field access
        public string Value
        {
            get { return _value; }
        }

        // Query result access

        // Mutable property access

    }
    
    public partial class ClipModule : CorrespondenceFact
    {
		// Factory
		internal class CorrespondenceFactFactory : ICorrespondenceFactFactory
		{
			private IDictionary<Type, IFieldSerializer> _fieldSerializerByType;

			public CorrespondenceFactFactory(IDictionary<Type, IFieldSerializer> fieldSerializerByType)
			{
				_fieldSerializerByType = fieldSerializerByType;
			}

			public CorrespondenceFact CreateFact(FactMemento memento)
			{
				ClipModule newFact = new ClipModule(memento);


				return newFact;
			}

			public void WriteFactData(CorrespondenceFact obj, BinaryWriter output)
			{
				ClipModule fact = (ClipModule)obj;
			}

            public CorrespondenceFact GetUnloadedInstance()
            {
                return ClipModule.GetUnloadedInstance();
            }

            public CorrespondenceFact GetNullInstance()
            {
                return ClipModule.GetNullInstance();
            }
		}

		// Type
		internal static CorrespondenceFactType _correspondenceFactType = new CorrespondenceFactType(
			"Principia.Model.ClipModule", 855963584);

		protected override CorrespondenceFactType GetCorrespondenceFactType()
		{
			return _correspondenceFactType;
		}

        // Null and unloaded instances
        public static ClipModule GetUnloadedInstance()
        {
            return new ClipModule((FactMemento)null) { IsLoaded = false };
        }

        public static ClipModule GetNullInstance()
        {
            return new ClipModule((FactMemento)null) { IsNull = true };
        }

        // Ensure
        public Task<ClipModule> EnsureAsync()
        {
            if (_loadedTask != null)
                return _loadedTask.ContinueWith(t => (ClipModule)t.Result);
            else
                return Task.FromResult(this);
        }

        // Roles
        private static Role _cacheRoleClip;
        public static Role GetRoleClip()
        {
            if (_cacheRoleClip == null)
            {
                _cacheRoleClip = new Role(new RoleMemento(
			        _correspondenceFactType,
			        "clip",
			        Clip._correspondenceFactType,
			        false));
            }
            return _cacheRoleClip;
        }
        private static Role _cacheRoleModule;
        public static Role GetRoleModule()
        {
            if (_cacheRoleModule == null)
            {
                _cacheRoleModule = new Role(new RoleMemento(
			        _correspondenceFactType,
			        "module",
			        Module._correspondenceFactType,
			        false));
            }
            return _cacheRoleModule;
        }
        private static Role _cacheRolePrior;
        public static Role GetRolePrior()
        {
            if (_cacheRolePrior == null)
            {
                _cacheRolePrior = new Role(new RoleMemento(
			        _correspondenceFactType,
			        "prior",
			        ClipModule._correspondenceFactType,
			        false));
            }
            return _cacheRolePrior;
        }

        // Queries
        private static Query _cacheQueryIsCurrent;

        public static Query GetQueryIsCurrent()
		{
            if (_cacheQueryIsCurrent == null)
            {
			    _cacheQueryIsCurrent = new Query()
		    		.JoinSuccessors(ClipModule.GetRolePrior())
                ;
            }
            return _cacheQueryIsCurrent;
		}

        // Predicates
        public static Condition IsCurrent = Condition.WhereIsEmpty(GetQueryIsCurrent());

        // Predecessors
        private PredecessorObj<Clip> _clip;
        private PredecessorObj<Module> _module;
        private PredecessorList<ClipModule> _prior;

        // Fields

        // Results

        // Business constructor
        public ClipModule(
            Clip clip
            ,Module module
            ,IEnumerable<ClipModule> prior
            )
        {
            InitializeResults();
            _clip = new PredecessorObj<Clip>(this, GetRoleClip(), clip);
            _module = new PredecessorObj<Module>(this, GetRoleModule(), module);
            _prior = new PredecessorList<ClipModule>(this, GetRolePrior(), prior);
        }

        // Hydration constructor
        private ClipModule(FactMemento memento)
        {
            InitializeResults();
            _clip = new PredecessorObj<Clip>(this, GetRoleClip(), memento, Clip.GetUnloadedInstance, Clip.GetNullInstance);
            _module = new PredecessorObj<Module>(this, GetRoleModule(), memento, Module.GetUnloadedInstance, Module.GetNullInstance);
            _prior = new PredecessorList<ClipModule>(this, GetRolePrior(), memento, ClipModule.GetUnloadedInstance, ClipModule.GetNullInstance);
        }

        // Result initializer
        private void InitializeResults()
        {
        }

        // Predecessor access
        public Clip Clip
        {
            get { return IsNull ? Clip.GetNullInstance() : _clip.Fact; }
        }
        public Module Module
        {
            get { return IsNull ? Module.GetNullInstance() : _module.Fact; }
        }
        public PredecessorList<ClipModule> Prior
        {
            get { return _prior; }
        }

        // Field access

        // Query result access

        // Mutable property access

    }
    

	public class CorrespondenceModel : ICorrespondenceModel
	{
		public void RegisterAllFactTypes(Community community, IDictionary<Type, IFieldSerializer> fieldSerializerByType)
		{
			community.AddType(
				Individual._correspondenceFactType,
				new Individual.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { Individual._correspondenceFactType }));
			community.AddQuery(
				Individual._correspondenceFactType,
				Individual.GetQueryCoursesAccepted().QueryDefinition);
			community.AddType(
				Token._correspondenceFactType,
				new Token.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { Token._correspondenceFactType }));
			community.AddType(
				Request._correspondenceFactType,
				new Request.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { Request._correspondenceFactType }));
			community.AddType(
				Grant._correspondenceFactType,
				new Grant.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { Grant._correspondenceFactType }));
			community.AddType(
				Accept._correspondenceFactType,
				new Accept.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { Accept._correspondenceFactType }));
			community.AddQuery(
				Accept._correspondenceFactType,
				Accept.GetQueryIsDeleted().QueryDefinition);
			community.AddType(
				AcceptDelete._correspondenceFactType,
				new AcceptDelete.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { AcceptDelete._correspondenceFactType }));
			community.AddType(
				Course._correspondenceFactType,
				new Course.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { Course._correspondenceFactType }));
			community.AddQuery(
				Course._correspondenceFactType,
				Course.GetQueryTitle().QueryDefinition);
			community.AddQuery(
				Course._correspondenceFactType,
				Course.GetQueryShortDescription().QueryDefinition);
			community.AddQuery(
				Course._correspondenceFactType,
				Course.GetQueryDescription().QueryDefinition);
			community.AddQuery(
				Course._correspondenceFactType,
				Course.GetQueryModules().QueryDefinition);
			community.AddType(
				Course__title._correspondenceFactType,
				new Course__title.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { Course__title._correspondenceFactType }));
			community.AddQuery(
				Course__title._correspondenceFactType,
				Course__title.GetQueryIsCurrent().QueryDefinition);
			community.AddType(
				Course__shortDescription._correspondenceFactType,
				new Course__shortDescription.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { Course__shortDescription._correspondenceFactType }));
			community.AddQuery(
				Course__shortDescription._correspondenceFactType,
				Course__shortDescription.GetQueryIsCurrent().QueryDefinition);
			community.AddType(
				Course__description._correspondenceFactType,
				new Course__description.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { Course__description._correspondenceFactType }));
			community.AddQuery(
				Course__description._correspondenceFactType,
				Course__description.GetQueryIsCurrent().QueryDefinition);
			community.AddType(
				Module._correspondenceFactType,
				new Module.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { Module._correspondenceFactType }));
			community.AddQuery(
				Module._correspondenceFactType,
				Module.GetQueryOrdinal().QueryDefinition);
			community.AddQuery(
				Module._correspondenceFactType,
				Module.GetQueryTitle().QueryDefinition);
			community.AddQuery(
				Module._correspondenceFactType,
				Module.GetQueryClips().QueryDefinition);
			community.AddType(
				Module__ordinal._correspondenceFactType,
				new Module__ordinal.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { Module__ordinal._correspondenceFactType }));
			community.AddQuery(
				Module__ordinal._correspondenceFactType,
				Module__ordinal.GetQueryIsCurrent().QueryDefinition);
			community.AddType(
				Module__title._correspondenceFactType,
				new Module__title.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { Module__title._correspondenceFactType }));
			community.AddQuery(
				Module__title._correspondenceFactType,
				Module__title.GetQueryIsCurrent().QueryDefinition);
			community.AddType(
				Clip._correspondenceFactType,
				new Clip.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { Clip._correspondenceFactType }));
			community.AddQuery(
				Clip._correspondenceFactType,
				Clip.GetQueryOrdinal().QueryDefinition);
			community.AddQuery(
				Clip._correspondenceFactType,
				Clip.GetQueryTitle().QueryDefinition);
			community.AddType(
				Clip__ordinal._correspondenceFactType,
				new Clip__ordinal.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { Clip__ordinal._correspondenceFactType }));
			community.AddQuery(
				Clip__ordinal._correspondenceFactType,
				Clip__ordinal.GetQueryIsCurrent().QueryDefinition);
			community.AddType(
				Clip__title._correspondenceFactType,
				new Clip__title.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { Clip__title._correspondenceFactType }));
			community.AddQuery(
				Clip__title._correspondenceFactType,
				Clip__title.GetQueryIsCurrent().QueryDefinition);
			community.AddType(
				ClipModule._correspondenceFactType,
				new ClipModule.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { ClipModule._correspondenceFactType }));
			community.AddQuery(
				ClipModule._correspondenceFactType,
				ClipModule.GetQueryIsCurrent().QueryDefinition);
		}
	}
}

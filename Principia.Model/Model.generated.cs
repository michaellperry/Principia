using System.Collections.Generic;
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
    Course__title -> Course
    Course__title -> Course__title [label="  *"]
    Course__shortDescription -> Course
    Course__shortDescription -> Course__shortDescription [label="  *"]
    Course__description -> Course
    Course__description -> Course__description [label="  *"]
    Share -> Individual
    Share -> Course
    ShareRevoke -> Share
    Module -> Course
    Module__ordinal -> Module
    Module__ordinal -> Module__ordinal [label="  *"]
    Module__title -> Module
    Module__title -> Module__title [label="  *"]
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
        private static Query _cacheQuerySharedCourses;

        public static Query GetQuerySharedCourses()
		{
            if (_cacheQuerySharedCourses == null)
            {
			    _cacheQuerySharedCourses = new Query()
    				.JoinSuccessors(Share.GetRoleIndividual(), Condition.WhereIsEmpty(Share.GetQueryIsCurrent())
				)
                ;
            }
            return _cacheQuerySharedCourses;
		}

        // Predicates

        // Predecessors

        // Fields
        private string _anonymousId;

        // Results
        private Result<Share> _sharedCourses;

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
            _sharedCourses = new Result<Share>(this, GetQuerySharedCourses(), Share.GetUnloadedInstance, Share.GetNullInstance);
        }

        // Predecessor access

        // Field access
        public string AnonymousId
        {
            get { return _anonymousId; }
        }

        // Query result access
        public Result<Share> SharedCourses
        {
            get { return _sharedCourses; }
        }

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
    
    public partial class Share : CorrespondenceFact
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
				Share newFact = new Share(memento);

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
				Share fact = (Share)obj;
				_fieldSerializerByType[typeof(Guid)].WriteData(output, fact._unique);
			}

            public CorrespondenceFact GetUnloadedInstance()
            {
                return Share.GetUnloadedInstance();
            }

            public CorrespondenceFact GetNullInstance()
            {
                return Share.GetNullInstance();
            }
		}

		// Type
		internal static CorrespondenceFactType _correspondenceFactType = new CorrespondenceFactType(
			"Principia.Model.Share", 1747774338);

		protected override CorrespondenceFactType GetCorrespondenceFactType()
		{
			return _correspondenceFactType;
		}

        // Null and unloaded instances
        public static Share GetUnloadedInstance()
        {
            return new Share((FactMemento)null) { IsLoaded = false };
        }

        public static Share GetNullInstance()
        {
            return new Share((FactMemento)null) { IsNull = true };
        }

        // Ensure
        public Task<Share> EnsureAsync()
        {
            if (_loadedTask != null)
                return _loadedTask.ContinueWith(t => (Share)t.Result);
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
			        false));
            }
            return _cacheRoleIndividual;
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
        private static Query _cacheQueryIsCurrent;

        public static Query GetQueryIsCurrent()
		{
            if (_cacheQueryIsCurrent == null)
            {
			    _cacheQueryIsCurrent = new Query()
		    		.JoinSuccessors(ShareRevoke.GetRoleShare())
                ;
            }
            return _cacheQueryIsCurrent;
		}

        // Predicates
        public static Condition IsCurrent = Condition.WhereIsEmpty(GetQueryIsCurrent());

        // Predecessors
        private PredecessorObj<Individual> _individual;
        private PredecessorObj<Course> _course;

        // Unique
        private Guid _unique;

        // Fields

        // Results

        // Business constructor
        public Share(
            Individual individual
            ,Course course
            )
        {
            _unique = Guid.NewGuid();
            InitializeResults();
            _individual = new PredecessorObj<Individual>(this, GetRoleIndividual(), individual);
            _course = new PredecessorObj<Course>(this, GetRoleCourse(), course);
        }

        // Hydration constructor
        private Share(FactMemento memento)
        {
            InitializeResults();
            _individual = new PredecessorObj<Individual>(this, GetRoleIndividual(), memento, Individual.GetUnloadedInstance, Individual.GetNullInstance);
            _course = new PredecessorObj<Course>(this, GetRoleCourse(), memento, Course.GetUnloadedInstance, Course.GetNullInstance);
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
        public Course Course
        {
            get { return IsNull ? Course.GetNullInstance() : _course.Fact; }
        }

        // Field access
		public Guid Unique { get { return _unique; } }


        // Query result access

        // Mutable property access

    }
    
    public partial class ShareRevoke : CorrespondenceFact
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
				ShareRevoke newFact = new ShareRevoke(memento);


				return newFact;
			}

			public void WriteFactData(CorrespondenceFact obj, BinaryWriter output)
			{
				ShareRevoke fact = (ShareRevoke)obj;
			}

            public CorrespondenceFact GetUnloadedInstance()
            {
                return ShareRevoke.GetUnloadedInstance();
            }

            public CorrespondenceFact GetNullInstance()
            {
                return ShareRevoke.GetNullInstance();
            }
		}

		// Type
		internal static CorrespondenceFactType _correspondenceFactType = new CorrespondenceFactType(
			"Principia.Model.ShareRevoke", 1755234432);

		protected override CorrespondenceFactType GetCorrespondenceFactType()
		{
			return _correspondenceFactType;
		}

        // Null and unloaded instances
        public static ShareRevoke GetUnloadedInstance()
        {
            return new ShareRevoke((FactMemento)null) { IsLoaded = false };
        }

        public static ShareRevoke GetNullInstance()
        {
            return new ShareRevoke((FactMemento)null) { IsNull = true };
        }

        // Ensure
        public Task<ShareRevoke> EnsureAsync()
        {
            if (_loadedTask != null)
                return _loadedTask.ContinueWith(t => (ShareRevoke)t.Result);
            else
                return Task.FromResult(this);
        }

        // Roles
        private static Role _cacheRoleShare;
        public static Role GetRoleShare()
        {
            if (_cacheRoleShare == null)
            {
                _cacheRoleShare = new Role(new RoleMemento(
			        _correspondenceFactType,
			        "share",
			        Share._correspondenceFactType,
			        false));
            }
            return _cacheRoleShare;
        }

        // Queries

        // Predicates

        // Predecessors
        private PredecessorObj<Share> _share;

        // Fields

        // Results

        // Business constructor
        public ShareRevoke(
            Share share
            )
        {
            InitializeResults();
            _share = new PredecessorObj<Share>(this, GetRoleShare(), share);
        }

        // Hydration constructor
        private ShareRevoke(FactMemento memento)
        {
            InitializeResults();
            _share = new PredecessorObj<Share>(this, GetRoleShare(), memento, Share.GetUnloadedInstance, Share.GetNullInstance);
        }

        // Result initializer
        private void InitializeResults()
        {
        }

        // Predecessor access
        public Share Share
        {
            get { return IsNull ? Share.GetNullInstance() : _share.Fact; }
        }

        // Field access

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
			"Principia.Model.Module", 517634410);

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
			        false));
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

        // Predicates

        // Predecessors
        private PredecessorObj<Course> _course;

        // Unique
        private Guid _unique;

        // Fields

        // Results
        private Result<Module__ordinal> _ordinal;
        private Result<Module__title> _title;

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
				Individual.GetQuerySharedCourses().QueryDefinition);
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
				Share._correspondenceFactType,
				new Share.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { Share._correspondenceFactType }));
			community.AddQuery(
				Share._correspondenceFactType,
				Share.GetQueryIsCurrent().QueryDefinition);
			community.AddType(
				ShareRevoke._correspondenceFactType,
				new ShareRevoke.CorrespondenceFactFactory(fieldSerializerByType),
				new FactMetadata(new List<CorrespondenceFactType> { ShareRevoke._correspondenceFactType }));
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
		}
	}
}

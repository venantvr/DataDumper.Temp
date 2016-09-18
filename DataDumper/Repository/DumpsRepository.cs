using System;
using System.Collections.Generic;
using DataDumper.Interfaces;

namespace DataDumper.Repository
{
    public class DumpsRepository
    {
        private readonly IDumper _dumper;

        private readonly Dictionary<Type, Dictionary<string, Func<object>>> _registry;

        public DumpsRepository(IDumper dumper)
        {
            _dumper = dumper;
            _registry = new Dictionary<Type, Dictionary<string, Func<object>>>();
        }

        public void Add<TEntity>(string name, Func<object> entity)
        {
            var type = typeof (TEntity);

            if (!_registry.ContainsKey(type))
            {
                _registry.Add(type, new Dictionary<string, Func<object>>());
            }

            _registry[type].Add(name, entity);
        }

        public void Dump<TEntity>(string name, Func<object> entity)
        {
            _dumper?.Dump(name, entity.Invoke());
        }

        public Dictionary<string, Func<object>> LambdaValues(Type type)
        {
            return _registry[type];
        }

        public void Flush()
        {
            foreach (var type in _registry.Keys)
            {
                foreach (var obj in LambdaValues(type))
                {
                    _dumper?.Dump(obj.Key, obj.Value.Invoke());
                }
            }
        }
    }
}
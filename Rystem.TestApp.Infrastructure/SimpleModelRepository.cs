using RepositoryFramework;
using Rystem.TestApp.Domain;

namespace Rystem.TestApp.Infrastructure
{
    public class SimpleModelRepository : IRepository<SimpleModel, string>
    {
        private Dictionary<string, SimpleModel> _inMemoryStorage = new();
        public Task<BatchResults<SimpleModel, string>> BatchAsync(BatchOperations<SimpleModel, string> operations, CancellationToken cancellationToken = default)
        {
            var results = new BatchResults<SimpleModel, string>();
            foreach (var operation in operations.Values)
            {
                if (operation.Command == CommandType.Insert)
                    results.AddInsert(operation.Key, new State<SimpleModel, string>(_inMemoryStorage.TryAdd(operation.Key, operation.Value)));
                else if (operation.Command == CommandType.Update)
                {
                    _inMemoryStorage[operation.Key] = operation.Value;
                    results.AddUpdate(operation.Key, State<SimpleModel, string>.Ok());
                }
                else if (operation.Command == CommandType.Delete)
                {
                    _inMemoryStorage.Remove(operation.Key);
                    results.AddDelete(operation.Key, State<SimpleModel, string>.Ok());
                }
            }
            return Task.FromResult(results);
        }

        public Task<State<SimpleModel, string>> DeleteAsync(string key, CancellationToken cancellationToken = default)
        {
            _inMemoryStorage.Remove(key);
            return Task.FromResult(State<SimpleModel, string>.Ok());
        }

        public Task<State<SimpleModel, string>> ExistAsync(string key, CancellationToken cancellationToken = default)
        {
            _inMemoryStorage.Remove(key);
            return Task.FromResult(State<SimpleModel, string>.Ok());
        }

        public Task<SimpleModel?> GetAsync(string key, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<State<SimpleModel, string>> InsertAsync(string key, SimpleModel value, CancellationToken cancellationToken = default)
        {
            //and so on
            return Task.FromResult(new State<SimpleModel, string>(_inMemoryStorage.TryAdd(key, value)));
        }

        public ValueTask<TProperty> OperationAsync<TProperty>(OperationType<TProperty> operation, IFilterExpression filter, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<Entity<SimpleModel, string>> QueryAsync(IFilterExpression filter, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<State<SimpleModel, string>> UpdateAsync(string key, SimpleModel value, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}